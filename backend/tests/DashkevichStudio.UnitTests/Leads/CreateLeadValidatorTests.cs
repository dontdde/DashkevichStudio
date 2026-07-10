using DashkevichStudio.Application.Leads;
using DashkevichStudio.Domain.Leads;

namespace DashkevichStudio.UnitTests.Leads;

public sealed class CreateLeadValidatorTests
{
    [Fact]
    public void Validate_WithValidCommand_DoesNotThrow()
    {
        var command = ValidCommand();

        var exception = Record.Exception(() => CreateLeadValidator.Validate(command));

        Assert.Null(exception);
    }

    [Fact]
    public void Validate_WithEmptyContact_ReturnsFieldError()
    {
        var command = ValidCommand() with { ContactValue = "" };

        var exception = Assert.Throws<LeadValidationException>(() =>
            CreateLeadValidator.Validate(command));

        Assert.Contains("contactValue", exception.Errors.Keys);
    }

    [Fact]
    public void Validate_WithOversizedFile_ReturnsFileError()
    {
        var file = new LeadFile(
            Stream.Null,
            "brief.pdf",
            "application/pdf",
            CreateLeadValidator.MaxFileSize + 1);
        var command = ValidCommand() with { File = file };

        var exception = Assert.Throws<LeadValidationException>(() =>
            CreateLeadValidator.Validate(command));

        Assert.Contains("file", exception.Errors.Keys);
    }

    private static CreateLeadCommand ValidCommand() => new(
        "Ян",
        ContactMethod.Telegram,
        "@dashkevich_studio",
        "Сайт или лендинг",
        null,
        "Нужно разработать сайт компании.",
        "/",
        null,
        null,
        null,
        null);
}
