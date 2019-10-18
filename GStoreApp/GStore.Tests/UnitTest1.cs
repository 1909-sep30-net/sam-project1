using System;
using Xunit;
using GStoreApp.ConsoleApp;
using GStoreApp.Library;
using DB.Repo;

namespace GStore.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void InputCheckShouldCheck()
        {
            Menu menu = new Menu();

            int finalInput1 = menu.InputCheckInt(1,1);
            int finalInput2 = menu.InputCheckInt(2,1);
            int finalInput3 = menu.InputCheckInt(3,1);
            int finalInput4 = menu.InputCheckInt(4,1);
            int finalInput5 = menu.InputCheckInt(1,2);
            int finalInput6 = menu.InputCheckInt(2,2);

            Assert.True(finalInput1 == 1);
            Assert.True(finalInput2 == 2);
            Assert.True(finalInput3 == 3);
            Assert.True(finalInput4 == 4);
            Assert.True(finalInput5 == 1);
            Assert.True(finalInput6 == 2);

        }

        [Fact]
        public void NameFormatTest()
        {
            Menu m = new Menu();

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
            Menu m = new Menu();

            string s1 = m.PhoneCheck("ss1234567890");
            string s2 = m.PhoneCheck("123s4567890s");
            string s3 = m.PhoneCheck("1234567ss890s");

            Assert.Equal("(123)456-7890", s1);
            Assert.Equal("(123)456-7890", s2);
            Assert.Equal("(123)456-7890", s3);

        }

        
    }
}
