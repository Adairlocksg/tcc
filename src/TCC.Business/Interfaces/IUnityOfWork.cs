namespace TCC.Business.Interfaces
{
    public interface IUnityOfWork
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
