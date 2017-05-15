namespace ConsoleApplication1
{
    using System.Linq;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    public class Olx
    {
        private readonly IWebDriver driver;
        private readonly INavigation navigation;

        public Olx(IWebDriver driver)
        {
            this.driver = driver;
            this.navigation = this.driver.Navigate();
        }

        public void Login(string username, string password)
        {
            this.navigation.GoToUrl("https://www.olx.bg/account/");
            var usernameInputField = this.driver.FindElement(By.Id("userEmail"));
            var passwordInputField = this.driver.FindElement(By.Id("userPass"));
            var loginButton = this.driver.FindElement(By.Id("se_userLogin"));

            usernameInputField.SendKeys(username);
            passwordInputField.SendKeys(password);

            loginButton.Click();
        }

        public void PublishOffer(OlxProduct product, string offerLocation)
        {
            this.navigation.GoToUrl("https://www.olx.bg/m/adding/");
            this.ChooseCategory(product.Categories);

            this.SubmitOfferDescription(product);
            this.SubmitOfferLocation(offerLocation);
            
            for (int i = 0; i < 3; i++)
            {
                //регион в населеното място - направо submit            
                //данни за контакт - направо submit
                //Добави обява - финална стъпка - submit

                this.SkipSubmit();
            }
        }

        /// <summary>
        /// Uploading image not working
        /// </summary>
        /// <param name="product"></param>
        private void SubmitOfferDescription(OlxProduct product)
        {
            var localImagePath = this.driver.DownloadPicture(product.PicturesUrls.First());

            //начална форма
            var titleInputField = this.driver.FindElement(By.Name("data[title]"));
            var priceInputField = this.driver.FindElement(By.Name("data[param_price][1]"));
            var deliveryCostCheckbox = this.driver.FindElement(By.Name("data[param_delivery_paid_by][]"));
            var stateSelect = new SelectElement(this.driver.FindElement(By.Name("data[param_state]")));
            var descriptionField = this.driver.FindElement(By.Name("data[description]"));
            var firstImageButton = this.driver.FindElement(By.Name("image[1]"));
            var submitButton = this.driver.FindElements(By.CssSelector("input[type=submit]")).Last();

            titleInputField.SendKeys(product.Name);
            priceInputField.SendKeys(product.Price.ToString("F2"));
            deliveryCostCheckbox.Click();
            stateSelect.SelectByValue(product.State.ToString().ToLower());
            descriptionField.SendKeys(product.Description);
            firstImageButton.SendKeys(localImagePath);

            //firstImageButton.Click();
            
            submitButton.Click();
        }

        private void SkipSubmit()
        {
            var submitButton = this.driver.FindElement(By.CssSelector("input[type=submit]"));
            submitButton.Click();
        }

        private void SubmitOfferLocation(string offerLocation)
        {
            var locationInputField = this.driver.FindElement(By.Name("data[city]"));
            var submitButton = this.driver.FindElement(By.CssSelector("input[type=submit]"));

            locationInputField.Clear();
            locationInputField.SendKeys(offerLocation);
            submitButton.Click();
        }

        private void ChooseCategory(string[] categories)
        {
            foreach (string category in categories)
            {
                var categoryUrl = this.driver.FindElements(By.CssSelector("a"))
                    .First(e => e.Text == category)
                    .GetAttribute("href");
                this.navigation.GoToUrl(categoryUrl);
            }
        }
    }

}