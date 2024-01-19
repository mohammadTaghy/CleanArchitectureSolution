using Application.Common.Model;
using Application.Mappings;
using Domain;
using Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Common.LessonsCategories.Command.Create
{
    public class CreateLessonsCategoriesCommand : IRequest<CommandResponse<Common_LessonsCategories>>,IMapFrom<Common_LessonsCategories>
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
            profile.CreateMap<CreateLessonsCategoriesCommand, Common_LessonsCategories>();
        }
    }
}
