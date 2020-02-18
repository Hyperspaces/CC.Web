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

            redisDb.StringSet("require", "first");
            redisDb.StringSet("require", "second");
            redisValue = redisDb.StringGet("require");

            Assert.AreEqual("second", redisValue.ToString());
        }

        [TestCase("LK", "LV", 2)]
        public void ListTest(string key, string value, int second)
        {
            redisDb.ListRightPush(key, value);
            redisDb.ListRightPush(key, value + "E");
            redisDb.ListLeftPush(key, value.Substring(0,1));
            redisDb.ListRightPush(key, value.Insert(0,"O") + "E");

            var rangeList = redisDb.ListRange(key, 1, 2);
            Assert.AreEqual("2", rangeList.Length);

            var listKey = redisDb.ListGetByIndex(key, 0);
            redisDb.ListLeftPop(key);
            Assert.AreEqual(value, listKey.ToString());

            var listKey2 = redisDb.ListRightPop(key);
            Assert.AreEqual(value + "E", listKey2.ToString());
        }

        [TestCase("obj")]
        public void HashTest(string key) {
            redisDb.HashSet(key, new HashEntry[] { new HashEntry("one", "first"), new HashEntry("two", "second"), new HashEntry("three", "third") });
            var keys = redisDb.HashKeys(key);
            Assert.AreEqual(3, keys.Length);

            var values = redisDb.HashValues(key);
            Assert.AreEqual(3, values.Length);

            var two = redisDb.HashGet(key, "two");
            Assert.AreEqual("second", two.ToString());
        }

        [TestCase("obj")]
        public void SetTest(string key)
        {
            redisDb.tag(key, new HashEntry[] { new HashEntry("one", "first"), new HashEntry("two", "second"), new HashEntry("three", "third") });
            var keys = redisDb.HashKeys(key);
            Assert.AreEqual(3, keys.Length);

            var values = redisDb.HashValues(key);
            Assert.AreEqual(3, values.Length);

            var two = redisDb.HashGet(key, "two");
            Assert.AreEqual("second", two.ToString());
        }
    }
}
