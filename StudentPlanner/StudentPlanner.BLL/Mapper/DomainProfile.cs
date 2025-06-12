using AutoMapper;
using StudentPlanner.BLL.Models;
using StudentPlanner.DAL.Entities;
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
        }
    }
}
