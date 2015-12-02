using System;

namespace ArticleCache
{
    public class Article
    {
        public Guid Id { get; set; }

        public string Body { get; set; }

        public DateTime LastAccessedTime { get; set; }
    }
}
