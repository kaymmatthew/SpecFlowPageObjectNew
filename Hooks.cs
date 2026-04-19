using OpenQA.Selenium.Chrome;
using SpecFlowPageObjectNew.Drivers;
using SpecFlowPageObjectNew.Support;

namespace SpecFlowPageObjectNew
{
    [Binding]
    public sealed class Hooks : DriverHelper
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            // Load config
            ConfigReader.Load();

            var options = new ChromeOptions();

            // ✅ REQUIRED for GitHub Actions / Linux CI
            options.AddArguments("--headless=new");
            options.AddArguments("--no-sandbox");
            options.AddArguments("--disable-dev-shm-usage");
            options.AddArguments("--window-size=1920,1080");

            // Optional but safe
            options.AddArguments("--incognito");

            // ✅ Selenium Manager will handle driver automatically
            Driver = new ChromeDriver(options);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
                Driver = null;
            }
        }
    }
}