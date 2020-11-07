using DataLayer.Database;
using DataLayer.EfClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AdminServices.Concrete
{
    public class ChangePubDateService
    {
        private readonly EcommerceContext Context;

        public ChangePubDateService(EcommerceContext context)
        {
            Context = context;
        }

        public ChangePubDateDto GetOriginal(int id)    //#A
        {
            return Context.Books
                .Select(p => new ChangePubDateDto      //#B
                {                                      //#B
                    BookId = p.BookId,                 //#B
                    Title = p.Title,                   //#B
                    PublishedOn = p.PublishedOn        //#B
                })                                     //#B
                .Single(k => k.BookId == id);          //#C
        }

        public Book Update(ChangePubDateDto changePubDateDto)
        {
            var Book = Context.Find<Book>(changePubDateDto.BookId);
            Book.PublishedOn = changePubDateDto.PublishedOn;
            Context.SaveChanges();
            return Book;
        }


    }
}
