using Blog.Web.Application.Service.Entity;
using Blog.Web.Application.Storage;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Web.Application.Service.Internal
{
	public class HomeService : IHomeService
	{
		private readonly IRepository _repository;

        public HomeService(IRepository repository)
		{
			_repository = repository;
		}

        public List<Home> GetAll()
		{
			return _repository.All<Home>().ToList();
		}

        public void Save(Home about)
		{
            _repository.Save<Home>(about);
		}

        public Home GetByTitle(string title)
		{
            return _repository.Single<Home>(title);
		}

		public bool Exists(string title)
		{
            return _repository.Exists<Home>(title);
		}

		public void Delete(string title)
		{
            _repository.Delete<Home>(title);
		}
	}
}