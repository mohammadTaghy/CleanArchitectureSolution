using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RedisConfig
    {
        public string RedisURL { get; set; }
    }
    public class ConnectionConfig
    {
        public string ConnectionStrings { get; set; }

    }
    public class Loggin
    {
        public int MyProperty { get; set; }
    }
    public class AppSettings
    {
        public string SecretKey { get; set; }
    }
}
