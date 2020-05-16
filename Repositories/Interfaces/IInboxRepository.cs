using OptimaJet.Workflow.Core.Runtime;
using System;

namespace SRS.Repositories.Interfaces
{
    public interface IInboxRepository
    {
        void DropWorkflowInbox(Guid processId);
        void FillInbox(Guid processId, WorkflowRuntime workflowRuntime);
        void RecalcInbox(WorkflowRuntime workflowRuntime);
    }
}
