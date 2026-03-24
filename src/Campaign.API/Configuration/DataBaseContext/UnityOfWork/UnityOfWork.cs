using System.Net;
using Campaign.API.Configuration.Exceptions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Campaign.API.Configuration.DataBaseContext.UnityOfWork
{
    public class UnityOfWork : IUnityOfWork
    {
        private IDbContextTransaction? _transaction;
        private readonly CampaingContextDb _context;

        public UnityOfWork(CampaingContextDb context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async void SecureCommitAsync(Func<Task> func)
        {
            try
            {
                _transaction = await _context.Database.BeginTransactionAsync();
                await func();
                await _context.Database.CommitTransactionAsync();
            }
            catch (CompaignException ex)
            {
                throw new CompaignException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                throw new CompaignException(HttpStatusCode.InternalServerError, "Ocorreu um erro ao persistir uma entidade no banco.");
            }
            finally
            {

                //TODO - Verificar se sempre vai cair aqui
                await _transaction!.RollbackAsync();
            }
        }
    }
}
