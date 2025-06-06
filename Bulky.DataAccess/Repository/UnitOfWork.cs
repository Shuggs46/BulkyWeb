﻿
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.DataAccess.Data;




namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _db;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; } 
        public IOrderDetailRepository OrderDetail { get; private set; }
        public IProductImageRepository ProductImage { get; private set; }

        IProductImageRepository IUnitOfWork.ProductImage => throw new NotImplementedException();

        public UnitOfWork(ApplicationDBContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);           
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            ProductImage = new ProductImageRepository(_db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
