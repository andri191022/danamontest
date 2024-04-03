using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService2.Model;

namespace WorkerService2.Service.IService
{
    public interface ICompany
    {
        Task<IEnumerable<tb_SMK>> GetCompanies();
        Task<IEnumerable<tb_SMK>> GetNewCompanies();
    }
}
