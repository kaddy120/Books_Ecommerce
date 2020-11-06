using DataLayer.Database;
using ServiceLayer.BookServices.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.BookServices.Concrete
{
    public class BookFilterDropdownService
    {
        private readonly EcommerceContext Context;

        public BookFilterDropdownService(EcommerceContext context)
        {
            Context = context;
        }

        public IEnumerable<DropdownTuple> GetFilterDropDownValues(BooksFilterBy filterBy)
        {
            switch (filterBy)
            {
                case BooksFilterBy.NoFilter:
                    return new List<DropdownTuple>();
                case BooksFilterBy.ByVotes:
                    return FormVotesDropDown();
                case BooksFilterBy.ByPublicationYear:
                    var comingSoon = Context.Books.                     //#A
                        Any(x => x.PublishedOn > DateTime.UtcNow);  //#A
                    var nextYear = DateTime.UtcNow.AddYears(1).Year;//#B
                    var result = Context.Books                          //#C
                        .Select(x => x.PublishedOn.Year)            //#C
                        .Distinct()                                 //#C
                        .Where(x => x < nextYear)                   //#C
                        .OrderByDescending(x => x)                  //#C
                        .Select(x => new DropdownTuple              //#D
                        {                                           //#D
                            Value = x.ToString(),                   //#D
                            Text = x.ToString()                     //#D
                        }).ToList();                                //#D
                    if (comingSoon)                                 //#E
                        result.Insert(0, new DropdownTuple          //#E
                        {
                            Value = BookListDtoFilter.AllBooksNotPublishedString,
                            Text = BookListDtoFilter.AllBooksNotPublishedString
                        });

                    return result;
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterBy), filterBy, null);
            }
        }
        private static IEnumerable<DropdownTuple> FormVotesDropDown()
        {
            return new[]
            {
                new DropdownTuple {Value = "4", Text = "4 stars and up"},
                new DropdownTuple {Value = "3", Text = "3 stars and up"},
                new DropdownTuple {Value = "2", Text = "2 stars and up"},
                new DropdownTuple {Value = "1", Text = "1 star and up"},
            };
        }
    }
}
