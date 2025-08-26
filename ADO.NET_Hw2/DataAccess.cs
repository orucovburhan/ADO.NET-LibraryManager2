using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Hw2;

class DataAccess
{
    DbConnection? conn = new SqlConnection("Data Source=STHQ0118-02;Initial Catalog=Library;User ID=admin;Password=admin;Connect Timeout=30;");

    public void ShowBooks()
    {
        SqlDataReader? reader = null;
        try
        {
            conn?.Open();

            using SqlCommand cmd = new SqlCommand("SELECT Name FROM Books", (SqlConnection)conn);
            reader = cmd.ExecuteReader();

            int count = 0;
            while (reader.Read())
            {
                count++;
                Console.WriteLine($"{count}.{reader[0]}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            conn?.Close();
            reader?.Close();
        }
    }



}
