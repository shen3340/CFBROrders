namespace CFBROrders.SDK.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Start new transaction
        void BeginTransaction();
        // Commit transaction
        void Commit();
        // Rollback transaction
        void Rollback();
    }
}