using ImageResizer;
using MarkdownSharp;
using Blog.Web.Application.Infrastructure;
using Blog.Web.Application.Service;
using Blog.Web.Application.Service.Entity;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public partial class AboutController : LayoutController
    {
        public AboutController(IServices services) : base(services) { }

        // GET: About
        [HttpGet]
        public ActionResult Index()
        {
            var model = new ShowModel();
            var about = Services.About.GetAll().FirstOrDefault();
            if (about != null)
            {
                model.Name = about.Name;
                model.Title = about.Title;
                model.ImageUrl = about.ImageUrl;
                model.Content = new Markdown().Transform(about.Content);
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(string title)
        {
            var model = new EditModel();
            var about = !string.IsNullOrEmpty(title) ? Services.About.GetByTitle(title) : Services.About.GetAll().FirstOrDefault();
            if (about != null)
            {
                model.Title = about.Title;
                model.Content = about.Content;
                model.Name = about.Name;
            }

            return View(model);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(EditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Get the About entity if it exists, so ImageUrl doesn't get overriden to null if no image is selected and user is saving in json instead of sql.
            var about = Services.About.Exists(model.Title) ? Services.About.GetByTitle(model.Title) : new About();
            about.Name = model.Name;
            about.Title = model.Title;
            about.Content = model.Content;
            // Save the image in blob storage
            if (model.Image != null)
            {
                var image = new Blog.Web.Application.Service.Entity.Image();
                var fileName = Path.GetFileName(model.Image.FileName);
                // Scale the image before saving
                using (var scaledImageStream = new MemoryStream())
                {
                    var settings = new ResizeSettings(200, 150, FitMode.None, "jpg");
                    ImageBuilder.Current.Build(model.Image.InputStream, scaledImageStream, settings);
                    image.StreamToUpload = scaledImageStream;
                    // Set FileName to save as, gets read as a repository key
                    image.FileName = fileName;
                    Services.Image.Save(image);
                }
                // Get the url to link to the About Entity
                about.ImageUrl = Services.Image.GetByFileName(fileName).Uri;
            }
            Services.About.Save(about);

            return RedirectToAction("Index", "About");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete(string title)
        {
            return View(new DeleteModel() { Title = title });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteModel model)
        {
            var imageUrl = Services.About.GetByTitle(model.Title).ImageUrl;
            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                Uri imageUri = new Uri(imageUrl);
                Services.Image.Delete(Path.GetFileName(imageUri.LocalPath));
            }
            Services.About.Delete(model.Title);
            return RedirectToAction("Index", "About");
        }
    }
}