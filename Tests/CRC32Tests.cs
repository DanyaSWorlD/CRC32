using System;
using System.IO;

using Daquga.Security.Cryptography;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CRC32Tests
    {
        [TestMethod]
        public void TestFileChecksum()
        {
            const string idealResult = "566C0AE3";
            var result = CRC32.FromFile(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\test.txt");

            if (!idealResult.Equals(result)) Assert.Fail("Результат не совпал");
        }

        [TestMethod]
        public void TestStringChecksum1()
        {
            const string idealResult = "8587D865";
            var result = CRC32.FromString("abcde");

            if (!idealResult.Equals(result)) Assert.Fail("Результат не совпал");
        }

        [TestMethod]
        public void TestStringChecksum2()
        {
            const string idealResult = "B7D1FDCE";
            var result = CRC32.FromString("abcdedfngh;sf;hjoifdjbhosjf;ojd;cbnnb;sonfbosnbo;bln");

            if (!idealResult.Equals(result)) Assert.Fail("Результат не совпал");
        }
    }
}
