﻿using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;


namespace Bulky.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDBContext _db;

        public CompanyRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
    

        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
    }
