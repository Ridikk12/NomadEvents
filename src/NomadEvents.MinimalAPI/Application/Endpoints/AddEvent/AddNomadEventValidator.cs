using FluentValidation;

namespace NomadEvents.MinimalAPI.Application.Endpoints;

public class AddNomadEventValidator : AbstractValidator<AddNomadEventRequest>
{
    public AddNomadEventValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).Length(150).NotEmpty();
        RuleFor(x => x.EventDate).NotEmpty();
        RuleFor(x => x.Price).NotNull();
        RuleFor(x => x.Url).NotEmpty();
    }
}