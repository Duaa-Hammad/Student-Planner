using Microsoft.EntityFrameworkCore;
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
        public async Task<Exam> FindExamByCourseId(int Id)
        {
            var exam = await data.Exams.Where(e => e.CourseId == Id).FirstOrDefaultAsync();
            return exam;
        }
        public async Task DeleteExamByCourseId(int Id)
        {
            var exmas = await data.Exams.Where(r => r.CourseId == Id).ToListAsync();

            if (exmas.Any())
            {
                data.Exams.RemoveRange(exmas);
                await data.SaveChangesAsync();
            }
        }
    }
}
