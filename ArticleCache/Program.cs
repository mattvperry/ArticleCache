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
                cache.CreateArticle(string.Format("Article {0}", num));
            }

            Console.WriteLine(cache.Count);
        }
    }
}
