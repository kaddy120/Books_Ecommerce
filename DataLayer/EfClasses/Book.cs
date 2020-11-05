using System;
using System.Collections.Generic;

namespace DataLayer.EfClasses
{
    public class Book
    {
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedOn { get; set; }
        public string Publisher { get; set; }
        public string Discription { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool SoftDeleted { get; set; }

        public string Title { get; set; }
        public PriceOffer Promotion { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<BookAuthor> AuthorLink { get; set; }
    }
}
