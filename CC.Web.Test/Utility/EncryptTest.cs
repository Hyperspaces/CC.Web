using CC.Web.Utility;
using NUnit.Framework;

namespace CC.Web.Test.Utility
{
    public class EncryptTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("SHA256","hello")]
        public void Test1(string dataSource,string key)
        {
            var hashData = EncryptHelper.EncryptWithSHA256(dataSource,key);
            var data = EncryptHelper.EncryptWithSHA256(dataSource, key);
            Assert.AreEqual(hashData, data);
        }
    }
}
