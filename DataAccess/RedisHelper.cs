using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.IO;

namespace DataAccess
{
    public class RedisHelper : IDataAccess
    {
        private ConnectionMultiplexer redisClient;

        public RedisHelper(string connectionString)
        {
            this.redisClient = ConnectionMultiplexer.Connect(connectionString);
        }

        public string GetValue(string key)
        {
            IDatabase db = redisClient.GetDatabase();
            return db.StringGet(key);
        }

        public void SetValue(string key, string value)
        {
            IDatabase db = redisClient.GetDatabase();
            db.StringSet(key, value);
        }
    }
}
