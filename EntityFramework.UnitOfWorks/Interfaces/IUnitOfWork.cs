using Microsoft.EntityFrameworkCore;

namespace EntityFramework.UnitOfWorks.Interfaces
{
    public interface IUnitOfWork
    {
        #region Transactions

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        #endregion
    }
}