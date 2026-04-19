using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SpecFlowPageObjectNew.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowPageObjectNew.Extensions
{
    public static class waitExtensions 
    {
        public static void wait(int seconds)=> Thread.Sleep(seconds);
        public static (IWebElement Single, IList<IWebElement> multiple) FindthisElement(this IWebDriver Driver, By by)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
            return (wait.Until(x => x.FindElement(by)), wait.Until(_ => _.FindElements(by)));

        }

        public static void ScrollIntoView (this IWebDriver Driver, By by)
        {
            var wait = new WebDriverWait (Driver, TimeSpan.FromSeconds(30));
            wait.Until(x => x.FindElement(by));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true)", wait);
        }
    }
}
