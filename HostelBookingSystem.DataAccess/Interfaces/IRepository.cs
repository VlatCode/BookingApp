namespace HostelBookingSystem.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        //CRUD
        List<T> GetAll();
        T GetById(int id);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
