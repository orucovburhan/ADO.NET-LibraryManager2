using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_Hw2;

class DataAccess
{
    DbConnection? conn = new SqlConnection("Server=localhost,1433;Database=library;User Id=sa;Password=MyS3cure_P@ssw0rd123;TrustServerCertificate=True;");
    DbDataAdapter? adapter = null;
    public DataTable? table = null;
    public List<string> bookList = new List<string>();
    public List<string> columnList = new List<string>();

    private void ShowDataTable(DataTable table)
    {
        foreach (DataColumn Dcolumn in table.Columns)
            Console.Write($"{Dcolumn.ColumnName,-15}");

        Console.WriteLine();

        foreach (DataRow Drow in table.Rows)
        {
            bookList.Add(Drow["Column1"].ToString());
            
            foreach (var item in Drow.ItemArray)
                Console.Write($"{item,-15}");
            Console.WriteLine();
        }

    }

    public void FillTableWithDisconnect()
    {
        var query = "SELECT CAST(Id AS NVARCHAR) + '.' + Name FROM Books;";

        var command = new SqlCommand()
        {
            CommandText = query,
            Connection = (SqlConnection)conn
        };

        adapter = new SqlDataAdapter(command);
        table = new DataTable();
        adapter.Fill(table);
        

        ShowDataTable(table);
    }
    public void ShowColumns()
    {
        var query = "SELECT * FROM Books";

        var command = new SqlCommand()
        {
            CommandText = query,
            Connection = (SqlConnection)conn
        };

        adapter = new SqlDataAdapter(command);
        DataTable table = new DataTable();
        adapter.Fill(table); 

        Console.WriteLine("Column Names:");
        foreach (DataColumn column in table.Columns)
        {
            columnList.Add(column.ColumnName);
            if (!column.ColumnName.Contains("Id"))//foregin keylerin hamisinda id oldugu ucun
                Console.WriteLine(column.ColumnName);
        }
    }
    public void UpdateBook(string bookName,string columnName,string newValue)
    {
        var query = $"UPDATE Books SET {columnName} = @newValue WHERE Name = @bookName;";

        var command = new SqlCommand()
        {
            CommandText = query,
            Connection = (SqlConnection)conn
        };
        command.Parameters.AddWithValue("@newValue", newValue);
        command.Parameters.AddWithValue("@bookName", bookName);
        adapter = new SqlDataAdapter(command);
        conn.Open();
        command.ExecuteNonQuery();
        conn.Close();
        Console.WriteLine("Updated");
        
    }
    public void AddCascade()
    {
        var query = $"EXEC AddCascadeToSCards";
        var command = new SqlCommand()
        {
            CommandText = query,
            Connection = (SqlConnection)conn
        };
        adapter = new SqlDataAdapter(command);
        conn.Open();
        command.ExecuteNonQuery();
        conn.Close();
        Console.WriteLine("Added");
    }

    public void DeleteBook(string bookName)
    {
        var query2 = $"DELETE FROM Books WHERE Name = @bookName;";

       
        var command2 = new SqlCommand()
        {
            CommandText = query2,
            Connection = (SqlConnection)conn
        };
        command2.Parameters.AddWithValue("@bookName", bookName);

        conn.Open();
        adapter = new SqlDataAdapter(command2);
        command2.ExecuteNonQuery();
        conn.Close();
        Console.WriteLine("Deleted");
        
    }

}
