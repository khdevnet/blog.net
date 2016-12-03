using Blog.Web.Application.Service.Entity;
using System.Collections.Generic;

namespace Blog.Web.Application.Service
{
	public interface IHomeService
	{
		List<Home> GetAll();
        void Save(Home home);
        Home GetByTitle(string title);
		bool Exists(string title);
		void Delete(string title);
	}
}
