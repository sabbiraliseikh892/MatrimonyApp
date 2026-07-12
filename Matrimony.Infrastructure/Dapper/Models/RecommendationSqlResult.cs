using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Infrastructure.Dapper.Models
{
    public class RecommendationSqlResult
    {
        public string Sql { get; set; } = string.Empty;

        public DynamicParameters Parameters { get; set; }
            = new DynamicParameters();
    }
}
