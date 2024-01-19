using Application.Common.Exceptions;
using Application.Common.Model;
using AutoMapper;
using Common;

namespace Application.UseCases.Membership.Permission.Command.Delete
{
    public class DeletePermissionCommandHandler : BaseCommandHandler<DeletePermissionCommand, CommandResponse<bool>, IPermissionRepo>
    {
        private readonly IRolesRepo _rolesRepo;

        public DeletePermissionCommandHandler(IPermissionRepo repo, IMapper mapper,IRolesRepo rolesRepo, ICacheManager cacheManager) : base(repo, mapper, cacheManager)
        {
            _rolesRepo = rolesRepo;
        }

        public override async Task<CommandResponse<bool>> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            if(request is null) throw new BadRequestException(string.Format(CommonMessage.NullException, nameof(request)));

            if (await _rolesRepo.AnyEntity(p => p.RolesPermission.Contains(p.RolesPermission.FirstOrDefault(q=>q.PermissionId == request.Id))))
                throw new DeleteFailureException("نقش", request.Id);

            if (!await _repo.DeleteItem(request.Id))
                throw new DeleteFailureException(nameof(request.Id), request.Id);

            return new CommandResponse<bool>(true);

        }
    }
}
