namespace ConsoleApplication1
{

    using OpenQA.Selenium;

    public class Google
    {
        private IWebDriver driver;

        private INavigation navigation;

        public Google(IWebDriver driver)
        {
            this.driver = driver;
            this.navigation = this.driver.Navigate();
        }
        
        /// <param name="fromLanguage">Use short name (usually 2 characters)</param>
        /// <param name="toLanguage">Use short name (usually 2 characters)</param>
        public string Translate(string text, string fromLanguage, string toLanguage)
        {
            this.navigation.GoToUrl($"https://translate.google.com/m?sl={fromLanguage}&tl={toLanguage}");

            var textInputField = this.driver.FindElement(By.Name("q"));
            var submitButton = this.driver.FindElement(By.CssSelector("input[type=submit]"));

            textInputField.SendKeys(text);
            submitButton.Click();

            var translatedText = this.driver.FindElement(By.ClassName("t0"));

            return translatedText.Text;
        }
    }

}