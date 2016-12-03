using System.IO;
namespace Blog.Web.Application.Service
{
    public interface IServices
    {
        IEntryService Entry { get; }
        IUserService User { get; }
        IConfigService Config { get; }
        IMessageService Message { get; }
        IThemeService Theme { get; }
        IAboutService About { get; }
        IHomeService Home { get; }
        IImageService Image { get; }
    }
}