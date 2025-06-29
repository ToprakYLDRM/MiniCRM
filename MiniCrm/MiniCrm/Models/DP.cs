using Dapper;
using System.Data.SqlClient;

namespace MiniCrm.Models
{
    public class DP
    {
        public static string connectionString = "Data Source=TOPRAK\\SQLEXPRESS;Initial Catalog=MiniCrm;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;";

        public static void ExecuteReturn(string procName, DynamicParameters param = null)
        {
            FixDateTimeParameters(param);
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                db.Execute(procName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public static IEnumerable<T> Listeleme<T>(string procName, DynamicParameters param = null)
        {
            FixDateTimeParameters(param);
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                return db.Query<T>(procName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        private static void FixDateTimeParameters(DynamicParameters param)
        {
            if (param == null) return;
            var paramLookup = param as SqlMapper.IDynamicParameters;
            var field = param.GetType().GetField("parameters", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                var dict = field.GetValue(param) as IDictionary<string, object>;
                if (dict != null)
                {
                    foreach (var key in dict.Keys.ToList())
                    {
                        if (dict[key] is DateTime dt && dt < new DateTime(1753, 1, 1))
                        {
                            dict[key] = DateTime.Now;
                        }
                    }
                }
            }
        }
    }
}
