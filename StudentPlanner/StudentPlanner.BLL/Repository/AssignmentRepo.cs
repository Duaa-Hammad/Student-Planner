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

    }
}
