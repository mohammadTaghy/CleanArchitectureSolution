using Application.Common.Exceptions;
using Application.Common.Model;
using AutoMapper;
using Common;
using Domain.Entities;

namespace Application.UseCases.Common.LessonsCategories.Command.Create
{
    public class CreateLessonsCategoriesCommandHandler : BaseCommandHandler<CreateLessonsCategoriesCommand, CommandResponse<Common_LessonsCategories>, ILessonsCategoriesRepo>
    {
        public CreateLessonsCategoriesCommandHandler(ILessonsCategoriesRepo lessonsCategoriesRepo, IMapper mapper, ICacheManager cacheManager) : base(lessonsCategoriesRepo, mapper, cacheManager)
        {

        }

        public override async Task<CommandResponse<Common_LessonsCategories>> Handle(CreateLessonsCategoriesCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new BadRequestException( string.Format(CommonMessage.NullException, "request"));

            Common_LessonsCategories permission = _mapper.Map<Common_LessonsCategories>(request);

            await _repo.Insert(permission);
            return new CommandResponse<Common_LessonsCategories>(true, permission);

        }
    }
}
