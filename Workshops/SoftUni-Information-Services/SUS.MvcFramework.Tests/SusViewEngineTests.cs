using System;
using System.IO;
using SUS.MvcFramework.ViewEngine;
using Xunit;

namespace SUS.MvcFramework.Tests
{
    public class SusViewEngineTests
    {
        [Theory]
        [InlineData("CleanHtml")]
        [InlineData("Foreach")]
        [InlineData("IfElseFor")]
        [InlineData("ViewModel")]
        public void TestGetHtml(string fileName)
        {
            string templateCode = File.ReadAllText($"ViewTests/{fileName}.html");

            TestViewModel viewModel = new TestViewModel
            {
                Name = "Dog go Argentina",
                Price = 12345.6m,
                DateOfBirth = new DateTime(2019, 6, 1)
            };

            IViewEngine susViewEngine = new SusViewEngine();

            string actualResult = susViewEngine.GetHtml(templateCode, viewModel);

            string expectedResult = File.ReadAllText($"ViewTests/{fileName}.Result.html");

            Assert.Equal(expectedResult, actualResult);
        }

        public class TestViewModel
        {
            public TestViewModel()
            {
                
            }

            public TestViewModel(string name, decimal price, DateTime dateOfBirth)
                :this()
            {
                Name = name;
                Price = price;
                DateOfBirth = dateOfBirth;
            }

            public string Name { get; set; }

            public decimal Price { get; set; }

            public DateTime DateOfBirth { get; set; }
        }
    }
}
