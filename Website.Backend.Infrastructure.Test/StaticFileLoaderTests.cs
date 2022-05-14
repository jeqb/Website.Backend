using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Website.Backend.Infrastructure.Email;

namespace Website.Backend.Infrastructure.Test
{
    public class StaticFileLoaderTests
    {
        [Fact]
        public async Task GetFileStringAsync_GetsEmailString_GivenFileName()
        {

            // Arrange

            string fileName = "ThankYouEmailBody.html";


            // Act

            string result = StaticFileLoader.GetFileString(fileName);

            bool containsSubString = result.Contains("<!DOCTYPE html>");


            // Assert

            Assert.True(containsSubString);
        }
    }
}
