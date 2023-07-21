// See https://aka.ms/new-console-template for more information
using HtmlAgilityPack;
using Microsoft.VisualBasic;
using System.Collections;
using System.Net;
using System.Web;
using WebScrapper;
using DocumentFormat.OpenXml;
using static System.Net.WebRequestMethods;

HelperScrapper helperScrapper = new HelperScrapper();
Console.WriteLine("Hello, World!");
List<Product> products = new List<Product>();
List<string> categoriesProductLink = new List<string>();
string fullUrl = "https://superetti.dz/";
var response = HelperScrapper.CallUrl(fullUrl).Result;
categoriesProductLink = HelperScrapper.GetCategoriesLink(response);
Console.WriteLine("hello");


foreach (string categoryProductLink in categoriesProductLink)
{
       List<Product> productspage = new List<Product>(); 
    string url = "";
    int i = 0;
    do
    {
        i++;
        url = categoryProductLink + $"page/{i}/";

        try
        {

            response = HelperScrapper.CallUrl(url)?.Result;
            productspage = HelperScrapper.GetProduct(response);
        }
        catch (Exception e)
        {

            productspage = new List<Product>(); ;
        }
        products.AddRange(productspage);
    } while (productspage.Count > 0);

    Console.WriteLine("hello world");
}
IEnumerable<Product> listProducts;


products.ToTable();
Console.WriteLine("hello world");

