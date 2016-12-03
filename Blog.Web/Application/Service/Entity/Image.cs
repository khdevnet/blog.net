using System.IO;

namespace Blog.Web.Application.Service.Entity
{
	public class Image
	{
		public Stream StreamToUpload { get; set; }
		public string Uri { get; set; }
		public string FileName { get; set; }
	}
}