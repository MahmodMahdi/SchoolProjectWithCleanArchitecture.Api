using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validators
{
	public class AddStudentValidator : AbstractValidator<AddStudentCommand>
	{
		#region Fields
		private readonly IStudentService _studentService;
		private readonly IStringLocalizer<SharedResources> _localizer;
		private readonly IDepartmentService _departmentService;
		#endregion
		#region Constructors
		public AddStudentValidator(IStudentService studentService
			, IStringLocalizer<SharedResources> localizer,
			IDepartmentService departmentService)
		{
			_studentService = studentService;
			_localizer = localizer;
			_departmentService = departmentService;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.NameEn)
				.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
				.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);

			RuleFor(x => x.NameAr)
			.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
			.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);

			RuleFor(x => x.Address)
				.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
				.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);

			RuleFor(x => x.DepartmentId)
			.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
		}
		public void ApplyCustomValidationsRules()
		{
			//RuleFor(x => x.NameEn)
			//	.MustAsync(async (Key, CancellationToken) => !await _studentService.IsNameExist(Key!))
			//	.WithMessage(_localizer[SharedResourcesKeys.IsExist]);
			RuleFor(x => x.Phone)
				.MustAsync(async (Key, CancellationToken) => !await _studentService.IsPhoneExist(Key!))
				.WithMessage(_localizer[SharedResourcesKeys.IsExist]);

			RuleFor(x => x.DepartmentId)
				.MustAsync(async (Key, CancellationToken) => await _departmentService.IsDepartmentIdExist(Key!))
				.WithMessage(_localizer[SharedResourcesKeys.IsNotExist]);
		}
		#endregion
	}
}
