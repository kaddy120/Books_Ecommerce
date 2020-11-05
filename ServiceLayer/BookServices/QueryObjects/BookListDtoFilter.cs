using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.BookServices.QueryObjects
{
    public enum BooksFilterBy
    {
        [Display(Name = "All")]
        NoFilter = 0,
        [Display(Name = "By Votes...")]
        ByVotes,
        [Display(Name = "By Year published...")]
        ByPublicationYear
    }

    //for IQuuery...the class should be static and the method should also be static

    public static class BookListDtoFilter
    {
        public const string AllBooksNotPublishedString = "Coming Soon";

        public static IQueryable<BookListDTO> FilterBooksBy(
            this IQueryable<BookListDTO> books,
            BooksFilterBy filterBy, string filterValue)         //#A
        {
            if (string.IsNullOrEmpty(filterValue))              //#B
                return books;                                   //#B

            switch (filterBy)
            {
                case BooksFilterBy.NoFilter:                    //#C
                    return books;                               //#C
                case BooksFilterBy.ByVotes:
                    var filterVote = int.Parse(filterValue);     //#D
                    return books.Where(x =>                      //#D
                            x.ReviewsAverageVotes > filterVote);   //#D
                case BooksFilterBy.ByPublicationYear:
                    if (filterValue == AllBooksNotPublishedString)//#E
                        return books.Where(                       //#E
                            x => x.PublishedOn > DateTime.UtcNow);//#E

                    var filterYear = int.Parse(filterValue);      //#F
                    return books.Where(                           //#F
                        x => x.PublishedOn.Year == filterYear     //#F
                            && x.PublishedOn <= DateTime.UtcNow);   //#F
                default:
                    throw new ArgumentOutOfRangeException
                        (nameof(filterBy), filterBy, null);
            }
        }
    }
}
