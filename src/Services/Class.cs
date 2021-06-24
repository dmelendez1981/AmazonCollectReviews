using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonCollectReviews.Services
{
    public class Class
    {

        /// <summary>

        /// This method is use to parse data from string

        /// Return object with data

        /// Lib Reference

        ///  1 : using HtmlAgilityPack;

        ///  2 : using Newtonsoft.Json;

        ///  3 : using Newtonsoft.Json.linq;

        /// </summary>

        /// <param name="strHtml"></param>

        /// <returns></returns>

        public static object DataParse(string strHtml)

        {

            string Asin = String.Empty;

            string url = String.Empty;

            string Upc = String.Empty;

            string Itemmodelnumber = String.Empty;

            string price = String.Empty;

            //string Shippingcost = String.Empty;

            //string availability = String.Empty;

            string Bsr = String.Empty;

            string Salesrankfinal = String.Empty;

            string Noofreviews = String.Empty;

            string Noofratings = String.Empty;

            //string productdescription = String.Empty;

            string Productdimensions = String.Empty;

            string Shippingweight = String.Empty;

            string category = String.Empty;

            List<string> hours = new List<string>();

            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();

            htmlDocument.LoadHtml(strHtml);

            htmlDocument.DocumentNode.Descendants()

            .Where(n => n.Name == "script" || n.Name == "style")

            .ToList()

            .ForEach(n => n.Remove());

            // strJson = htmlDocument.DocumentNode.SelectSingleNode("//script[@type=’application/ld+json’]").InnerText;

            //JObject jObject = JObject.Parse(strJson);

            Asin = htmlDocument.DocumentNode.SelectNodes("//div[@class=’content’]/ul/li").ToList().Where(x => x.InnerText.Contains("ASIN:")).FirstOrDefault().InnerText.Replace("ASIN:","").Trim();

            Upc = htmlDocument.DocumentNode.SelectNodes("//div[@class=’content’]/ul/li").ToList().Where(x => x.InnerText.Contains("UPC:")).FirstOrDefault().InnerText.Replace("UPC:", "").Trim();

            Itemmodelnumber = htmlDocument.DocumentNode.SelectNodes("//div[@class=’content’]/ul/li").ToList().Where(x => x.InnerText.Contains("Item model number:")).FirstOrDefault().InnerText.Replace("Item model number:", "").Trim();

            Noofreviews = htmlDocument.DocumentNode.SelectNodes("//div[@class=’content’]/ul/li").ToList().Where(x => x.InnerText.Contains("Average Customer Review:")).FirstOrDefault().SelectSingleNode(".//span[@class = ‘a-size-small’]").InnerText.Replace("customer reviews", "").Trim();

            Salesrankfinal = htmlDocument.DocumentNode.SelectNodes("//div[@class=’content’]/ul/li").ToList().Where(x => x.InnerText.Contains("Amazon Best Sellers Rank:")).FirstOrDefault().SelectSingleNode(".//ul[@class = ‘zg_hrsr’]").InnerText.Trim().Replace("Amazon Best Sellers Rank:", "").Replace("&nbsp;","").Replace("&gt;","").Replace("\n","").Trim();

            //Noofratings =  htmlDocument.DocumentNode//    .SelectNodes("//div[@class=’content’]/ul/li").ToList().Where(x => x.InnerText.Contains("customer reviews")).FirstOrDefault().InnerText.Replace("customer reviews", "").Replace(",", "").Trim();

            Noofratings = htmlDocument.DocumentNode.SelectSingleNode("//span[@id = ‘acrPopover’]/span[1]/a/i[1]/span").InnerText.Trim();

            //productdescription = htmlDocument.DocumentNode//.SelectSingleNode("//div[@id=’productDescription’]/ul").InnerText.Trim();

            Productdimensions = htmlDocument.DocumentNode.SelectNodes("//div[@class=’content’]/ul/li").ToList().Where(x => x.InnerText.Contains("Product Dimensions:")).FirstOrDefault().InnerText.Replace("Product Dimensions:", "").Replace("; 1.6 ounces", "").Trim();

            Shippingweight = htmlDocument.DocumentNode.SelectNodes("//div[@class=’content’]/ul/li").ToList().Where(x => x.InnerText.Contains("Shipping Weight:")).FirstOrDefault().InnerText.Replace("Shipping Weight:", "").Trim().Replace("(View shipping rates and policies)", "");

            //Shippingweight = Productdimensions.Substring(Productdimensions.IndexOf(";")).Replace(";", "");

            category = htmlDocument.DocumentNode.SelectSingleNode("//span[@id=’productTitle’]").InnerText.Trim();

            price = htmlDocument.DocumentNode.SelectSingleNode("//span[@class=’a-color-price’]").InnerText.Trim();

            //Shippingcost = htmlDocument.DocumentNode.SelectSingleNode("//span[@id=’ourprice_shippingmessage’]/span").InnerText.Trim();

            //availability = htmlDocument.DocumentNode.SelectSingleNode("//span[@id=’availability’]").InnerText.Trim();

            Bsr = Salesrankfinal.Substring(Salesrankfinal.IndexOf("#")).Replace("#", "");

            Bsr = htmlDocument.DocumentNode.SelectNodes("//div[@class=’content’]/ul/li").ToList().Where(x => x.InnerText.Contains("Amazon Best Sellers Rank:")).FirstOrDefault().InnerText.Replace("Amazon Best Sellers Rank:", "");

            Bsr = Bsr.Substring(Bsr.IndexOf("#")).Replace("#","");

            Bsr = Bsr.Substring(0, Bsr.IndexOf("("));

            url = htmlDocument.DocumentNode.SelectSingleNode("//link[@rel = ‘canonical’]").Attributes["href"].Value;

            return new

            {

                URL = url,

                ASIN = Asin,

                UPC = Upc,

                ItemModelNumber = Itemmodelnumber,

                NoofReviews = Noofreviews,

                SalesrankFinal = Salesrankfinal,

                NoofRatings = Noofratings,

                //productDescription = productdescription,

                ProductDimensions = Productdimensions,

                //Availability = availability,

                BSR = Bsr,

                ShippingWeight = Shippingweight,

                Category = category,

                Price = price,

                //ShippingCost = Shippingcost,

            };

        }

    }


}

