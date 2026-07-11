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
        var attachmentPath = lead.Attachment is null
            ? null
            : fileStorage.GetPath(lead.Attachment.StoredName);

        if (lead.Attachment is not null && File.Exists(attachmentPath))
        {
            await using var file = File.OpenRead(attachmentPath);
            using var content = new MultipartFormDataContent
            {
                { new StringContent(_options.ChatId), "chat_id" },
                { new StringContent(BuildMessage(lead, compact: true)), "caption" },
                { new StringContent("HTML"), "parse_mode" },
                { new StreamContent(file), "document", lead.Attachment.OriginalName }
            };

            var documentResponse = await httpClient.PostAsync(
                BuildApiUri("sendDocument"),
                content,
                cancellationToken);

            await EnsureSuccessAsync(documentResponse, cancellationToken);
            return;
        }

        var messageResponse = await httpClient.PostAsJsonAsync(
            BuildApiUri("sendMessage"),
            new { chat_id = _options.ChatId, text = BuildMessage(lead), parse_mode = "HTML" },
            cancellationToken);

        await EnsureSuccessAsync(messageResponse, cancellationToken);
    }

    private static string BuildMessage(Lead lead, bool compact = false)
    {
        static string E(string? value) => WebUtility.HtmlEncode(value ?? "—");
        var service = lead.Service is "custom" or "Свой вариант" ? lead.CustomService : ServiceLabel(lead.Service);
        var description = compact ? Truncate(lead.Description, 360) : lead.Description;
        var minskTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(lead.CreatedAtUtc, "Europe/Minsk");

        return $"""
            <b>Новая заявка</b>

            <b>Имя:</b> {E(compact ? Truncate(lead.Name, 80) : lead.Name)}
            <b>Связь:</b> {E(ContactMethodLabel(lead.ContactMethod))} — {E(compact ? Truncate(lead.ContactValue, 120) : lead.ContactValue)}
            <b>Услуга:</b> {E(compact ? Truncate(service, 120) : service)}
            <b>Задача:</b>
            {E(description)}

            <b>Получена:</b> {minskTime:dd.MM.yyyy HH:mm} (Минск)
            """;
    }

    private static string ServiceLabel(string service) => service switch
    {
        "website" => "Сайт или лендинг",
        "tilda-wordpress" => "Tilda / WordPress",
        "store" => "Интернет-магазин",
        "web-service" => "Веб-сервис / личный кабинет",
        "telegram-bot" => "Telegram-бот",
        "crm-automation" => "CRM или автоматизация",
        "ai-agent" => "AI-агент / AI-помощник",
        "integrations" => "Интеграции с сервисами",
        _ => service
    };

    private static string Truncate(string? value, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "—";

        var trimmed = value.Trim();
        return trimmed.Length <= maxLength ? trimmed : $"{trimmed[..(maxLength - 1)]}…";
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
