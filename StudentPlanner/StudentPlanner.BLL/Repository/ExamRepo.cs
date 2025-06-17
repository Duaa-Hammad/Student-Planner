using StudentPlanner.BLL.Interfaces;
using StudentPlanner.DAL.Database;
using StudentPlanner.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Repository
{
    public class ExamRepo : IExam
    {
        private readonly StPlannerContext data;
        public ExamRepo(StPlannerContext data)
        {
            this.data = data;
        }
        public async Task AddExam(Exam model)
        {
            await data.Exams.AddAsync(model);
            await data.SaveChangesAsync();
        }
    }
}
