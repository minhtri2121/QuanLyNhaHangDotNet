using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaHang.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        public string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=QL_NhaHang;Integrated Security=True;";

        public DataTable ExcuteQuery(string query, object[] parameters = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        string[] listParams = query.Split(' ');
                        int i = 0;
                        foreach (string item in listParams)
                        {
                            if (item.Contains("@"))
                            {
                                if (parameters[i] == null || parameters[i].ToString() == "")
                                {
                                    parameters[i] = DBNull.Value;
                                }
                                command.Parameters.AddWithValue(item, parameters[i]);
                                i++;
                            }
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);
                }
            }
            return data;
        }


        public int ExcuteNonQuery(string query, object[] paremeter = null)
        {
            int data = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand(query, conn);

                if (paremeter != null)
                {
                    string[] ListPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in ListPara)
                    {
                        if (item.Contains('@'))
                        {
                            sqlCommand.Parameters.AddWithValue(item, paremeter[i]);
                            i++;
                        }
                    }
                }

                data = sqlCommand.ExecuteNonQuery();

                conn.Close();
            }
            return data;
        }

        public object ExcuteNonScalar(string query, object[] paremeter = null)
        {
            object data = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand(query, conn);

                if (paremeter != null)
                {
                    string[] ListPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in ListPara)
                    {
                        if (item.Contains('@'))
                        {
                            sqlCommand.Parameters.AddWithValue(item, paremeter[i]);
                            i++;
                        }
                    }
                }

                data = sqlCommand.ExecuteScalar();

                conn.Close();
            }
            return data;
        }
    }
}
