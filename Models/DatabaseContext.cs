﻿using Microsoft.EntityFrameworkCore;

namespace RealEstate.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            // Leaving empty on purpose
        }

        public DbSet<ArticleType> ArticleTypes { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
