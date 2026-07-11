using System.Net;
using System.Net.Http.Json;
using DashkevichStudio.Application.Abstractions;
using DashkevichStudio.Domain.Leads;
using Microsoft.Extensions.Options;

namespace DashkevichStudio.Infrastructure.Telegram;

public sealed class TelegramNotifier(
    HttpClient httpClient,
    IOptions<TelegramOptions> options,
    IFileStorage fileStorage) : ITelegramNotifier
{
    private readonly TelegramOptions _options = options.Value;

    public async Task SendAsync(Lead lead, CancellationToken cancellationToken)
    {
        EnsureConfigured();
        var text = BuildMessage(lead);
        var messageResponse = await httpClient.PostAsJsonAsync(
            BuildApiUri("sendMessage"),
            new { chat_id = _options.ChatId, text, parse_mode = "HTML" },
            cancellationToken);

        await EnsureSuccessAsync(messageResponse, cancellationToken);

        if (lead.Attachment is not null && File.Exists(fileStorage.GetPath(lead.Attachment.StoredName)))
        {
            await using var file = File.OpenRead(fileStorage.GetPath(lead.Attachment.StoredName));
            using var content = new MultipartFormDataContent
            {
                { new StringContent(_options.ChatId), "chat_id" },
                { new StringContent($"Файл к заявке {lead.Id}"), "caption" },
                { new StringContent("HTML"), "parse_mode" },
                { new StreamContent(file), "document", lead.Attachment.OriginalName }
            };

            var documentResponse = await httpClient.PostAsync(
                BuildApiUri("sendDocument"),
                content,
                cancellationToken);

            await EnsureSuccessAsync(documentResponse, cancellationToken);
        }
    }

    private static string BuildMessage(Lead lead)
    {
        static string E(string? value) => WebUtility.HtmlEncode(value ?? "—");
        var service = lead.Service == "Свой вариант" ? lead.CustomService : lead.Service;

        return $"""
            <b>Новая заявка с сайта</b>

            <b>Имя:</b> {E(lead.Name)}
            <b>Связь:</b> {E(ContactMethodLabel(lead.ContactMethod))} — {E(lead.ContactValue)}
            <b>Услуга:</b> {E(service)}
            <b>Задача:</b>
            {E(lead.Description)}

            <b>Страница:</b> {E(lead.SourcePage)}
            <b>Создана:</b> {lead.CreatedAtUtc:dd.MM.yyyy HH:mm} UTC
            <b>ID:</b> <code>{lead.Id}</code>
            """;
    }

    private static string ContactMethodLabel(ContactMethod method) => method switch
    {
        ContactMethod.Telegram => "Telegram",
        ContactMethod.Instagram => "Instagram",
        ContactMethod.Email => "Email",
        ContactMethod.Phone => "Телефон",
        ContactMethod.Custom => "Другой способ",
        _ => method.ToString()
    };

    private static async Task EnsureSuccessAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
            return;

        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        throw new HttpRequestException($"Telegram API returned {(int)response.StatusCode}: {body}");
    }

    private void EnsureConfigured()
    {
        if (string.IsNullOrWhiteSpace(_options.BotToken) || string.IsNullOrWhiteSpace(_options.ChatId))
            throw new InvalidOperationException("Telegram BotToken and ChatId must be configured.");
    }

    private Uri BuildApiUri(string method) =>
        new($"https://api.telegram.org/bot{_options.BotToken}/{method}");
}
