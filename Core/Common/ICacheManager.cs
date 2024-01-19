using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ICacheManager
    {
        Task<bool> AddAsync<T>(string key, T value);
        Task<bool> AddAsync<T>(string key, T value, int minuteTimeOut);
        bool Add<T>(string key, T value);
        bool Add<T>(string key, T value, int minuteTimeOut);
        Task<bool> UpdateAsync<T>(string key, T value);
        bool Update<T>(string key, T value);
        bool Remove(string key);
        Task<bool> RemoveAsync(string key);
        T GetWithKey<T>(string key);
        Task<T> GetWithKeyAsync<T>(string key);
        object Get(string key);

    }
}
