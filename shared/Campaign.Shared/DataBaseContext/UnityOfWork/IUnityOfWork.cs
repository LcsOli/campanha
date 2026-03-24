namespace Campaign.Shared.DataBaseContext.Entities.UnityOfWork
{
    public interface IUnityOfWork
    {
        Task SaveAsync();
        void SecureCommitAsync(Func<Task> func);
    }
}
