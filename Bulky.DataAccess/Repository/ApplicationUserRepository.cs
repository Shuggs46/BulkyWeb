﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;


namespace BulkyBook.DataAccess.Repository
{

    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository

    {
        private readonly ApplicationDBContext _db;
        public ApplicationUserRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationUser applicationUser)
        {
            _db.ApplicationUsers.Update(applicationUser);
        }
    }
}

