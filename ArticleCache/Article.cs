using System;

namespace ArticleCache
{
    public class Article
    {
        public Article()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public string Body { get; set; }

        public DateTime LastAccessedTime { get; set; }
    }
}
