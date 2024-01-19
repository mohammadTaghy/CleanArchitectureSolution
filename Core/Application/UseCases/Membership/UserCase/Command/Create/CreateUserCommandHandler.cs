using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Model;
using Application.Validation;
using AutoMapper;
using Common;
using Domain;
using Domain.Entities;
using Domain.Model;
using MediatR;
using Newtonsoft.Json;

namespace Application.UseCases.UserCase.Command.Create
{

    public class CreateUserCommandHandler : BaseCommandHandler<CreateUserCommand, CommandResponse<int>, IUserRepo>
    {

        public CreateUserCommandHandler(IUserRepo userRepo, IMapper mapper, ICacheManager cacheManager)
            :base(userRepo, mapper, cacheManager)
        {
        }

        public override async Task<CommandResponse<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
                throw new BadRequestException(string.Format(CommonMessage.NullException, "request"));

            Membership_User user = _mapper.Map<Membership_User>(request);
            bool existUser = await _repo.AnyEntity(p=>p.UserName==request.UserName);

            if (existUser)
                throw new ValidationException(new List<FluentValidation.Results.ValidationFailure>
                {
                    new FluentValidation.Results.ValidationFailure(nameof(Membership_User.UserName), String.Format(CommonMessage.IsDuplicateUserName, user.UserName))
                });
            await _repo.Insert(request);
            //_rabbitMQUtility.SendMessage("CQRS", JsonConvert.SerializeObject(new RabbitMQMessageModel
            //{
            //    ChangedType = (byte)Constants.ChangedType.Create,
            //    AggregateId = user.Id,
            //    AssemblyFullName = typeof(IUserRepoRead).AssemblyQualifiedName,
            //    Body = JsonConvert.SerializeObject(user)
            //}));
            ///Call event for insert into read DB
            return await Task.FromResult(new CommandResponse<int>(true, user.Id));
        }

    }

}
