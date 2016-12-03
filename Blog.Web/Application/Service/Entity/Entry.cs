using System;

namespace Blog.Web.Application.Service.Entity
{
    public class Entry
    {
        public int Id { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime DateCreated { get; set; }

        public string Markdown { get; set; }

        public bool? IsPublished { get; set; }

        public bool? IsCodePrettified { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}