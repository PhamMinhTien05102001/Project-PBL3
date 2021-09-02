using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Đồ_án_3_lớp.DAL
{
    class DBHelper
    {
        private static DBHelper _Instance;
        public SqlConnection Connect;
        public static DBHelper Instance
        {
            get
            {
                if(_Instance == null)
                {
                    //Tiến
                    string s = @"Data Source = MSI\SQLEXPRESS; Initial Catalog = DoAn; Integrated Security = True; user = sa; password = 123456";
                    // Minh
                    //string s = @"Data Source=LAPTOP-UJNHQJLL\SQLEXPRESS;Initial Catalog=DA;Integrated Security=True";
                    // Thịnh
                    //string s = @"Data Source=LAPTOP-8SGILAKG\SQLEXPRESS;Initial Catalog=QLDA;Integrated Security=True";
                    // Quang
                    //string s = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=DoAn;Integrated Security=True";
                    _Instance = new DBHelper(s);
                }
                return _Instance;
            }
            private set { }
        }
        private DBHelper(string s)
        {
            Connect = new SqlConnection(s);
        }
        public DataTable GetRecord(string Query)
        {
            DataTable data = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Query, Connect);
            Connect.Open();
            da.Fill(data);
            Connect.Close();
            return data;
        }
        public void UpdateCSDL(String Query)
        {
            SqlCommand cmd = new SqlCommand(Query, Connect);
            Connect.Open();
            cmd.ExecuteNonQuery();
            Connect.Close();
        }
    }
}
