namespace ConsoleApplication1
{

    using System;
    using System.Collections.Generic;
    using System.Threading;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Extensions;

    public class AliExpress
    {
        private IWebDriver driver;
        private INavigation navigation;

        public AliExpress(IWebDriver driver)
        {
            this.driver = driver;
            this.navigation = this.driver.Navigate();
        }

        public void LoginAliexpress(string username, string password)
        {
            this.navigation.GoToUrl("https://login.aliexpress.com/");
            
            this.driver.SwitchTo()
                .Frame("alibaba-login-box");
            var usernameInputField = this.driver.FindElement(By.Id("fm-login-id"));
            var passwordInputField = this.driver.FindElement(By.Id("fm-login-password"));

            usernameInputField.SendKeys(username);
            passwordInputField.SendKeys(password);

            var submitButton = this.driver.FindElement(By.Id("fm-login-submit"));
            submitButton.Click();
            
            Thread.Sleep(2000);
        }

        public IList<AliExpressProduct> Extract(string url, int limit = Int32.MaxValue)
        {
            this.navigation.GoToUrl(url);

            Thread.Sleep(500);
            this.driver.ExecuteJavaScript("window.scrollTo(0, document.body.scrollHeight / 2);");//load all images
            Thread.Sleep(500);
            this.driver.ExecuteJavaScript("window.scrollTo(0, document.body.scrollHeight);");
            Thread.Sleep(500);

            var productsElements = this.driver.FindElements(By.ClassName("item"));
            var products = new List<AliExpressProduct>();

            foreach (var productWebElement in productsElements)
            {
                var imageUrl = productWebElement.FindElement(By.CssSelector(".pic a > img"))
                    .GetAttribute("src");
                var name = productWebElement.FindElement(By.CssSelector(".info h3 .product"))
                    .GetAttribute("title");
                var priceRange = productWebElement.FindElement(By.CssSelector(".info .price > span"))
                    .Text;
                var product = new AliExpressProduct()
                              {
                                  ImageUrl = imageUrl,
                                  Name = name,
                                  PriceRange = priceRange
                              };
                products.Add(product);

                if (products.Count == limit)
                {
                    break;
                }
            }

            return products;
        }
    }

}