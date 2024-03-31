using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Departments.Commands.Validator
{
	public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
	{
		#region Fields
		private readonly IDepartmentService _departmentService;
		private readonly IStringLocalizer<SharedResources> _localizer;
		#endregion
		#region Constructors
		public UpdateDepartmentValidator(IDepartmentService departmentService, IStringLocalizer<SharedResources> localizer)
		{
			_departmentService = departmentService;
			_localizer = localizer;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.DepartmentNameEn)
				.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
				.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);
			RuleFor(x => x.DepartmentNameAr)
			.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
			.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);
		}
		public void ApplyCustomValidationsRules()
		{
			RuleFor(x => x.DepartmentNameAr)
				.MustAsync(async (model, Key, CancellationToken) => !await _departmentService.IsNameExcludeSelf(Key!, model.DepartmentId))
				.WithMessage(_localizer[SharedResourcesKeys.IsExist]);

			RuleFor(x => x.DepartmentNameEn)
				.MustAsync(async (model, Key, CancellationToken) => !await _departmentService.IsNameExcludeSelf(Key!, model.DepartmentId))
				.WithMessage(_localizer[SharedResourcesKeys.IsExist]);
			RuleFor(x => x.DepartmentManager)
				.MustAsync(async (model, Key, CancellationToken) => !await _departmentService.IsManagerExcludeSelf(Key, model.DepartmentId))
				.WithMessage(_localizer[SharedResourcesKeys.IsExist]);
		}
		#endregion
	}

}
