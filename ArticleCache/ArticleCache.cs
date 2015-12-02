using System;
using System.Collections.Generic;

namespace ArticleCache
{
    public class ArticleCache
    {
        private const int MAX_ARTICLES = 100;

        private readonly Dictionary<Guid, LinkedListNode<Article>> articleLookup;

        private LinkedListNode<Article> head = null;

        private LinkedListNode<Article> tail = null;

        public ArticleCache()
        {
            this.articleLookup = new Dictionary<Guid, LinkedListNode<Article>>();
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
            if (this.articleLookup.Count >= MAX_ARTICLES)
            {
                this.articleLookup.Remove(head.Value.Id);
                head = head.Next;
                head.Previous = null;
            }

            var node = new LinkedListNode<Article>
            {
                Next = null,
                Previous = tail,
                Value = new Article
                {
                    Id = Guid.NewGuid(),
                    Body = articleBody,
                    LastAccessedTime = DateTime.Now
                }
            };

            if (this.Count == 0)
            {
                head = node;
                tail = node;
            }
            else
            {
                tail.Next = node;
                tail = tail.Next;
            }

            this.articleLookup[tail.Value.Id] = tail;
            return tail.Value;
        }

        public Article GetByGuid(Guid id)
        {
            var node = this.articleLookup[id];
            node.Value.LastAccessedTime = DateTime.Now;

            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;

            tail.Next = node;
            node.Previous = tail;
            node.Next = null;

            return node.Value;
        }
    }
}
