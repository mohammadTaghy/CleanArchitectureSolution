using Application.Mappings;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Common;
using Common.Extention;

namespace Application.UseCases.UserProfileCase.Query.GetUserList
{
    public class UserProfileListDto :UserProfileBaseDto,IMapFrom<Membership_UserProfile>
    {
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Membership_UserProfile, UserProfileListDto>().
                ForMember(d => d.FullName, s => s.MapFrom(source => source.FirstName + " " + source.LastName))
                .ForMember(d => d.BirthDate, s => s.MapFrom(source =>
                        source.BirthDate != null ? DateTimeHelper.ToLocalDateTimeSh(source.BirthDate.Value) : ""))
                .ForMember(d => d.GenderString, s => s.MapFrom(source => GetGenderString(source.Gender)));
            //profile.CreateMap<List<Membership_UserProfile>, List<UserProfileBaseDto>>();
        }
    }
}
