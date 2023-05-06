using KicWeb.Data;
using KicWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace KicWeb.DAL
{
    public interface IServiceCategoryRepository : IDisposable
    {
        ServiceCategory GetServiceCategoryById(int? id);
        IEnumerable<ServiceCategory> GetServiceCategories();
        void AddServiceCategory(ServiceCategory serviceCategory);
        void UpdateServiceCategory(ServiceCategory serviceCategory);
        void RemoveServiceCategory(ServiceCategory serviceCategory);
        int Save();
    }
    public class ServiceCategoryRepository : IServiceCategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public ServiceCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public ServiceCategory GetServiceCategoryById(int? id)
        {
            return _db.ServiceCategories.Find(id);
        }
        public IEnumerable<ServiceCategory> GetServiceCategories()
        {
            return _db.ServiceCategories.ToList();
        }
        public void AddServiceCategory(ServiceCategory serviceCategory)
        {
            _db.ServiceCategories.Add(serviceCategory);
        }
        public void UpdateServiceCategory(ServiceCategory serviceCategory)
        {
            _db.ServiceCategories.Update(serviceCategory);
            //_db.Entry(serviceCategory).State = EntityState.Modified;
        }
        public void RemoveServiceCategory(ServiceCategory serviceCategory)
        {
            _db.ServiceCategories.Remove(serviceCategory);
        }
        public int Save()
        {
            return _db.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
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
