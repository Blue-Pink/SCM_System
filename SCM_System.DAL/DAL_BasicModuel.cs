﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCM_System.IDAL;
using SCM_System.Model;
namespace SCM_System.DAL
{
    public class DAL_BasicModuel<T> : DAL_UniversalModuel<T>, IDAL.IDAL_UniversalModuel<T> where T : class
    {
        public async Task<List<CustomerLevel>> GetCustomerLevel()
        {
            return await entities.CustomerLevel.ToListAsync();


        }
    }
}
