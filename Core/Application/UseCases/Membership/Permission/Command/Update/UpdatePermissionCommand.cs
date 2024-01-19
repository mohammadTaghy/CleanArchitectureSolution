using Application.Common.Model;
using Application.Mappings;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Membership.Permission.Command.Update
{
    public class UpdatePermissionCommand : IRequest<CommandResponse<Membership_Permission>>,IMapFrom<Membership_Permission>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string CommandName { get; set; }
        public byte FeatureType { get; set; }
        public bool IsActive { get; set; }
        public int ParentId { get; set; }
        public string IConPath { get; set; }

        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<UpdatePermissionCommand, Membership_Permission>();
        }

    }
}
