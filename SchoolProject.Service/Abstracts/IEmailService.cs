namespace SchoolProject.Service.Abstracts
{
	public interface IEmailService
	{
		public Task<string> SendEmailAsync(string email, string message, string title);
	}
}
