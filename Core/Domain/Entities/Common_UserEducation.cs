using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Common_UserEducation:Entity
    {
        public int Id { get; set; }
        public int LessonsCategoriesId { get; set; }
        public int UserId { get; set; }
        public Common_LessonsCategories LessonsCategories { get; set; }
        public Membership_User User { get; set; }
    }
}
