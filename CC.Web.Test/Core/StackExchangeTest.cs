using NUnit.Framework;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CC.Web.Test.Core
{
    public class StackExchangeTest
    {
        private IDatabase redisDb { get; set; }

        [SetUp]
        public void Setup() {
            var conn = ConnectionMultiplexer.Connect("localhost,password=a3101170z");
            redisDb = conn.GetDatabase();
        }

        [TestCase("SEK","SEV",2)]
        public void StringTest(string key,string value,int second) {
            redisDb.StringSet(key, value, new TimeSpan(0, 0, second));
            var redisValue = redisDb.StringGet(key);
            Assert.AreEqual(value, redisValue.ToString());

            Thread.Sleep(3000);

            redisValue = redisDb.StringGet(key);
            Assert.IsFalse(redisValue.HasValue);

            redisDb.StringSet("countNum", "1");
            redisDb.StringSet("countWord", "a");

            redisDb.StringIncrement("countNum");
            var countNum = redisDb.StringGet("countNum");
            Assert.AreEqual("2", countNum.ToString());

            Assert.Throws<RedisServerException>(() => redisDb.StringIncrement("countWord"));

            redisDb.StringAppend("countWord","bc");
            redisValue = redisDb.StringGet("countWord");
            Assert.AreEqual("abc", redisValue.ToString());

            var type = redisDb.KeyType("countWord");
            Assert.AreEqual("String", type.ToString());
        }

        [TestCase("LK", "LV", 2)]
        public void ListTest(string key, string value, int second)
        {
            redisDb.ListRightPush(key, value);
            redisDb.ListRightPush(key, value + "E");

            var listKey = redisDb.ListGetByIndex(key, 0);
            Assert.AreEqual(value, listKey.ToString());

            var listKey2 = redisDb.ListRightPop(key);
            Assert.AreEqual(value + "E", listKey2.ToString());
        }
    }
}
