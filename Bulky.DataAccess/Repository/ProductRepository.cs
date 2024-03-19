using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDBContext _db;
        public ProductRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Product obj)
        {
        //    var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
        //    if (objFromDb != null)
        //    {
               
        //        objFromDb.Title = obj.Title;
        //        objFromDb.ISBN = obj.ISBN;
        //        objFromDb.Author = obj.Author;
        //        objFromDb.ListPrice = obj.ListPrice;
        //        objFromDb.Price = obj.Price;
        //        objFromDb.Price50 = obj.Price50;
        //        objFromDb.Price100 = obj.Price100;
        //        objFromDb.CategoryID = obj.CategoryID;
        //        if (obj.ImageURL != null)
        //        {
        //            objFromDb.ImageURL = obj.ImageURL;
        //        }
        //    }
            _db.Products.Update(obj);
        }
    }
}
