using System;
using System.Collections.Generic;
using System.IO;
using SUS.MvcFramework.Tests.ViewModels;
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
        [InlineData("ParsingScripts")]
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

            string actualResult = susViewEngine.GetHtml(templateCode, viewModel, null);

            string expectedResult = File.ReadAllText($"ViewTests/{fileName}.Result.html");

            Assert.Equal(expectedResult, actualResult);

        }

        [Fact]
        public void TestTemplateViewModel()
        {
            IViewEngine vurViewEngine = new SusViewEngine();

            var actualResult = vurViewEngine
                .GetHtml(@"@foreach(var num in Model)
                {
<span>@num</span>
                }", new List<int> { 1, 2, 3 } , null);

            var expectedResult = @"<span>1</span>
<span>2</span>
<span>3</span>";

            Assert.Equal(expectedResult, actualResult);
        }
    }

}
