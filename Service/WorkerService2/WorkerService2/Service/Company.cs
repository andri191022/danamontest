using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService2.DBContext;
using WorkerService2.Model;
using WorkerService2.Service.IService;

namespace WorkerService2.Service
{
    public class Company : ICompany
    {
        private readonly string _connectionString;
        private readonly DapperContext _context;
        private readonly ILogger<Worker> _logger;

        public Company(IConfiguration configuration, DapperContext context, ILogger<Worker> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<tb_SMK>> GetCompanies()
        {
            try
            {


                using var connection = new SqlConnection(_connectionString);

                var sql = "SELECT [NPSN],[Kode_Kabupaten],[Nama_Sekolah],[Status_Sekolah],[Status_LSP],[Kode_KK] FROM tb_SMK";

                return (IEnumerable<tb_SMK>)await connection.QueryAsync<tb_SMK>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<tb_SMK>();
            }
        }

        public async Task<IEnumerable<tb_SMK>> GetNewCompanies()
        {
            try
            {

                var sql = "SELECT [NPSN],[Kode_Kabupaten],[Nama_Sekolah],[Status_Sekolah],[Status_LSP],[Kode_KK] FROM tb_SMK";
                using (var connection = _context.CreateConnection())
                {
                    var companies = await connection.QueryAsync<tb_SMK>(sql);
                    return companies.ToList();
                }
            }
            catch (Exception er)
            {
                _logger.LogError(er.Message);
                return new List<tb_SMK>();
            }
        }

    }
}
