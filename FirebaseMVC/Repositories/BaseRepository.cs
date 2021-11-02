using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using CostumeCraze.Auth.Models;
using CostumeCraze.Models;
using Microsoft.Extensions.Configuration;

namespace CostumeCraze.Repositories
{
    public class BaseRepository
    {
        private string _connectionString;
        public BaseRepository(IConfiguration _config)
        {
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }
        protected SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
    }
}
