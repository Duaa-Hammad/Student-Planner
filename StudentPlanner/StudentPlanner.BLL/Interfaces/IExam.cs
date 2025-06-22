using StudentPlanner.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentPlanner.BLL.Interfaces
{
    public interface IExam
    {
        public Task AddExam(Exam model);
        public Task DeleteExamByCourseId(int Id);
        public Task<Exam> FindExamByCourseId(int Id);
        public Task DeleteExamById(int? Id);
    }
}
