using Stretto.Models;
using System.Data;

namespace Stretto.Helpers
{
    public interface IConsoleHelper
    {
        void DisplayListOfObjectsInConsole<T>(List<T> data);
        void DisplayObjectInConsole<T>(T item);
        void DisplayHousesInConsoleFromDataTable(DataTable table);
    }

    public class ConsoleHelper : IConsoleHelper
    {
        public void DisplayListOfObjectsInConsole<T>(List<T> data)
        {
            foreach (var row in data)
            {
                DisplayObjectInConsole(row);
                Console.WriteLine("");
            }
        }

        public void DisplayObjectInConsole<T>(T item)
        {
            var fields = item.GetType().GetProperties();
            for (int i = 0; i < fields.Length; i++)
            {
                Console.WriteLine(fields[i].Name + " " + fields[i].GetValue(item));
            }
        }
        public void DisplayHousesInConsoleFromDataTable(DataTable table)
        {

            for (int i = 0; i < table.Columns.Count; i++)
            {
                Console.Write($"\"{table.Columns[i].ColumnName}\",");
            }

            Console.WriteLine("");
            Console.WriteLine("");

            foreach (DataRow row in table.Rows)
            {

                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    Console.Write($"\"{row.ItemArray[i]}\",");
                }

                Console.WriteLine("");
            }
        }
    }
}
