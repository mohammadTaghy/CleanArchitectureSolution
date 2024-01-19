using Common;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Configuration;

namespace Persistence
{
    public class ConnectionHelper
    {
        static ConnectionHelper()
        {
            var basePath = Directory.GetCurrentDirectory();
            
            ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() => {
                return ConnectionMultiplexer.Connect(IOCManager.GetService<IConfiguration>()["RedisURL"]??throw new ArgumentNullException("RedisUrl"));
            });
        }
        private static Lazy<ConnectionMultiplexer> lazyConnection;
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
