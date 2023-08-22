using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper
{
    public class Product
    {
       
        public string Name { get; set; } = String.Empty;
        public string Category { get; set; } = String.Empty;
        public Decimal Price { get; set; } = Decimal.Zero;
        public String UrlImage { get; set; }= String.Empty;
        public String FileName { get; set; } = String.Empty;

    }
}
