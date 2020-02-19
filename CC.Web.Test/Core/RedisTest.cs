using NUnit.Framework;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Web.Test.Core
{
    public class RedisTest
    {
        private IRedisTypedClient<RedisClass> RedisClassRedis { get; set; }

        private List<RedisClass> RedisClasses { get; set; }

        [SetUp]
        public void Setup()
        {
            var redis = new RedisClient(RedisConfig.DefaultHost,RedisConfig.DefaultPort);
            RedisClassRedis = redis.As<RedisClass>();
            RedisClasses = new List<RedisClass>();
            RedisClasses.Add(new RedisClass()
            {
                Id = RedisClassRedis.GetNextSequence(),
                Name = "测试1",
                Age = 25,
            });
            RedisClasses.Add(new RedisClass()
            {
                Id = RedisClassRedis.GetNextSequence(),
                Name = "Test2",
                Age = 10,
            });
            RedisClassRedis.StoreAll(RedisClasses);
        }

        [Test]
        public void GetTest()
        {
            var list = RedisClassRedis.GetAll();
        }
    }
}
