using System;
using Xunit;

namespace SUS.MvcFramework.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            TestViewModel viewModel = new TestViewModel
            {
                Name = "Dog go Argentina",
                Price = 12345.6m,
                DateOfBirth = new DateTime(2019, 6, 1)
            };
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
