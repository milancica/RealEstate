using System;
using System.Collections.Generic;
using System.Linq;
using RealEstate.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RealEstate.Repository.DbSeed
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder appBuilder)
        {
            DatabaseContext _context = appBuilder
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<DatabaseContext>();

            if (_context.Database.GetPendingMigrations().Any())
                _context.Database.Migrate();


            var anyArticles = _context.Articles.Any();

            if (!anyArticles)
            {
                var ArticleTypes = GetMainArticleTypes();
                _context.ArticleTypes.AddRange(ArticleTypes);

                _context.SaveChanges();

                var ArticlesWithArticleTypes = GetArticles();

                var Articles = ArticlesWithArticleTypes.Select(x => x.Key)
                    .ToList();

                _context.Articles.AddRange(Articles);


                foreach (var Article in ArticlesWithArticleTypes.Keys)
                {
                    var ArticleArticleType = ArticlesWithArticleTypes
                        .Where(x => x.Key == Article)
                        .FirstOrDefault().Value ?? "";

                    Article.ArticleTypeId = _context.ArticleTypes.FirstOrDefault(x => x.Name == ArticleArticleType).ArticleTypeId;
                }

                _context.SaveChanges();
            }
        }

        static List<string> GetTableNames(this DatabaseContext context)
        {
            return new List<string>()
            {
                "Articles", "ArticleTypes", "ArticleArticleTypes"
            };
        }

        static Dictionary<Article, string> GetArticles()
        {
            return new Dictionary<Article, string>()
            {
                {
                    new Article()
                    {
                        Name = "popuniti",
                        Description = "popuniti",
                        Price = GetRandomPrice()
                    }, 
                    GetRandomArticleType()
                },
                {
                    new Article()
                    {
                        Name = "popuniti",
                        Description = "popuniti",
                        Price = GetRandomPrice()
                    }, 
                    GetRandomArticleType()
                },
                {
                    new Article()
                    {
                        Name = "popuniti",
                        Description = "popuniti",
                        Price = GetRandomPrice()
                    }, 
                    GetRandomArticleType()
                },
                {
                    new Article()
                    {
                        Name = "popuniti",
                        Description = "popuniti",
                        Price = GetRandomPrice()
                    }, 
                    GetRandomArticleType()
                },
                {
                    new Article()
                    {
                        Name = "popuniti",
                        Description = "popuniti",
                        Price = GetRandomPrice()
                    }, 
                    GetRandomArticleType()
                }
            };
        }

        private static string GetRandomArticleType()
        {
            var random = new Random();
            var ArticleTypes = GetMainArticleTypes();
            var ArticleTypesCount = ArticleTypes.Count;
            int numRandomArticleType = random.Next(1, ArticleTypesCount);

            return ArticleTypes[numRandomArticleType].Name;
        }

        private static decimal GetRandomPrice()
        {
            var random = new Random();
            var basePrice = (decimal)(random.Next(6, 75) * 1.0);
            var decimalPrice = (decimal)(random.NextDouble() * 99);
            return basePrice + decimalPrice;
        }

        static List<ArticleType> GetMainArticleTypes()
        {
            return new List<ArticleType>()
            {
                new ArticleType() { Name = "Fantasy", Description = "Fantasy" },
                new ArticleType() { Name = "Action", Description = "Action" },
                new ArticleType() { Name = "Mystery", Description = "Mystery" },
                new ArticleType() { Name = "Sci-Fi", Description = "Science fiction" },
            };
        }
    }
}
