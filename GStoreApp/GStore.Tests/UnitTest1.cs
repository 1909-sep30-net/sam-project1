using System;
using Xunit;
using GStore.WebUI.Controllers;
using GStoreApp.Library;
using DB.Repo;

namespace GStore.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void NameFormatTest()
        {
            FormatHandler m = new FormatHandler();

            string s1 = m.NameFormat("Sam");
            string s2 = m.NameFormat("bob");
            string s3 = m.NameFormat("123bOb");
            string s4 = m.NameFormat("boB321");

            Assert.Equal("Sam", s1);
            Assert.Equal("Bob", s2);
            Assert.Equal("Bob", s3);
            Assert.Equal("Bob", s4);
        }

        [Fact]
        public void PhoneCheckTest()
        {
            FormatHandler m = new FormatHandler();

            string s1 = m.PhoneCheck("ss1234567890");
            string s2 = m.PhoneCheck("123s4567890s");
            string s3 = m.PhoneCheck("1234567ss890s");

            Assert.Equal("(123)456-7890", s1);
            Assert.Equal("(123)456-7890", s2);
            Assert.Equal("(123)456-7890", s3);

        }
  
    }
}
