using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Departments.Commands.Validator
{
	public class AddDepartmentValidator : AbstractValidator<AddDepartmentCommand>
	{
		#region Fields
		private readonly IDepartmentService _departmentService;
		private readonly IInstructorService _instructorService;
		private readonly IStringLocalizer<SharedResources> _localizer;
		#endregion
		#region Constructors
		public AddDepartmentValidator(IDepartmentService departmentService,
			IStringLocalizer<SharedResources> localizer,
			IInstructorService instructorService)
		{
			_departmentService = departmentService;
			_localizer = localizer;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
			_instructorService = instructorService;
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
			RuleFor(x => x.DepartmentNameEn)
				.MustAsync(async (Key, CancellationToken) => !await _departmentService.IsNameExist(Key!))
				.WithMessage(_localizer[SharedResourcesKeys.IsExist]);

			RuleFor(x => x.DepartmentNameAr)
				.MustAsync(async (Key, CancellationToken) => !await _departmentService.IsNameExist(Key!))
				.WithMessage(_localizer[SharedResourcesKeys.IsExist]);

			RuleFor(x => x.DepartmentManager)
				.MustAsync(async (Key, CancellationToken) => await _instructorService.IsDepartmentManagerIdExist(Key!))
				.WithMessage(_localizer[SharedResourcesKeys.IsNotExist]);
		}
		#endregion
	}
}
