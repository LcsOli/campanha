using Campaign.Shared.Exceptions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;

namespace Campaign.Shared.DataBaseContext.Entities.UnityOfWork
{
    public class UnityOfWork : IUnityOfWork
    {
        private IDbContextTransaction? _transaction;
        private readonly CampaingContextDb _context;
        
      //  private readonly IRegisterTraceService _stackTraceService;

        public UnityOfWork(CampaingContextDb context/*, IRegisterTraceService stackTraceService*/)
        {
            _context = context;
            //_stackTraceService = stackTraceService;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task SecureCommitAsync(Func<Task> func)
        {
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
                
               // _stackTraceService.RegisterTrace(ex);

                throw new CompaignException(HttpStatusCode.InternalServerError, "Ocorreu um erro ao persistir uma entidade no banco.");
            }
        }
    }
}