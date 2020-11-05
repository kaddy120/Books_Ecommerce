﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EfClasses
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public ICollection<BookAuthor> BooksLink { get; set; }
    }
}