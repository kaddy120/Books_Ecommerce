using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Database
{
    public class EcommerceContext: DbContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> option): base(option)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors{ get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BookAuthor> AuthorLink { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasKey(c => new {c.BookId, c.AuthorId});
            modelBuilder.Entity<Book>().HasQueryFilter(p=>!p.SoftDeleted);
        }
    }
}
