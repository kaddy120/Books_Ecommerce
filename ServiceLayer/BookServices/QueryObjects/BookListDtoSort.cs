using DataLayer.EfClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.BookServices.QueryObjects
{
    public enum OrderByOptions
    {
        [Display(Name = "sort by...")]
        SimpleOrder = 0,
        [Display(Name = "Votes ↑")]
        ByVotes,
        [Display(Name = "Publication Date ↑")]
        ByPublicationDate,
        [Display(Name = "Price ↓")]
        ByPriceLowestFirst,
        [Display(Name = "Price ↑")]
        ByPriceHigestFirst
    }

    public static class BookListDtoSort
    {
        public static IQueryable<BookListDTO> OrderBooksBy(
            this IQueryable<BookListDTO> books,
            OrderByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case OrderByOptions.SimpleOrder:
                    return books.OrderByDescending(b => b.BookId);
                case OrderByOptions.ByVotes:
                    return books.OrderByDescending(b => b.ReviewsAverageVotes);
                case OrderByOptions.ByPublicationDate:
                    return books.OrderByDescending(b => b.PublishedOn);
                case OrderByOptions.ByPriceLowestFirst:
                    return books.OrderBy(b => b.ActualPrice);
                case OrderByOptions.ByPriceHigestFirst:
                    return books.OrderByDescending(b => b.ActualPrice);
                default:
                    throw new ArgumentOutOfRangeException(
                         nameof(orderByOptions), orderByOptions, null);
            }

        }
        
    }
}
