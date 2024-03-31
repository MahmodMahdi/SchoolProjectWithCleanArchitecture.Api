using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Context;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators
{
	public class UniquePhoneAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			UpdateUserCommand User = (UpdateUserCommand)validationContext.ObjectInstance;
			ApplicationDBContext db = (ApplicationDBContext)validationContext.GetService(typeof(ApplicationDBContext))!;
			string? phoneNumber = value?.ToString();
			User user = db.Users.FirstOrDefault(c => c.PhoneNumber == phoneNumber && c.Id != User.Id)!;
			if (user == null)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("Phone Number already exist");
		}

	}
	public class UniquePhoneNumberAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			AddUserCommand User = (AddUserCommand)validationContext.ObjectInstance;
			ApplicationDBContext db = (ApplicationDBContext)validationContext.GetService(typeof(ApplicationDBContext))!;
			string? phoneNumber = value?.ToString();
			User user = db.Users.FirstOrDefault(c => c.PhoneNumber == phoneNumber && c.Email != User.Email)!;
			if (user == null)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("Phone Number already exist");
		}

	}

}
