using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.BookServices.QueryObjects
{
    
    public static class BookListDtoSelect
    {

        public static IQueryable<BookListDTO>             //#A
            MapBookToDto(this IQueryable<Book> books)     //#A
        {
            return books.Select<Book, BookListDTO>(book => new BookListDTO
            {
               BookId = book.BookId,
               Title = book.Title,
               PublishedOn = book.PublishedOn,
               Price = book.Promotion==null ? book.Price: book.Promotion.NewPrice,
               ActualPrice = book.Price,
               PromotionPromotionalText = book.Promotion==null? "": book.Promotion.PromotionalText,
               ReviewsCount = book.Reviews.Count, 
               ReviewsAverageVotes = book.Reviews.Select(y =>              //#G
                        (double?)y.NumStar).Average(),
               AuthorsOrdered = String.Join(", ",
               book.AuthorLink.
               OrderBy(p=>p.Order).
               Select(n=>n.Author.Name))
    });

        }
    }
}
