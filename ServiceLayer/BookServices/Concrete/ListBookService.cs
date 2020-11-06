using DataLayer.Database;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.BookServices.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.BookServices.Concrete
{
    public class ListBookService
    {
        private readonly EcommerceContext Context;

        public ListBookService(EcommerceContext context)
        {
            Context = context;
        }

        public IQueryable<BookListDTO> SortFilterPage(SortFilterPageOptions options)
        {
            var BooksQuery = Context.Books.
                AsNoTracking().
                MapBookToDto().
                FilterBooksBy(options.FilterBy, options.FilterValue).
                OrderBooksBy(options.OrderByOptions);

            options.SetupRestOfDto(BooksQuery);
            
            return BooksQuery
                .Page<BookListDTO>(options.PageNum - 1,options.PageSize);
        }

    }
}
