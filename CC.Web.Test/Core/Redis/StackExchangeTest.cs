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
        private IDatabase RedisDb { get; set; }

        [SetUp]
        public void Setup() {
            var conn = ConnectionMultiplexer.Connect("localhost,password=a3101170z");
            RedisDb = conn.GetDatabase();
        }

        [TestCase("SEK","SEV",2)]
        public void StringTest(string key,string value,int second) {
            RedisDb.StringSet(key, value, new TimeSpan(0, 0, second));
            var redisValue = RedisDb.StringGet(key);
            Assert.AreEqual(value, redisValue.ToString());

            Thread.Sleep(3000);

            redisValue = RedisDb.StringGet(key);
            Assert.IsFalse(redisValue.HasValue);

            RedisDb.StringSet("countNum", "1");
            RedisDb.StringSet("countWord", "a");

            RedisDb.StringIncrement("countNum");
            var countNum = RedisDb.StringGet("countNum");
            Assert.AreEqual("2", countNum.ToString());

            Assert.Throws<RedisServerException>(() => RedisDb.StringIncrement("countWord"));

            RedisDb.StringAppend("countWord","bc");
            redisValue = RedisDb.StringGet("countWord");
            Assert.AreEqual("abc", redisValue.ToString());

            var type = RedisDb.KeyType("countWord");
            Assert.AreEqual("String", type.ToString());

            RedisDb.StringSet("require", "first");
            RedisDb.StringSet("require", "second");
            redisValue = RedisDb.StringGet("require");

            Assert.AreEqual("second", redisValue.ToString());
        }

        [TestCase("LK", "LV", 2)]
        public void ListTest(string key, string value, int second)
        {
            RedisDb.ListRightPush(key, value);
            RedisDb.ListRightPush(key, value + "E");
            RedisDb.ListLeftPush(key, value.Substring(0,1));
            RedisDb.ListRightPush(key, value.Insert(0,"O") + "E");

            var rangeList = RedisDb.ListRange(key, 1, 2);
            Assert.AreEqual("2", rangeList.Length);

            var listKey = RedisDb.ListGetByIndex(key, 0);
            RedisDb.ListLeftPop(key);
            Assert.AreEqual(value, listKey.ToString());

            var listKey2 = RedisDb.ListRightPop(key);
            Assert.AreEqual(value + "E", listKey2.ToString());
        }

        [TestCase("obj")]
        public void HashTest(string key) {
            RedisDb.HashSet(key, new HashEntry[] { new HashEntry("one", "first"), new HashEntry("two", "second"), new HashEntry("three", "third") });
            var keys = RedisDb.HashKeys(key);
            Assert.AreEqual(3, keys.Length);

            var values = RedisDb.HashValues(key);
            Assert.AreEqual(3, values.Length);

            var two = RedisDb.HashGet(key, "two");
            Assert.AreEqual("second", two.ToString());
        }
    }
}
