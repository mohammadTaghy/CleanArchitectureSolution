using Application.Mappings;
using Common;
using Common.Extention;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserProfileCase.Query
{
    public class UserProfileBaseDto : IMapFrom<Membership_UserProfile>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string MobileNumber { get; set; }
        public bool IsMobileNumberConfirmed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsUserConfirm { get; set; }
        public byte ManagerConfirm { get; set; }
        public byte Gender { get; set; }
        public string FullName { get; set; }
        public string GenderString { get; set; }
        public int UserCode { get; set; }
        public string? PostalCode { get; set; }
        public string? PicturePath { get; set; }
        public string BirthDate { get; set; }
        public string? EducationGrade { get; set; }
        public string? NationalCode { get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Membership_UserProfile, UserProfileBaseDto>().
                ForMember(d => d.FullName, s => s.MapFrom(source => source.FirstName + " " + source.LastName))
                .ForMember(d => d.BirthDate, s => s.MapFrom(source =>
                        source.BirthDate != null ? DateTimeHelper.ToLocalDateTimeSh(source.BirthDate.Value) : ""))
                .ForMember(d => d.GenderString, s => s.MapFrom(source => GetGenderString(source.Gender)))
                ;
        }

        private object GetGenderString(byte gender)
        {
            switch (gender)
            {
                case (byte)Constants.Gender.Male:
                    return Constants.Gender.Male.DisplayName();
                case (byte)Constants.Gender.Female:
                    return Constants.Gender.Female.DisplayName();
                default:
                    return Constants.Gender.Male.DisplayName();
            }
        }
    }
}
