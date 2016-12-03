using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Web.Application.Service.Entity
{
    public class Config
    {
        private readonly bool isSqlRepositoryType = ContainerConfig.JsonRepositoryType.Equals("sql", StringComparison.InvariantCultureIgnoreCase);

        public string AdminsCsv { get; set; }

        [JsonProperty("Admins")]
        private List<string> _admins;

        [JsonIgnore]
        public List<string> Admins
        {
            get
            {
                if (!isSqlRepositoryType)
                {
                    return _admins;
                }
                else
                {
                    return AdminsCsv.Split(',').ToList();
                }
            }
            set
            {
                _admins = value;
            }
        }

        public int ContactFormId { get; set; }

        public ContactFormConfig ContactForm { get; set; }

        public string Crossbar { get; set; }

        public int DisqusId { get; set; }

        public DisqusConfig Disqus { get; set; }

        public string GoogleAnalyticsId { get; set; }

        public string Heading { get; set; }

        public string MetaDescription { get; set; }

        public string Site { get; set; }

        public string Tagline { get; set; }

        public string Theme { get; set; }

        public string Title { get; set; }

        public string TwitterUsername { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public class ContactFormConfig
        {
            public string RecipientEmail { get; set; }

            public string RecipientName { get; set; }

            public string Subject { get; set; }
        }

        public class DisqusConfig
        {
            public bool DevelopmentMode { get; set; }

            public string Shortname { get; set; }
        }
    }
}