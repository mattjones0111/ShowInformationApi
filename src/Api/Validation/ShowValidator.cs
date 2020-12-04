using System.Linq;
using Api.Models;
using FluentValidation;

namespace Api.Validation
{
    // ReSharper disable once UnusedMember.Global - bound dynamically
    public class ShowValidator : AbstractValidator<Show>
    {
        public ShowValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleForEach(x => x.Cast)
                .SetValidator(new CastMemberValidator())
                .When(x => x.Cast != null && x.Cast.Any());
        }
    }
}
