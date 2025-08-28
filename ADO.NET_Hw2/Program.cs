using System.Text;
using ADO.NET_Hw2;

class Program
{
    static void Main(string[] args)
    {
        var app = new DataAccess();
        // app.AddCascade();//book-a reference eden foreign key-leri on cascade edirki delete edende problem yaranmasin.
        // app.DeleteBook("SQL");

        while (true)
        {
            app.FillTableWithDisconnect();
            Console.WriteLine("Choose one");
            string choice = Console.ReadLine();
            Console.Clear();
            try
            {
                Console.WriteLine(app.bookList[int.Parse(choice)-2]);
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("Wrong input.Please enter the numbers that are written");
                Thread.Sleep(2000);
                continue;
            }
            Console.WriteLine("1.Update");
            Console.WriteLine("2.Delete");
            string choice2 = Console.ReadLine();
            if (choice2 == "1")
            {
                app.ShowColumns();
                Console.Write("Choose one: ");
                string choice3 = Console.ReadLine();
                if (app.columnList.Contains(choice3))
                {
                    Console.Write("Enter new value: ");
                    string newValue = Console.ReadLine();
                    app.UpdateBook(app.bookList[int.Parse(choice)-2].Substring(2),choice3,newValue);
                    Console.WriteLine("Successfully updated!");
                    Thread.Sleep(2000);
                }
            }
            
        }
    }
}