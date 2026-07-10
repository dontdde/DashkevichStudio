using DashkevichStudio.Api.Contracts;
using DashkevichStudio.Application.Leads;
using DashkevichStudio.Domain.Leads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DashkevichStudio.Api.Controllers;

[ApiController]
[Route("api/v1/leads")]
public sealed class LeadsController(ILeadSubmissionService leadSubmissionService) : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, ContactMethod> ContactMethods =
        new Dictionary<string, ContactMethod>(StringComparer.OrdinalIgnoreCase)
        {
            ["telegram"] = ContactMethod.Telegram,
            ["instagram"] = ContactMethod.Instagram,
            ["email"] = ContactMethod.Email,
            ["phone"] = ContactMethod.Phone,
            ["custom"] = ContactMethod.Custom
        };

    [HttpPost]
    [Consumes("multipart/form-data")]
    [EnableRateLimiting("LeadSubmission")]
    [ProducesResponseType<CreateLeadResponse>(StatusCodes.Status202Accepted)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromForm] CreateLeadRequest request,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.Company))
            return Accepted(new CreateLeadResponse(Guid.Empty, "Заявка принята."));

        if (!request.PersonalDataConsent)
            return BadRequest(new ProblemDetails
            {
                Title = "Проверьте данные формы.",
                Status = StatusCodes.Status400BadRequest,
                Extensions =
                {
                    ["errors"] = new Dictionary<string, string[]>
                    {
                        ["personalDataConsent"] = ["Необходимо согласие на обработку персональных данных."]
                    }
                }
            });

        if (!ContactMethods.TryGetValue(request.ContactMethod, out var contactMethod))
            return BadRequest(new ProblemDetails
            {
                Title = "Проверьте данные формы.",
                Status = StatusCodes.Status400BadRequest,
                Extensions =
                {
                    ["errors"] = new Dictionary<string, string[]>
                    {
                        ["contactMethod"] = ["Выберите доступный способ связи."]
                    }
                }
            });

        LeadFile? file = null;
        if (request.File is not null)
        {
            file = new LeadFile(
                request.File.OpenReadStream(),
                request.File.FileName,
                request.File.ContentType,
                request.File.Length);
        }

        var command = new CreateLeadCommand(
            request.Name,
            contactMethod,
            request.ContactValue,
            request.Service,
            request.CustomService,
            request.Description,
            request.SourcePage,
            request.UtmSource,
            request.UtmMedium,
            request.UtmCampaign,
            file);

        var id = await leadSubmissionService.CreateAsync(command, cancellationToken);
        return Accepted(new CreateLeadResponse(id, "Заявка отправлена. Мы скоро свяжемся с вами."));
    }
}
