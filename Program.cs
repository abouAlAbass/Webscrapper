// See https://aka.ms/new-console-template for more information
using HtmlAgilityPack;
using Microsoft.VisualBasic;
using System.Collections;
using System.Net;
using System.Web;
using WebScrapper;
using static System.Net.WebRequestMethods;
using ClosedXML.Excel;

HelperScrapper helperScrapper = new HelperScrapper();
List<Product> products = new List<Product>();
List<string> categoriesProductLink = new List<string>();
string fullUrl = "https://superetti.dz/";
var response = HelperScrapper.CallUrl(fullUrl).Result;
categoriesProductLink = HelperScrapper.GetCategoriesLink(response);



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
var wb = new XLWorkbook();
var ws = wb.Worksheets.Add("Data_Test_Worksheet");
ws.Cell(1, 1).InsertTable<Product>(products);

wb.SaveAs(@"products.xlsx");

