using Microsoft.EntityFrameworkCore;
using SRS.Data;
using SRS.Models;
using SRS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.Repositories
{
    public class WorkOrderRepository : IWorkOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public WorkOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void ChangeState(Guid id, string nextState, string nextStateName)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmptyPreHistory(Guid processId)
        {
            throw new NotImplementedException();
        }

        public List<WorkOrder> Get(out int count, int page = 0, int pageSize = 128)
        {
            throw new NotImplementedException();
        }

        public WorkOrder Get(Guid id, string nextState, string nextStateName)
        {
            throw new NotImplementedException();
        }

        public List<WorkOrderTransitionHistory> GetHistory(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<WorkOrder> GetInbox(Guid identityId, out int count, int page = 0, int pageSize = 128)
        {
            throw new NotImplementedException();
        }

        public List<WorkOrder> GetOutbox(Guid identityId, out int count, int page = 0, int pageSize = 128)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetPermisionUsers(int woId)
        {
            throw new NotImplementedException();
        }

        public bool HasPermision(int woId, Guid identityId)
        {
            throw new NotImplementedException();
        }

        public WorkOrder InsertOrUpdate(WorkOrder workOrder)
        {
            throw new NotImplementedException();
        }

        public void UpdateTransitionHistory(Guid id, string currentState, string nextState, string command, Guid? employeeId)
        {
            throw new NotImplementedException();
        }

        public void WriteTransitionHistory(Guid id, string currentState, string nextState, string command, IEnumerable<string> identities)
        {
            throw new NotImplementedException();
        }

        private WorkOrder GetWorkOrder(int id, bool loadChildEntities = true) 
        {
            WorkOrder wo = null;
            if (!loadChildEntities)
            {
                wo = _context.WorkOrders.FirstOrDefault(w => w.Id == id);
            }
            else
            {
                wo = _context.WorkOrders
                    .Include(w => w.PortfolioManager)
                    .Include(w => w.AltPortfolioManager)
                    .Include(w => w.CreatedBy)
                    .Include(w => w.Requestor)
                    .FirstOrDefault(w => w.Id == id);
            }

            return wo;
        }
    }
}
