using DataLayer.Database;
using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AdminServices.Concrete
{
    public class AddReviewService
    {
        private readonly EcommerceContext _context;

        public string BookTitle { get; private set; }

        public AddReviewService(EcommerceContext context)
        {
            _context = context;
        }

        public Review GetReview(int id)
        {
            BookTitle = _context.Books.
                Where(b => b.BookId == id).
                Select(b => b.Title).
                Single();

            return new Review
            {
                BookId = id
            };
            
        }

        public Book Update(Review review)
        {
            var Book = _context.Books.
                Include(b => b.Reviews).Single(b => b.BookId == review.BookId);
            Book.Reviews.Add(review);
            _context.SaveChanges();
            return Book;
        }
        
    }
}
