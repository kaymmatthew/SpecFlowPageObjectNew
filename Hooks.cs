using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlowPageObjectNew.Drivers;
using SpecFlowPageObjectNew.Support;

namespace SpecFlowPageObjectNew
{
    [Binding]
    public sealed class Hooks : DriverHelper
    {
        private readonly ScenarioContext _scenarioContext;

        // ✅ Dependency injection (replaces ScenarioContext.Current)
        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            // Load config
            ConfigReader.Load();

            var options = new ChromeOptions();

            // CI-safe options
            options.AddArguments("--headless=new");
            options.AddArguments("--no-sandbox");
            options.AddArguments("--disable-dev-shm-usage");
            options.AddArguments("--window-size=1920,1080");
            options.AddArguments("--incognito");

            // Selenium Manager handles driver automatically
            Driver = new ChromeDriver(options);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // ✅ Take screenshot only if test failed
            if (_scenarioContext.TestError != null)
            {
                try
                {
                    var screenshot = ((ITakesScreenshot)Driver!).GetScreenshot();
                    var path = Path.Combine(AppContext.BaseDirectory, "screenshot.png");

                    screenshot.SaveAsFile(path);

                    AllureHelper.AttachScreenshot(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Screenshot capture failed: " + ex.Message);
                }
            }

            Driver?.Quit();
            Driver?.Dispose();
            Driver = null;
        }
    }
}