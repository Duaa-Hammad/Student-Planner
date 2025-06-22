using StudentPlanner.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentPlanner.BLL.Interfaces
{
    public interface IAssignment
    {
        public Task AddAssignment(Assignment model);
        public Task DeleteAssignmentByCourseId(int Id);
        public Task<Assignment> FindAssignmentByCourseId(int Id);
        public Task DeleteAssignmentById(int? Id);
    }
}
