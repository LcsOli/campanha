using System.Net;
using Campaign.Shared.Exceptions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Campaign.Shared.DataBaseContext.Entities.UnityOfWork
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

        public async Task SecureCommitAsync(Func<Task> func)
        {
            //TODO - Verificar a possibilidade de refatorar o código para evitar a repetição do bloco try/catch, visto que ele é o mesmo para ambos os métodos.

            try
            {
                _transaction = await _context.Database.BeginTransactionAsync();
                await func();
                await _context.Database.CommitTransactionAsync();
            }
            catch (CompaignCollectionMessagesExceptions ex)
            {
                await _transaction!.RollbackAsync();
                throw new CompaignCollectionMessagesExceptions(ex.Code, ex.Messages);
            }
            catch (CompaignException ex)
            {
                await _transaction!.RollbackAsync();
                throw new CompaignException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                await _transaction!.RollbackAsync();
                throw new CompaignException(HttpStatusCode.InternalServerError, "Ocorreu um erro ao persistir uma entidade no banco.");
            }
        }
    }
}
