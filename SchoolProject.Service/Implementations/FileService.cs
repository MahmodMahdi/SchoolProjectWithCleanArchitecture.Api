using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
	public class FileService : IFileService
	{
		#region Fields
		private readonly IWebHostEnvironment _webHost;
		#endregion

		#region Constructor
		public FileService(IWebHostEnvironment webHost)
		{
			_webHost = webHost;
		}
		#endregion

		#region Handle Functions
		public async Task<string> UploadImage(string location, IFormFile file)
		{
			var path = _webHost.WebRootPath + "/" + location + "/";
			var extension = Path.GetExtension(file.FileName);
			var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extension;
			if (file.Length > 0)
			{
				try
				{
					if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
					using (FileStream fileStream = File.Create(path + fileName))
					{
						await file.CopyToAsync(fileStream);
						await fileStream.FlushAsync();
						return $"/{location}/{fileName}";
					}
				}
				catch (Exception)
				{
					return "FailedToUploadImage";
				}
			}
			else { return "NoImage"; }
		}
		#endregion
	}
}
