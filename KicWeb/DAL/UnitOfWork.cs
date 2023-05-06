namespace KicWeb.DAL
{
    public interface IUnitOfWork : IDisposable 
    {
        IServiceCategoryRepository ServiceCategoryRepository { get; }
        void Commit();
    }
    public class UnitOfWork:IUnitOfWork
    {
        IServiceCategoryRepository _serviceCategoryRepository;
        public UnitOfWork(IServiceCategoryRepository serviceCategoryRepository)
        {
            _serviceCategoryRepository = serviceCategoryRepository;
        }
        public IServiceCategoryRepository ServiceCategoryRepository => _serviceCategoryRepository;
        public void Commit()
        {
            _serviceCategoryRepository.Save();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _serviceCategoryRepository.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
