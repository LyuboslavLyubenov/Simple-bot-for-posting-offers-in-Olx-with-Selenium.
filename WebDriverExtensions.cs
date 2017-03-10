namespace ConsoleApplication1
{

    using System.IO;
    using System.Linq;
    using System.Reflection;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Extensions;

    public static class WebDriverExtensions
    {
        /// <returns>local path</returns>
        public static string DownloadPicture(this IWebDriver driver, string pictureUrl)
        {
            var pictureName = pictureUrl.Split('/')
                .Last();

            driver.OpenNewTab(pictureUrl);
            driver.TakeScreenshot().SaveAsFile(pictureName, ScreenshotImageFormat.Jpeg);
            driver.Close();

            var windowHandle = driver.WindowHandles.First();
            driver.SwitchTo()
                .Window(windowHandle);

            return Directory.GetCurrentDirectory() + pictureName;
        }
        
        /// <returns>opened window handle</returns>
        public static string OpenNewTab(this IWebDriver driver, string url)
        {
            var windowHandles = driver.WindowHandles;
            driver.ExecuteJavaScript(string.Format("window.open('{0}', '_blank');", url));
            var newWindowHandles = driver.WindowHandles;
            var openedWindowHandle = newWindowHandles.Except(windowHandles).Single();
            driver.SwitchTo().Window(openedWindowHandle);
            return openedWindowHandle;
        }
    }
}
