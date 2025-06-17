using StudentPlanner.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.BLL.Interfaces
{
    public interface IAssignment
    {
        public Task AddAssignment(Assignment model);
    }
}
