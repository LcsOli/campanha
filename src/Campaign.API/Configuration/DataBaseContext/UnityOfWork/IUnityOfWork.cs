namespace Campaign.API.Configuration.DataBaseContext.UnityOfWork
{
    public interface IUnityOfWork
    {
        Task SaveAsync();
        void SecureCommitAsync(Func<Task> func);
    }
}
