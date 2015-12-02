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

        public Article CreateArticle(string articleBody)
        {
            var article = new Article
            {
                Id = Guid.NewGuid(),
                Body = articleBody,
                LastAccessedTime = DateTime.Now
            };

            if (this.articleLookup.Count >= MAX_ARTICLES)
            {
                this.articleLookup.Remove(this.sortedArticles.First.Value.Id);
                this.sortedArticles.RemoveFirst();
            }

            this.sortedArticles.AddLast(article);
            this.articleLookup[article.Id] = this.sortedArticles.Last;
            return article;
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
