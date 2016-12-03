using Blog.Web.Application;
using Blog.Web.Application.Infrastructure;

namespace Blog.Web.Controllers
{
    public partial class ErrorController
    {
        public class ErrorModel : LayoutModel
        {
            public string Heading { get; set; }
            public string Message { get; set; }
        }
    }
}