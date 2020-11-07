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
    public class ChangePriceOfferServices
    {
        private readonly EcommerceContext _context;

        public Book OrgBook { get; private set; }

        public ChangePriceOfferServices(EcommerceContext context)
        {
            _context = context;
        }

        public PriceOffer GetPriceOffer(int id)
        {
            var OrgBook = _context.Books.
                Include(b => b.Promotion).
                Single(b => b.BookId == id);

            return OrgBook?.Promotion              //#C
                ?? new PriceOffer                  //#C
                {                               //#C
                    BookId = id,                //#C
                    NewPrice = OrgBook.Price    //#C
                };
        }

        public Book Update(PriceOffer priceOffer)
        {
            var book = _context.Books.
                Include(b => b.Promotion).
                Single(b => b.BookId == priceOffer.BookId);

            if(book?.Promotion == null)
            {
                book.Promotion = priceOffer;
            }
            else
            {
                book.Promotion.NewPrice = priceOffer.NewPrice;
                book.Promotion.PromotionalText = priceOffer.PromotionalText;
            }

            _context.SaveChanges();

            return book;
        }
        
    }
}
