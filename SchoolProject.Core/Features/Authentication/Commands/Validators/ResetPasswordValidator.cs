using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Authentication.Commands.Validators
{
	public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _localizer;
		#endregion
		#region Constructors
		public ResetPasswordValidator(IStringLocalizer<SharedResources> localizer)
		{
			_localizer = localizer;
			ApplyValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.Email)
			   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			   .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
			RuleFor(x => x.Password)
			   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			   .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

			RuleFor(x => x.ConfirmPassword)
			   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			   .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);



		}
		#endregion
	}
}
