using Blog.Web.Application.Service.Entity;

namespace Blog.Web.Application.Service
{
    public interface IThemeService
    {
        Theme Current { get; }
    }
}