using Application.Common;
using Application.Mappings;
using Domain;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Users.Command.Create
{
    public class CreateUserCommand:IRequest<CommandRequest<int>>,IMapFrom<User>
    {
        
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserCode { get; set; }
        public string NationalCode { get; set; }
        public string MobileNumber { get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<CreateUserCommand, UserRead>();
        }
    }
}
