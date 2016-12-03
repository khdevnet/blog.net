using Blog.Web.Application.Service.Entity;

namespace Blog.Web.Application.Service
{
    public interface IConfigService
    {
        Config Current { get; }
    }
}