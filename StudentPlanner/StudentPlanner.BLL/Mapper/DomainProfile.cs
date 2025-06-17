using AutoMapper;
using StudentPlanner.BLL.Models;
using StudentPlanner.DAL.Entities;
using StudentPlanner.DAL.Extends;
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
            CreateMap<CourseVM, Course>();
            CreateMap<Course, CourseVM>();

            //When filling ApplicationUser.UserName, use the Email from RegistrationVM.
            CreateMap<RegistrationVM, ApplicationUser>().
            ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<ReminderVM, Reminder>();
            CreateMap<Reminder, ReminderVM>();
        }
    }
}
