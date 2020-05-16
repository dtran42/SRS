using System;
using System.Collections.Generic;

namespace SRS.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Models.ApplicationUser> GetAll();
        string GetNameById(Guid id);
        IEnumerable<string> GetInRole(string roleName);
        bool CheckRole(Guid employeeId, string roleName);
    }
}
