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
        public void Setup()
        {
            var conn = ConnectionMultiplexer.Connect("localhost,password=a3101170z");
            redisDb = conn.GetDatabase();
        }

        [TestCase("SEK", "SEV", 2)]
        public void StringTest(string key, string value, int second)
        {
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

            redisDb.StringAppend("countWord", "bc");
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
            redisDb.ListLeftPush(key, value.Substring(0, 1));
            redisDb.ListRightPush(key, value.Insert(1, "O") + "E");

            var rangeList = redisDb.ListRange(key, 1, 2);
            Assert.AreEqual(2, rangeList.Length);

            var listKey = redisDb.ListGetByIndex(key, 0);
            Assert.AreEqual("L", listKey.ToString());

            redisDb.ListLeftPop(key);
            var listKey2 = redisDb.ListRightPop(key);
            Assert.AreEqual("LOVE", listKey2.ToString());
        }

        [TestCase("obj")]
        public void HashTest(string key)
        {
            redisDb.HashSet(key, new HashEntry[] { new HashEntry("one", "first"), new HashEntry("two", "second"), new HashEntry("three", "third") });
            var keys = redisDb.HashKeys(key);
            Assert.AreEqual(3, keys.Length);

            var values = redisDb.HashValues(key);
            Assert.AreEqual(3, values.Length);

            var two = redisDb.HashGet(key, "two");
            Assert.AreEqual("second", two.ToString());
        }

        [TestCase("set1", "set2")]
        public void SetTest(string key1, string key2)
        {
            redisDb.SetAdd(key1, new RedisValue[] { "card1", "card2", "card3" });
            redisDb.SetAdd(key2, new RedisValue[] { "card3", "card4", "card5" });

            var differenceSet = redisDb.SetCombine(SetOperation.Difference, key1, key2);
            var IntersectSet = redisDb.SetCombine(SetOperation.Intersect, key1, key2);
            var UnionSet = redisDb.SetCombine(SetOperation.Union, key1, key2);

            Assert.AreEqual(2, differenceSet.Length);
            Assert.AreEqual(1, IntersectSet.Length);
            Assert.AreEqual(5, UnionSet.Length);
        }

        [TestCase("SortedSet1")]
        public void SortedSetTest(string key)
        {
            redisDb.SortedSetAdd(key, "card1", 1);
            redisDb.SortedSetAdd(key, "card3", 3);
            redisDb.SortedSetAdd(key, "card2", 2);

            var withScoresSet = redisDb.SortedSetRangeByRankWithScores(key, 0, -1);

            Assert.AreEqual("card2", withScoresSet[1].Element.ToString());
            Assert.AreEqual((double)2, withScoresSet[1].Score);
        }
    }
}
