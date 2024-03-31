using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators
{
	public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _localizer;
		#endregion
		#region Constructors
		public UpdateUserValidator(IStringLocalizer<SharedResources> localizer)
		{
			_localizer = localizer;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.FirstName)
			   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			   .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
			   .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);

			RuleFor(x => x.LastName)
			   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			   .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
			   .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);


			RuleFor(x => x.Email)
			   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			   .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
		}
		public void ApplyCustomValidationsRules()
		{
		}
		#endregion
	}
}
