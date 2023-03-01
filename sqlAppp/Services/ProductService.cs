using sqlAppp.Models;
using System.Data.SqlClient;

namespace sqlAppp.Services
{
    public class ProductService
    {
        private static string db_source = "appdbserver11.database.windows.net";
        private static string db_user = "ctrad";
        private static string db_password = "AQYWSDaqywsd1";
        private static string db_database = "appDB";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
           
            return new SqlConnection(_builder.ConnectionString);

        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();

            List<Product> _products_lst = new List<Product>() ;
            
            string statement = "SELECT ProductID, ProductName, Quantity from Products";
            
            conn.Open();

            SqlCommand cmd = new SqlCommand(statement, conn);

            using(SqlDataReader reader = cmd.ExecuteReader()){
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    _products_lst.Add(product);
                }
            }
            conn.Close();
            return _products_lst;
        }
    }
}
