using MailKit.Net.Smtp;
using MimeKit;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
	public class EmailService : IEmailService
	{
		#region Fields
		private readonly EmailSettings _emailSettings;
		#endregion

		#region  Constructor
		public EmailService(EmailSettings emailSettings)
		{
			_emailSettings = emailSettings;
		}
		#endregion

		#region  Handle Functions
		public async Task<string> SendEmailAsync(string email, string Message, string title)
		{
			try
			{
				//sending the Message of passwordResetLink
				using (var client = new SmtpClient())
				{
					await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
					client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);
					var bodybuilder = new BodyBuilder
					{
						HtmlBody = $"{Message}",
						TextBody = "Welcome",
					};
					var message = new MimeMessage
					{
						Body = bodybuilder.ToMessageBody()
					};
					message.From.Add(new MailboxAddress("Mahmoud", _emailSettings.FromEmail));
					message.To.Add(new MailboxAddress("Confirm", email));
					message.Subject = title == null ? "Not Submitted" : title;
					await client.SendAsync(message);
					await client.DisconnectAsync(true);
				}
				//end of sending email
				return "Success";

			}
			catch (Exception)
			{
				return "Failed";
			}
		}
		#endregion
	}
}
