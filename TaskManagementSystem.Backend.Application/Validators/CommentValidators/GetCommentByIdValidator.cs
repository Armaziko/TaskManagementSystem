using FluentValidation;
using TaskManagementSystem.Backend.Application.Queries.CommentQueries;

namespace TaskManagementSystem.Backend.Application.Validators.CommentValidators
{
    public class GetCommentByIdValidator : AbstractValidator<GetCommentByIdQuery>
    {
        public GetCommentByIdValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}