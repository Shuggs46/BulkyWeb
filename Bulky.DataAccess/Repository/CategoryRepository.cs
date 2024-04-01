using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System.Linq.Expressions;


namespace BulkyBook.DataAccess.Repository
{

    public class CategoryRepository : Repository<Category>, ICategoryRepository
    //public class CategoryRepository : Repository<Category>
    {
        private readonly ApplicationDBContext _db;
        public CategoryRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Add(OrderHeader entity)
        {
            throw new NotImplementedException();
        }

        public OrderHeader Get(Expression<Func<OrderHeader, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderHeader> GetAll(Expression<Func<OrderHeader, bool>>? filter = null, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public void Remove(OrderHeader entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<OrderHeader> entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }

        public void Update(OrderHeader obj)
        {
            throw new NotImplementedException();
        }
    }
}

