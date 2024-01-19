using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IIOCManager
    {
        T GetService<T>();
        T GetService<T>(string name);
    }
    public class IOCManager
    {
        private static IServiceProvider _services;

        public static void SetService(IServiceProvider services)
        {
            if (_services == null) _services = services;
        }
        public static T? GetService<T>()
        {
            return _services.GetService<T>();
        }
        public static T GetService<T>(string name) where T : class
        {
            return _services.GetServices<T>().Where(p=>p.GetType().Name==name).FirstOrDefault();
        }
        public static object GetService(string name)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            var type = assembly.GetType(name);
            return _services.GetService(type);
        }
    }
}
