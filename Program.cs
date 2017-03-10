using System;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;

    using OpenQA.Selenium.Chrome;

    class Program
    {
        static void Main(string[] args)
        {
            var driver = new ChromeDriver();

            /*
            driver.DownloadPicture(
                @"https://ae01.alicdn.com/kf/HTB1a.7hPpXXXXaiXVXXq6xXFXXXj/HDY-Haoduoyi-Black-Women-Dress-Long-Sleeve-Turn-down-Collar-Split-Side-Straigh-Dress-Women-Embroidery.jpg_640x640.jpg");

            //*/
            
            
            var username = "cecilapabe@cheaphub.net";
            var password = "parolatamieqka";
            var extractor = new AliExpress(driver);

            extractor.LoginAliexpress(username, password);

            var products = extractor.Extract(
                "https://www.aliexpress.com/category/200003482/dresses.html?spm=2114.01010108.0.0.KD2cah&site=glo&g=y&SortType=total_tranpro_desc&tag=",
                5);
            //*/
            /*
            var products = new[]
                           {
                               new AliExpressProduct()
                               {
                                   Name =
                                       "Women Floral Lace Dresses Short Sleeve Party Casual Color Blue Red Black Mini Dress",
                                   ImageUrl =
                                       "https://ae01.alicdn.com/kf/HTB1lOlqOVXXXXazapXXq6xXFXXXd/Fashion-Women-Floral-Lace-Dress-Short-Sleeve-Summer-Party-Casual-Mini-Dress.jpg_220x220.jpg",
                                   PriceRange = "US $6.62 - 7.42"
                               }
                           };
                           */
            var olx = new Olx(driver);
            var olxUsername = "havof@tverya.com";
            var olxPassword = "olxpass123123";

            olx.Login(olxUsername, olxPassword);

            var google = new Google(driver);

            foreach (var product in products)
            {
                var productNameTranslated = google.Translate(product.Name, "en", "bg");
                var price = Regex.Match(product.PriceRange, @"\$*(\d+[.]*\d*)")
                    .Groups[1].Value;
                var olxProduct = new OlxProduct()
                                 {
                                     Categories = new string[]
                                                  {
                                                      "Мода",
                                                      "Дрехи",
                                                      "Дамски дрехи",
                                                      "Рокли и сукмани"
                                                  },
                                     Name = string.Join(
                                         " ",
                                         productNameTranslated.Split(' ')
                                         .Take(6)),
                                     Description = productNameTranslated,
                                     OlxOfferType = OlxOfferType.Private,
                                     PicturesUrls = new string[]
                                                    {
                                                        product.ImageUrl
                                                    },
                                     Price = float.Parse(price) * 2.1f,
                                     State = OlxProductState.New
                                 };

                olx.PublishOffer(olxProduct, "гр. София"); 
            }
        //*/
        }
    }
}
