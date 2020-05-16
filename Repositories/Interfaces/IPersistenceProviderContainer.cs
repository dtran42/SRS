using OptimaJet.Workflow.Core.Persistence;

namespace SRS.Repositories.Interfaces
{
    public interface IPersistenceProviderContainer
    {
        IWorkflowProvider Provider { get; }
    }
}
