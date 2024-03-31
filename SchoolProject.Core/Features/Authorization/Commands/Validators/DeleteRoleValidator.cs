using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
	public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
	{
		#region Fields
		private readonly IAuthorizationService _authorizationService;
		private readonly IStringLocalizer<SharedResources> _localizer;
		#endregion
		#region Constructors
		public DeleteRoleValidator(IStringLocalizer<SharedResources> localizer, IAuthorizationService authorizationService)
		{
			_localizer = localizer;
			_authorizationService = authorizationService;
			ApplyValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.Id)
		   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
		   .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
		}
		#endregion
	}
}
