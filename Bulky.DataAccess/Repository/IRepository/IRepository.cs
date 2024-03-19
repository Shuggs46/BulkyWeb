using System.Linq.Expressions;


namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T-Category
        IEnumerable<T> GetAll(string? includeProperties = null);        
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);


        //IEnumerable<T> Get(int id);
        //IEnumerable<T> GetFirstorDefault(int id);
        //IEnumerable<T> Get(string id);

    }
}
