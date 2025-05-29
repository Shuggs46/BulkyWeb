namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork1
    {
        IApplicationUserRepository ApplicationUser { get; }
        ICategoryRepository Category { get; }
        ICompanyRepository Company { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IProductRepository Product { get; }
        IShoppingCartRepository ShoppingCart { get; }

        void Save();
    }
}