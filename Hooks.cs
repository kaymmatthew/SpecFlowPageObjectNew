using OpenQA.Selenium.Chrome;
using SpecFlowPageObjectNew.Drivers;
using SpecFlowPageObjectNew.Extensions;
using System.Diagnostics;
using SpecFlowPageObjectNew.Support;
using TechTalk.SpecFlow;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Runtime.InteropServices;

namespace SpecFlowPageObjectNew
{
    [Binding]
    public sealed class Hooks : DriverHelper
    { 

        [BeforeScenario]
        public void BeforeScenarioWithTag()
        {
            // Load configuration first
            ConfigReader.Load();

            var logger = Log.CreateLogger<Hooks>();

            var options = new ChromeOptions();
            options.AddArguments("start-maximized", "incognito");
            options.AddAdditionalOption("headless", true);

            try
            {
                // Let Selenium Manager resolve the appropriate chromedriver for the installed browser
                Driver = new ChromeDriver(options);
                logger.LogInformation("Started ChromeDriver using Selenium Manager");
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, "Failed to start ChromeDriver via Selenium Manager. Attempting local fallback.");

                // Attempt fallback to a locally provided chromedriver binary
                try
                {
                    var baseDir = AppContext.BaseDirectory;
                    // Allow override via environment variable CHROMEDRIVER_PATH
                    var driverDir = Environment.GetEnvironmentVariable("CHROMEDRIVER_PATH") ?? Path.Combine(baseDir, "drivers");
                    string driverFileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "chromedriver.exe" : "chromedriver";
                    var driverPath = Path.Combine(driverDir, driverFileName);

                    if (File.Exists(driverPath))
                    {
                        logger.LogInformation("Found local chromedriver at {path}. Starting using local binary", driverPath);
                        var service = ChromeDriverService.CreateDefaultService(driverDir);
                        service.HideCommandPromptWindow = true;
                        Driver = new ChromeDriver(service, options);
                    }
                    else
                    {
                        logger.LogError("Local chromedriver not found at {path}. Cannot start browser.", driverPath);
                        throw; // rethrow original exception
                    }
                }
                catch (System.Exception innerEx)
                {
                    logger.LogError(innerEx, "Local chromedriver fallback failed.");
                    throw;
                }
            }
        }


        [AfterScenario]
        public void AfterScenario()
        {
            waitExtensions.wait(2000);
            Driver?.Quit();
            using (var process = Process.GetCurrentProcess())
            {
                if (process.ToString() == "chromedriver")
                {
                    process.Kill();
                }
                else if (process.ToString() == "geckodriver")
                {
                    process.Kill();
                }
                Driver?.Dispose();Driver = null; 
            }
        }
    }
}