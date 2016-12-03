using Blog.Web.Application.Service.Entity;
using System.Collections.Generic;

namespace Blog.Web.Application.Service
{
	public interface IAboutService
	{
		List<About> GetAll();
		void Save(About about);
		About GetByTitle(string title);
		bool Exists(string title);
		void Delete(string title);
	}
}
