using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EfClasses
{
    public class Review
    {
        public int ReviewId { get; set; }
        [Range(0, 5)]
        public int NumStar { get; set; }
        public string VoterName { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
    }
}
