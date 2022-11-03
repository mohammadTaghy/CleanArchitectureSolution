using Application.Mappings;
using Application.UseCases.UserProfileCase.Query.GetUserList;
using Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserProfileCase.Query.GetUserItem
{
    public class UserItemDto:UserProfileBaseDto, IMapFrom<Membership_UserProfile>
    {
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Membership_UserProfile, UserItemDto>().
                ForMember(d => d.FullName, s => s.MapFrom(source => source.FirstName + " " + source.LastName))
                .ForMember(d => d.BirthDate, s => s.MapFrom(source =>
                        source.BirthDate != null ? DateTimeHelper.ToLocalDateTimeSh(source.BirthDate.Value) : ""))
                .ForMember(d => d.GenderString, s => s.MapFrom(source => GetGenderString(source.Gender)));
            //profile.CreateMap<List<Membership_UserProfile>, List<UserProfileBaseDto>>();
        }
    }
}
