namespace Blog.Web.Application.Service.Internal
{
    public class Services : IServices
    {
        public Services(
            IEntryService entryService,
            IUserService userService,
            IConfigService configService,
            IMessageService messageService,
            IThemeService themeService,
            IAboutService aboutService,
            IHomeService homeService,
            IImageService imageService)
        {
            Entry = entryService;
            User = userService;
            Config = configService;
            Message = messageService;
            Theme = themeService;
            About = aboutService;
            Home = homeService;//TODO inject
            Image = imageService;
        }

        public IEntryService Entry { get; private set; }
        public IUserService User { get; private set; }
        public IConfigService Config { get; private set; }
        public IMessageService Message { get; private set; }
        public IThemeService Theme { get; private set; }
        public IAboutService About { get; private set; }
        public IImageService Image { get; private set; }


        public IHomeService Home { get; private set; }
    }
}