using System;
using System.Linq;

namespace ArticleCache
{
    public class Program
    {
        static void Main(string[] args)
        {
            var cache = new ArticleCache();

            foreach(var num in Enumerable.Range(0, 200))
            {
                cache.AddArticle(new Article
                {
                    Body = string.Format("Article {0}", num),
                    LastAccessedTime = DateTime.Now
                });
            }

            Console.WriteLine(cache.Count);
        }
    }
}
