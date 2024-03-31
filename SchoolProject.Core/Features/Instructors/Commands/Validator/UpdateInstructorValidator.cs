using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Instructors.Commands.Validator
{
	public class UpdateInstructorValidator : AbstractValidator<UpdateInstructorCommand>
	{
		#region Fields
		private readonly IInstructorService _instructorService;
		private readonly IDepartmentService _departmentService;
		private readonly IStringLocalizer<SharedResources> _localizer;

		#endregion
		#region Constructors
		public UpdateInstructorValidator(IInstructorService instructorService,
			IStringLocalizer<SharedResources> localizer,
			IDepartmentService departmentService)
		{
			_instructorService = instructorService;
			_localizer = localizer;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
			_departmentService = departmentService;
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.InstructorNameEn)
				.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
				.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);
			RuleFor(x => x.InstructorNameAr)
			.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
			.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);
		}
		public void ApplyCustomValidationsRules()
		{
			RuleFor(x => x.Phone)
				.MustAsync(async (model, Key, CancellationToken) => !await _instructorService.IsPhoneExcludeSelf(Key!, model.InstructorId))
				.WithMessage(_localizer[SharedResourcesKeys.IsExist]);

			RuleFor(x => x.SuperVisorId)
				.MustAsync(async (Key, CancellationToken) => await _instructorService.IsSupervisorIdExist(Key!))
				.WithMessage(_localizer[SharedResourcesKeys.IsNotExist]);

			RuleFor(x => x.DepartmentId)
				.MustAsync(async (Key, CancellationToken) => await _departmentService.IsDepartmentIdExist(Key!))
				.WithMessage(_localizer[SharedResourcesKeys.IsNotExist]);
		}
		#endregion
	}
}
