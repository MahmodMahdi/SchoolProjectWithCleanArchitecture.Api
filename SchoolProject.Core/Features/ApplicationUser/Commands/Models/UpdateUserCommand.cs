using MediatR;
using SchoolProject.Core.Bases;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Models;
public class UpdateUserCommand : IRequest<Response<string>>
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    //public string? UserName { get; set; }
    [RegularExpression("[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{3}", ErrorMessage = "Enter valid Email")]
    public string? Email { get; set; }
    [RegularExpression("01[0125][0-9]{8}", ErrorMessage = "Enter Valid Phone Number.")]
    // [UniquePhone]
    public string? Phone { get; set; }
    [RegularExpression("(Alex|Cairo|Tanta|Santa)")]
    public string? Address { get; set; }
}

