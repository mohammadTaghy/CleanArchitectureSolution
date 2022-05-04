using Application.Mappings;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.User.Query.GetUserList
{
    public class UserListDto:IMapFrom<IUser>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int UserCode { get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<IUser, UserListDto>().
                ForMember(m=>m.FullName,m=>m.MapFrom(p=>p.FirstName+" "+p.LastName));
        }
    }
}
