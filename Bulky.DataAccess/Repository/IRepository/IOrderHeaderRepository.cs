﻿using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
        void UpdateStatus(int Id, string orderStatus, string? paymentStatus = null);
        void UpdateStripePaymentId(int Id, string sessionId, string paymentIntentId);
    }
}
