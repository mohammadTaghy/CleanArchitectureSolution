using Microsoft.Extensions.Caching.Memory;
using System;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MemoryCacheManager: IMemoryCacheManager
    {
        private IMemoryCache _memoryCache;

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        #region Manipulate
        public async Task AddAsync<T>(string key, T value)
        {
            if(string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (_memoryCache.Get(key) != null)
                _memoryCache.Remove(key);
             _memoryCache.Set<T>(key, value);
            await Task.Delay(1000);
        }
        public async Task AddAsync<T>(string key, T value, int minuteTimeOut)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (_memoryCache.Get(key) != null)
                _memoryCache.Remove(key);
            _memoryCache.Set<T>(key, value, DateTimeHelper.CurrentMDateTime.AddMinutes(minuteTimeOut));
            await Task.Delay(1000);
        }
        public void Add<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (_memoryCache.Get(key) != null)
                _memoryCache.Remove(key);
            _memoryCache.Set<T>(key, value);
        }
        public void Add<T>(string key, T value, int minuteTimeOut)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (_memoryCache.Get(key) != null)
                _memoryCache.Remove(key);
            _memoryCache.Set<T>(key, value, DateTimeHelper.CurrentMDateTime.AddMinutes(minuteTimeOut));
        }
        public async Task Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            _memoryCache.Remove(key);
            await Task.Delay(1000);
        }
        #endregion
        #region Get
        public T GetWithKey<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }
        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        #endregion
    }
}
