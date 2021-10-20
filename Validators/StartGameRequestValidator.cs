using FluentValidation;
using jdobson_pairs.Requests;

namespace jdobson_pairs.Validators
{
    public class StartGameRequestValidator: AbstractValidator<StartGameRequest>
    {
        public StartGameRequestValidator() { 
            // I could have asked for a numbr of pairs, then this validation would be moot, but I wanted an example
            // of fluent validation in here.
            RuleFor(c => c.NumCards).Must(x => x % 2 == 0).WithMessage("Must request an even number of cards");
        }
    }
}
