namespace TCC.Business.Interfaces
{
    public interface IUnityOfWork
    {
        Task Commit();
    }
}
