# Simple-bot-for-posting-offers-in-Olx-with-Selenium.
Simple bot for posting offers in Olx. Made with Selenium. It scrappes product data from Aliexpress and uses it to create offer (post) in Olx.bg

## Requirements
- dotnet 4
- windows
- selenium

## Usage

1. Create aliexpress object

```csharp
//driver = new ChromeDriver();
//or
//driver = new FirefoxDriver();
var aliexpress = new Aliexpress(driver);
```

2. Scrape products from page
```csharp
var products = extractor.Extract(
                "https://www.aliexpress.com/category/200003482/dresses.html?spm=2114.01010108.0.0.KD2cah&site=glo&g=y&SortType=total_tranpro_desc&tag=",
                5);

//it will look something like this:
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
```

3. Create olx object
```csharp
var olx = new Olx(driver);
var olxUsername = "..."; //your olx username
var olxPassword = "..."; //your olx password

olx.Login(olxUsername, olxPassword); //login
```

4. (Optional) Create google object. It's provides translate functionality
```csharp
var google = new Google(driver);
```

5. Create olxProduct object (item you want to sell)
```csharp
var olxProduct = new OlxProduct()
                                 {
                                     Categories = new string[]
                                                  {
                                                      "Мода",
                                                      "Дрехи",
                                                      "Дамски дрехи",
                                                      "Рокли и сукмани"
                                                  }, //categories
                                     Name = string.Join(
                                         " ",
                                         productNameTranslated.Split(' ')
                                         .Take(6)), //item's name
                                     Description = productNameTranslated, //item's description
                                     OlxOfferType = OlxOfferType.Private, //offer type (ussualy aways on Prive)
                                     PicturesUrls = new string[]
                                                    {
                                                        product.ImageUrl
                                                    }, //images
                                     Price = float.Parse(price) * 2.1f, //your prive
                                     State = OlxProductState.New //product state (is it second hand or used)
                                 };
```

6. Publush offer
```csharp
olx.PublishOffer(olxProduct, "гр. София"); //second argument is item's location
```

You are done!

## Warning
Olx doesnt allow more than 1 offer per category. You must pay for premium account (i think) in order to be able to publish many offers in one category. 
