using DocumentFormat.OpenXml.Spreadsheet;
using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebScrapper
{
    public class HelperScrapper
    {
       public static async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();
            var response = client.GetStringAsync(fullUrl);
            return await response;
        }
        public static List<string> GetCategoriesLink(string html)
        {
            var categoriesList = new List<string>();
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var categories = htmlDoc.DocumentNode.Descendants("Div")
                    .Where(node => node.GetAttributeValue("class", "").Contains("category-image")).ToList();
            foreach (var category in categories)
            {
                var linkCategory = category.SelectSingleNode(".//a[@class='category-image']").GetAttributeValue("href", string.Empty); ;
                categoriesList.Add(linkCategory);
            }
            return categoriesList;
        }


        public static List<Product> GetProduct(string html)
        {
            List<Product> products = new List<Product>();
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var productNodes = htmlDoc.DocumentNode.Descendants("Div")
                    .Where(node => node.GetAttributeValue("class", "").Contains("product-grid-item")).ToList();
            foreach (var node in productNodes)
            {
                var title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//h3[@class='product-title']").InnerText).Trim();
                var category = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='woodmart-product-cats']").InnerText).Trim();
                var nodePrice = node.SelectSingleNode(".//span[@class='woocommerce-Price-amount amount']");
                var productLinkNode = node.SelectSingleNode(".//a[@class='product-image-link']/img");
                string urlImg = "";
                //productLinkNode.SelectSingleNode(".//img"); 


                string pricestring = "0";
                decimal price = 0;
                string fileName = "";
                if (nodePrice!=null)
                {

                    pricestring=  HttpUtility.HtmlDecode(node.SelectSingleNode(".//span[@class='woocommerce-Price-amount amount']")?.InnerText).Replace("DZD", "").Trim();
                    Decimal.TryParse(pricestring, out price);
                }
                if (productLinkNode!=null)
                {
                   urlImg = productLinkNode.Attributes["src"].Value;
                     fileName = title.Replace("/", "_").Replace("*", "").Replace(";", "").Replace(':', ' ')+".png";
                    DownloadFileAsync(urlImg, ".\\imgProduct\\"+fileName);
                }
              
              
                Product product = new Product{ Name = title, Category = category, Price = price,UrlImage=urlImg,FileName= fileName };
                products.Add(product);
            }
        return  products;
        }
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async void DownloadFileAsync(string uri
             , string outputPath)
        {
            Uri uriResult;

            if (!Uri.TryCreate(uri, UriKind.Absolute, out uriResult))
                throw new InvalidOperationException("URI is invalid.");

            //if (!File.Exists(outputPath))
            //    throw new FileNotFoundException("File not found."
            //       , nameof(outputPath));

            byte[] fileBytes = await _httpClient.GetByteArrayAsync(uri);
            try
            {
                File.WriteAllBytes(outputPath, fileBytes);
            }
            catch (Exception e)
            {

                Console.WriteLine($"error {e.Message}");
            }
           
        }
      
    }


   
}
