using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Web.Application;
using Blog.Web.Application.Infrastructure;
using Blog.Web.Application.Service;

namespace Blog.Web.Controllers
{
    public partial class SearchController : LayoutController
    {
        public SearchController(IServices services) : base(services) { }

        [HttpGet]
        public ActionResult Index(string q)
        {
            var entries = Services.Entry.GetList();
            var results =
                entries
                    .Where(e => e.Title.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0)
                    .Select(e => new SearchResultModel { Slug = e.Slug, Title = e.Title, Date = e.DateCreated.ToDateString()});

            var model = new IndexModel { Results = results, QueryText = q };
            return View(model);
        }
    }
}
