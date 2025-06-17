using StudentPlanner.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Interfaces
{
    public interface IExam
    {
        public Task AddExam(Exam model);
    }
}
