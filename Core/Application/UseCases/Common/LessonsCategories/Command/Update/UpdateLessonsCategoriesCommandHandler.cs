using Application.Common.Exceptions;
using Application.Common.Model;
using AutoMapper;
using Common;
using Domain.Entities;
using FluentValidation.Results;
using Microsoft.OData.ModelBuilder.Core.V1;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Application.UseCases.Common.LessonsCategories.Command.Update
{
    public class UpdateLessonsCategoriesCommandHandler : BaseCommandHandler<UpdateLessonsCategoriesCommand, CommandResponse<Common_LessonsCategories>, ILessonsCategoriesRepo>
    {
        private readonly ILessonsCategoriesRepo _lessonsCategoriesRepo;

        public UpdateLessonsCategoriesCommandHandler(ILessonsCategoriesRepo lessonsCategoriesRepo, IMapper mapper, ICacheManager cacheManager) : base(lessonsCategoriesRepo, mapper, cacheManager)
        {
            _lessonsCategoriesRepo = lessonsCategoriesRepo;
        }

        public override async Task<CommandResponse<Common_LessonsCategories>> Handle(UpdateLessonsCategoriesCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new BadRequestException( string.Format(CommonMessage.NullException, "request"));

            if (await _lessonsCategoriesRepo.AnyEntity(p => p.ParentId == request.ParentId && p.Name == request.Name))
                throw new ValidationException(new ValidationFailure {PropertyName = "Name", ErrorMessage = "The name of the Lessons Categories is repetitive" } );

            Common_LessonsCategories common = _mapper.Map<Common_LessonsCategories>(request);

            if (await _lessonsCategoriesRepo.FindAsync(p=>p.Id== common.Id, cancellationToken) is null)
                throw new NotFoundException("LessonsCategories", common.Id);

            await _repo.Update(common);
            return new CommandResponse<Common_LessonsCategories>(true, common);
        }
    }
}
