using Blog.Web.Application.Service.Entity;

namespace Blog.Web.Application.Service
{
    public interface IImageService
    {
        void Save(Image stream);
        Image GetByFileName(string fileName);
        void Delete(string fileName);
    }
}