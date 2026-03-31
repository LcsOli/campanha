namespace Campaign.Shared.DataBaseContext.Entities.UnityOfWork
{
    public interface IUnityOfWork
    {
        Task SaveAsync();
        Task SecureCommitAsync(Func<Task> func);
    }
}
