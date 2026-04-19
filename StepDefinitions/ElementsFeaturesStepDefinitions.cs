using NUnit.Framework;
using SpecFlowPageObjectNew.Pages;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using static SpecFlowPageObjectNew.EnumExtensions.EnumValues;

namespace SpecFlowPageObjectNew.StepDefinitions
{
    [Binding]
    public class ElementsFeaturesStepDefinitions
    {
        HomePage _homepage = new HomePage();

        [Given(@"I navige to demoQa page")]
        public void GivenINavigeToDemoQaPage()
        {
            // Ensure configuration is loaded and navigate to base url
            SpecFlowPageObjectNew.Support.ConfigReader.Load();
            _homepage.NavigateToDemoQASite();
        }

        [When(@"I click Elements")]
        public void WhenIClickElements()
        {
            _homepage.NavigateToDemoQASite();
            _homepage.ClickElement();
        }

        [When(@"I click Text Box")]
        public void WhenIClickTextBox()
        {
            _homepage.ClickTextBox();
        }

        [When(@"I enter the following data")]
        public void WhenIEnterTheFollowingData(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            _homepage.EnterContactDetails(data.FullName, data.Email, data.CurrentAddress, data.PermanentAddress);
        }

        [When(@"I click submit button")]
        public void WhenIClickSubmitButton()
        {
            _homepage.ClickSubmitBtn(); 
        }

        //[Then(@"following data has been added")]
        //public void ThenFollowingDataHasBeenAdded(Table table)
        //{
        //    dynamic expected = table.CreateDynamicInstance();
        //    var actual = _homepage.getelementsValue();
        //    Assert.Multiple(() =>
        //    {
        //        Assert.AreEqual(expected.FullName, actual?.FirstOrDefault()?.Text.Split(":")[(int)IntValue.One]);
        //        Assert.AreEqual(expected.Email, actual?.ElementAtOrDefault((int)IntValue.One)?.Text.Split(":")[(int)IntValue.One]);
        //        Assert.AreEqual(expected.CurrentAddress, actual?.ElementAtOrDefault((int)IntValue.Two)?.Text.Split(":")[(int)IntValue.One]);
        //        Assert.AreEqual(expected.PermanentAddress, actual?.ElementAtOrDefault((int)IntValue.Three)?.Text.Split(":")[(int)IntValue.One]);
        //    });

        //}
        [Then(@"following data has been added")]
        public void ThenFollowingDataHasBeenAdded(Table table)
        {
            dynamic expected = table.CreateDynamicInstance();
            var actual = _homepage.getelementsValue();

            Assert.Multiple(() =>
            {
                Assert.AreEqual((string)expected.FullName,
                    GetValue(actual?.FirstOrDefault()?.Text));

                Assert.AreEqual((string)expected.Email,
                    GetValue(actual?.ElementAtOrDefault((int)IntValue.One)?.Text));

                Assert.AreEqual((string)expected.CurrentAddress,
                    GetValue(actual?.ElementAtOrDefault((int)IntValue.Two)?.Text));

                Assert.AreEqual((string)expected.PermanentAddress,
                    GetValue(actual?.ElementAtOrDefault((int)IntValue.Three)?.Text));
            });
        }

        private static string GetValue(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;

            var parts = text.Split(':', 2); // prevent index errors
            return parts.Length > 1 ? parts[1].Trim() : null;
        }
    }
}