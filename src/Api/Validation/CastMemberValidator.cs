using Api.Models;
using FluentValidation;

namespace Api.Validation
{
    public class CastMemberValidator : AbstractValidator<CastMember>
    {
        public CastMemberValidator()
        {
            RuleFor(x => x.Name)
                .NotNull();
        }
    }
}
