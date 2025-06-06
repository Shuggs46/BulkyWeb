﻿using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;



namespace Bulky.DataAccess.Repository
{

    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDBContext _db;
        public OrderDetailRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        
        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}

