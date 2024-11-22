using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Dal
{
    public interface ICompanyDbContext
    {
        DbSet<Company> Companies { get; set; }
    }
}
