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

namespace Application.UseCases.Membership.UserCase
{
    public class UserDto : IMapFrom<Membership_User>
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
        public string UserCode { get; set; }
        public string? PostalCode { get; set; }
        public string? PicturePath { get; set; }
        public string BirthDate { get; set; }
        public string? EducationGrade { get; set; }
        public string? NationalCode { get; set; }

        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Membership_User, UserDto>().
                ForMember(d => d.FullName, s => s.MapFrom(source => source.UserProfile != null ?
                    source.UserProfile.FirstName + " " + source.UserProfile.LastName :
                    ""))
                .ForMember(d => d.BirthDate, s => s.MapFrom(source =>
                        source.UserProfile != null && source.UserProfile.BirthDate != null ?
                            DateTimeHelper.ToLocalDateTimeSh(source.UserProfile.BirthDate.Value) :
                            ""))
                .ForMember(d => d.GenderString, s => s.MapFrom(source => Constants.GetGenderString(source.UserProfile.Gender)))
                .ForMember(d => d.PicturePath, s => s.MapFrom(source => source.UserProfile.PicturePath))
                .ForMember(d => d.FirstName, s => s.MapFrom(source => source.UserProfile.FirstName))
                .ForMember(d => d.Gender, s => s.MapFrom(source => source.UserProfile.Gender))
                .ForMember(d => d.LastName, s => s.MapFrom(source => source.UserProfile.LastName))
                .ForMember(d => d.PostalCode, s => s.MapFrom(source => source.UserProfile.PostalCode))
                .ForMember(d => d.NationalCode, s => s.MapFrom(source => source.UserProfile.NationalCode))
                ;
        }
    }
}
