using Common;
using StackExchange.Redis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class RedisCacheService : ICacheManager
    {
        private IDatabase _db;
        public RedisCacheService()
        {
            //ConfigureRedis();
        }
        private void ConfigureRedis()
        {
            _db = ConnectionHelper.Connection.GetDatabase();
        }
        public bool Add<T>(string key, T value)
        {
            TimeSpan expiryTime = DateTimeHelper.CurrentMDateTime.AddHours(1).Subtract(DateTimeHelper.CurrentMDateTime);
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }

        public bool Add<T>(string key, T value, int minuteTimeOut)
        {
            TimeSpan expiryTime = DateTimeHelper.CurrentMDateTime.AddMinutes(minuteTimeOut).Subtract(DateTimeHelper.CurrentMDateTime);
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }

        public async Task<bool> AddAsync<T>(string key, T value)
        {
            TimeSpan expiryTime = DateTimeHelper.CurrentMDateTime.AddHours(1).Subtract(DateTimeHelper.CurrentMDateTime);
            return await _db.StringSetAsync(key, JsonConvert.SerializeObject(value), expiryTime);
        }

        public async Task<bool> AddAsync<T>(string key, T value, int minuteTimeOut)
        {
            TimeSpan expiryTime = DateTimeHelper.CurrentMDateTime.AddMinutes(minuteTimeOut).Subtract(DateTimeHelper.CurrentMDateTime);
            return await _db.StringSetAsync(key, JsonConvert.SerializeObject(value), expiryTime);
        }

        public object Get(string key)
        {
            var value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<object>(value);
            }
            return default;
        }

        public T GetWithKey<T>(string key)
        {
            if (_db.KeyExists(key))
            {
                var rediskey=new RedisKey(key);
                var value = _db.StringGet(rediskey);
                if (!string.IsNullOrEmpty(value))
                {
                    return JsonConvert.DeserializeObject<T>(value);
                }
            }
            return default;
        }
        public async Task<T> GetWithKeyAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }
        public bool Remove(string key)
        {
            bool _isKeyExist = _db.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _db.KeyDelete(key);
            }
            return false;
        }
        public async Task<bool> RemoveAsync(string key)
        {
            bool _isKeyExist = await _db.KeyExistsAsync(key);
            if (_isKeyExist == true)
            {
                return await _db.KeyDeleteAsync(key);
            }
            return false;
        }

        public async Task<bool> UpdateAsync<T>(string key, T value)
        {
            TimeSpan expiryTime = DateTimeHelper.CurrentMDateTime.AddHours(1).Subtract(DateTimeHelper.CurrentMDateTime);
             await _db.StringSetAsync(new RedisKey(key), new RedisValue(JsonConvert.SerializeObject(value)));
            return true;
        }

        public bool Update<T>(string key, T value)
        {
            TimeSpan expiryTime = DateTimeHelper.CurrentMDateTime.AddHours(1).Subtract(DateTimeHelper.CurrentMDateTime);
             _db.StringSet(new RedisKey(key), new RedisValue(JsonConvert.SerializeObject(value)));
            return true;
        }
    }
}
