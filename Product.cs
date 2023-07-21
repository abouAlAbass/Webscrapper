using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper
{
    public class Product
    {
        public string Link { get; set; } = String.Empty; 
        public string Name { get; set; } = String.Empty;
        public string Category { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public Decimal Price { get; set; } = Decimal.Zero;
        public String UrlImage { get; set; }= String.Empty;

    }
}
