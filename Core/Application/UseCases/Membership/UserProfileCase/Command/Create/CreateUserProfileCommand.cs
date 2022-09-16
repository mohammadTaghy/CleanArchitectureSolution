using Application.Common.Model;
using Application.Mappings;
using Application.UseCases.UserCase.Command;
using Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserProfileCase.Command.Create
{
    public class CreateUserProfileCommand: UserCommandBase, IRequest<CommandResponse<int>>, IMapFrom<UserProfile>
    {
        public int? UserId { get; set; }
        public byte Gender { get; set; }
        public string? PostalCode { get; set; }
        public string? PicturePath { get; set; }
        public string BirthDate { get; set; }
        public string? EducationGrade { get; set; }
        public string? UserDescription { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? NationalCode { get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<CreateUserProfileCommand, UserProfile>().
                ForMember(t=>t.BirthDate,s=>s.MapFrom(source=>
                        !string.IsNullOrEmpty(source.BirthDate)?DateTimeHelper.ToDateTime(source.BirthDate):DateTimeHelper.CurrentMDateTime));
        }
    }
}
