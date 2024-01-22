using Application.Common.Model;
using Application.Mappings;
using Domain;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Common.LessonsCategories.Command.Update
{
    public class UpdateLessonsCategoriesCommand : IRequest<CommandResponse<Common_LessonsCategories>>,IMapFrom<Common_LessonsCategories>
    {
        [Column(IsRequired = true, Title = "نام")]
        public string Name { get; set; }
        [Column(IsRequired = true, Title = "عنوان")]
        public string Title { get; set; }
        [Column(Title = "آدرس آیکن")]
        public string IConPath { get; set; }
        [Column(IsRequired = true, Title = "فعال")]
        public bool IsActive { get; set; }
        [Column(IsRequired = false, Title = "ضریب")]
        public bool Coefficient { get; set; }
        [Column(IsRequired = false, Title = "نوع دسته بندی")]
        public byte CategoryType { get; set; }
        [Column(IsRequired = false, Title = "کد رایانه پدر")]
        public int? ParentId { get; set; }

        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<UpdateLessonsCategoriesCommand, Common_LessonsCategories>();
        }
    }
}
