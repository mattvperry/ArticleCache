using System;
using System.Collections.Generic;

namespace ArticleCache
{
    public class ArticleCache
    {
        private const int MAX_ARTICLES = 100;

        private readonly Dictionary<Guid, LinkedListNode<Article>> articleLookup;

        private readonly LinkedList<Article> sortedArticles;

        public ArticleCache()
        {
            this.articleLookup = new Dictionary<Guid, LinkedListNode<Article>>();
            this.sortedArticles = new LinkedList<Article>();
        }

        public int Count
        {
            get
            {
                return this.articleLookup.Count;
            }
        }

        public void AddArticle(Article article)
        {
            if (this.articleLookup.Count >= MAX_ARTICLES)
            {
                this.articleLookup.Remove(this.sortedArticles.First.Value.Id);
                this.sortedArticles.RemoveFirst();
            }

            // Here we make the assumption that article.LastAccessedTime will always be DateTime.Now
            // when adding to the cache
            this.sortedArticles.AddLast(article);
            this.articleLookup[article.Id] = this.sortedArticles.Last;
        }

        public Article GetByGuid(Guid id)
        {
            var node = this.articleLookup[id];
            node.Value.LastAccessedTime = DateTime.Now;

            this.sortedArticles.Remove(node);
            this.sortedArticles.AddLast(node);

            return node.Value;
        }
    }
}
