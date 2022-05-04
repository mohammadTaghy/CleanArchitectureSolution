using System;

namespace Application.Decorators
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DatabaseRetryAttribute : Attribute
    {
        public DatabaseRetryAttribute()
        {
        }
    }
}
