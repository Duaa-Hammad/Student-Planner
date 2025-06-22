using Microsoft.EntityFrameworkCore;
using StudentPlanner.BLL.Interfaces;
using StudentPlanner.BLL.Models;
using StudentPlanner.DAL.Database;
using StudentPlanner.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Repository
{
    public class AssignmentRepo : IAssignment
    {
        private readonly StPlannerContext data;
        public AssignmentRepo(StPlannerContext data)
        {
            this.data = data;
        }
        public async Task AddAssignment(Assignment model)
        {
            await data.Assignments.AddAsync(model);
            await data.SaveChangesAsync();
        }
        public async Task<Assignment> FindAssignmentByCourseId(int Id)
        {
            var assignment = await data.Assignments.Where(e => e.CourseId == Id).FirstOrDefaultAsync();
            return assignment;
        }
        public async Task DeleteAssignmentByCourseId(int Id)
        {
            var assignments = await data.Assignments
             .Where(r => r.CourseId == Id)
             .ToListAsync();

            if (assignments.Any())
            {
                data.Assignments.RemoveRange(assignments);
                await data.SaveChangesAsync();
            }
        }
        public async Task DeleteAssignmentById(int? Id)
        {
            var assignment =  await data.Assignments.Where(data => data.Id == Id).FirstOrDefaultAsync();
            data.Assignments.Remove(assignment);
            await data.SaveChangesAsync();
        }

    }
}
