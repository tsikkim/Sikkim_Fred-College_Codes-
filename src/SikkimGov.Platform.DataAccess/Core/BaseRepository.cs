using System.Configuration;
using System.Data.SqlClient;


namespace SikkimGov.Platform.DataAccess.Core
{
    public class BaseRepository
    {
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["SikkiFredConnectionString"].ConnectionString);
        }
    }
}