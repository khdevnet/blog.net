using Blog.Web.Application.Service.Entity;
using Blog.Web.Application.Storage;

namespace Blog.Web.Application.Service.Internal
{
    public class ConfigService : IConfigService
    {
        private readonly IRepository _repository;

        public ConfigService(IRepository repository)
        {
            _repository = repository;
        }

        public Config Current { get { return _repository.Single<Config>("settings"); } }
    }
}