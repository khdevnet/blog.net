using System;
using Blog.Web.Application.Service.Entity;

namespace Blog.Web.Application.Service.Internal
{
    public class ThemeService : IThemeService
    {
        private readonly IConfigService _configService;

        public ThemeService(IConfigService configService)
        {
            _configService = configService;
        }

        public Theme Current
        {
            get
            {
                var themeBasePath = String.Format("~/Themes/{0}", _configService.Current.Theme);
                return new Theme(_configService.Current.Theme, themeBasePath);
            }
        }
    }
} 