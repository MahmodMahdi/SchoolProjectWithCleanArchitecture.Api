
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Responses;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
	public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaimsResponse>>
	{
		public int UserId { get; set; }
		public ManageUserClaimsQuery(int id)
		{
			UserId = id;
		}
	}
}
