using AutoMapper;
using StudentPlanner.BLL.Models;
using StudentPlanner.DAL.Entities;
<<<<<<< HEAD
using StudentPlanner.DAL.Extends;
=======
>>>>>>> 75dd13d (Created Mapper and Ojbect Lifetime)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            // Add your mappings here
            // CreateMap<SourceEntity, DestinationEntity>();
<<<<<<< HEAD
            CreateMap<CourseVM, Course>();
            CreateMap<Course, CourseVM>();

            //When filling ApplicationUser.UserName, use the Email from RegistrationVM.
            CreateMap<RegistrationVM, ApplicationUser>().
            ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<ReminderVM, Reminder>().ForMember(dest => dest.Course, opt => opt.Ignore());

            CreateMap<Reminder, ReminderVM>();

            CreateMap<StudentVM, Student>()
                .ForMember(dest => dest.IdentityUserId, opt => opt.Ignore())
                .ForMember(dest => dest.IdentityUser, opt => opt.Ignore());
            CreateMap<Student, StudentVM>()
                .ForMember(dest => dest.IdentityUserId, opt => opt.Ignore());
=======
             CreateMap<CourseVM, Course>();
            CreateMap<Course, CourseVM>();
>>>>>>> 75dd13d (Created Mapper and Ojbect Lifetime)
        }
    }
}
