using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Subjects.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Subjects.Commands.Validator
{
	public class AddSubjectValidator : AbstractValidator<AddSubjectCommand>
	{
		#region Fields
		private readonly ISubjectService _subjectService;
		private readonly IStringLocalizer<SharedResources> _localizer;
		#endregion
		#region Constructors
		public AddSubjectValidator(ISubjectService subjectService, IStringLocalizer<SharedResources> localizer)
		{
			_subjectService = subjectService;
			_localizer = localizer;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.NameAr)
				.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
				.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);
			RuleFor(x => x.NameEn)
			.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
			.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);
		}
		public void ApplyCustomValidationsRules()
		{
			RuleFor(x => x.NameAr)
				.MustAsync(async (Key, CancellationToken) => !await _subjectService.IsNameExist(Key!))
				.WithMessage(_localizer[SharedResourcesKeys.IsExist]);
			RuleFor(x => x.NameEn)
				.MustAsync(async (Key, CancellationToken) => !await _subjectService.IsNameExist(Key!))
				.WithMessage(_localizer[SharedResourcesKeys.IsExist]);
		}
		#endregion
	}
}
