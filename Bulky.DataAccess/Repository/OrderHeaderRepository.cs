using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System.Linq.Expressions;


namespace BulkyBook.DataAccess.Repository
{

    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDBContext _db;
        public OrderHeaderRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
                _db.OrderHeaders.Update(obj);        
        }

        public void UpdateStatus(int Id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == Id);
            if (orderFromDb !=null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStripePaymentId(int Id, string sessionId, string paymentIntentId = null)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == Id);
            if (!string.IsNullOrEmpty(sessionId))
            {
                orderFromDb.SessionId = sessionId;

            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                orderFromDb.PaymentIntentId = paymentIntentId;
                orderFromDb.PaymentDate = DateTime.Now; 


            }
        }
    }
}

