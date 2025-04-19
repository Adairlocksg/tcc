namespace TCC.Business.Interfaces
{
    public interface IUnityOfWork
    {
        Task BeginTransactionAsync();
        Task Commit();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
