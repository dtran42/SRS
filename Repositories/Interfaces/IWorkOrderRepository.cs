using SRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Repositories.Interfaces
{
    public interface IWorkOrderRepository
    {        
        //bool IsAuthorsBoss(Guid documentId, Guid identityId);
        //IEnumerable<string> GetAuthorsBoss(Guid documentId);

        WorkOrder InsertOrUpdate(WorkOrder workOrder);
        void DeleteEmptyPreHistory(Guid processId);
        List<WorkOrder> Get(out int count, int page = 0, int pageSize = 128);
        List<WorkOrder> GetInbox(Guid identityId, out int count, int page = 0, int pageSize = 128);
        List<WorkOrder> GetOutbox(Guid identityId, out int count, int page = 0, int pageSize = 128);
        List<WorkOrderTransitionHistory> GetHistory(Guid id);
        WorkOrder Get(Guid id, string nextState, string nextStateName);
        void Delete(Guid[] ids);
        void ChangeState(Guid id, string nextState, string nextStateName);
        void WriteTransitionHistory(Guid id, string currentState, string nextState, string command, IEnumerable<string> identities);
        void UpdateTransitionHistory(Guid id, string currentState, string nextState, string command, Guid? employeeId);
    }
}
