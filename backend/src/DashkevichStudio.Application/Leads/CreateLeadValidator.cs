namespace DashkevichStudio.Application.Leads;

public static class CreateLeadValidator
{
    public const long MaxFileSize = 10 * 1024 * 1024;

    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".pdf", ".doc", ".docx", ".txt", ".rtf", ".png", ".jpg", ".jpeg", ".webp", ".zip"
    };

    public static void Validate(CreateLeadCommand command)
    {
        var errors = new Dictionary<string, string[]>();

        CheckRequired(errors, "name", command.Name, 100);
        CheckRequired(errors, "contactValue", command.ContactValue, 200);
        CheckRequired(errors, "service", command.Service, 150);
        CheckRequired(errors, "description", command.Description, 3000);

        if (command.CustomService?.Length > 200)
            errors["customService"] = ["Свой вариант услуги не должен превышать 200 символов."];

        if (command.File is not null)
        {
            var extension = Path.GetExtension(command.File.FileName);
            if (command.File.Length <= 0 || command.File.Length > MaxFileSize)
                errors["file"] = ["Размер файла должен быть не больше 10 МБ."];
            else if (!AllowedExtensions.Contains(extension))
                errors["file"] = ["Допустимы PDF, DOC, DOCX, TXT, RTF, PNG, JPG, WEBP и ZIP."];
        }

        if (errors.Count > 0)
            throw new LeadValidationException(errors);
    }

    private static void CheckRequired(
        IDictionary<string, string[]> errors,
        string key,
        string value,
        int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            errors[key] = ["Поле обязательно."];
        else if (value.Length > maxLength)
            errors[key] = [$"Максимальная длина: {maxLength} символов."];
    }
}
