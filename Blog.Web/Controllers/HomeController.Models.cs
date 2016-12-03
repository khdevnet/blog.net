using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Web.Application;
using Blog.Web.Application.Infrastructure;

namespace Blog.Web.Controllers
{
    public partial class HomeController
    {
        public class IndexModel : LayoutModel
        {
            public IEnumerable<EntrySummaryModel> Entries { get; set; }
        }

        public class EntrySummaryModel
        {
            public string Key { get; set; }
            public string Title { get; set; }
            public string Date{ get; set; }
            public string PrettyDate { get; set; }
            public bool IsPublished { get; set; }
        }
    }
}