using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public interface IPersistanceDBContext
    {
        DbSet<IUser> Users { get; set; }
    }
}
