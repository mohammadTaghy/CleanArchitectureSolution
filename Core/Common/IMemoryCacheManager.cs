using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IMemoryCacheManager
    {
        Task AddAsync<T>(string key, T value);
        Task AddAsync<T>(string key, T value, int minuteTimeOut);
        void Add<T>(string key, T value);
        void Add<T>(string key, T value, int minuteTimeOut);
        Task Remove(string key);
        T GetWithKey<T>(string key);
        object Get(string key);

    }
}
