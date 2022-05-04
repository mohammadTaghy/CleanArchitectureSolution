using System;

namespace Application.Decorators
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AuditLogAttribute : Attribute
    {
        public AuditLogAttribute()
        {
        }
    }
}
