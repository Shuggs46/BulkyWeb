using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;


namespace BulkyBook.DataAccess.Repository
{

    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository

    {
        private readonly ApplicationDBContext _db;
        public ShoppingCartRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public void Update(ShoppingCart obj)
        {
            _db.ShoppingCarts.Update(obj);
        }

    }
}

