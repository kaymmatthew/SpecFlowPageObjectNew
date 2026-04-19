using OpenQA.Selenium;
using SpecFlowPageObjectNew.Drivers;
using SpecFlowPageObjectNew.Extensions;
using SpecFlowTestOctoberProject.Extensions;


namespace SpecFlowPageObjectNew.Pages
{
    public class HomePage : DriverHelper
    {
        private IWebElement? element => Driver?.FindthisElement(
            By.XPath("//div[contains(@class,'card mt-')][.='Elements']")).Single;

        private IWebElement? textBox => Driver?.FindthisElement(By.XPath("//span[@class='text'][.='Text Box']")).Single;

        private IList<IWebElement>? Output => Driver!.FindthisElement(By.XPath("//div[@id='output']//p")).multiple;

        private IWebElement? FullName => Driver?.FindthisElement(By.Id("userName")).Single;
        private IWebElement? Email => Driver?.FindthisElement(By.Id("userEmail")).Single;
        private IWebElement? CAddress => Driver?.FindthisElement(By.Id("currentAddress")).Single;
        private IWebElement? PAddress => Driver?.FindthisElement(By.Id("permanentAddress")).Single;
        private IWebElement? submitBtn => Driver?.FindthisElement(By.Id("submit")).Single;



        public void NavigateToDemoQASite()
        {
            // Use configuration reader to get base URL
            Driver?.Navigate().GoToUrl(SpecFlowPageObjectNew.Support.ConfigReader.DemoQaUrl);
        }

        public void ClickElement() => element?.Click();  
        public void ClickTextBox() => textBox?.Click();

        public void EnterContactDetails(string fullName, string email, string cAddress, string pAddress)
        {
            FullName?.EnterText(fullName);
            Email?.EnterText(email);
            CAddress?.EnterText(cAddress);
            PAddress?.EnterText(pAddress);

        }

        public void ClickSubmitBtn() => submitBtn?.ClickViaJs(Driver!);

        public IList<IWebElement>? getelementsValue() => Output?.ToList();
    }
}