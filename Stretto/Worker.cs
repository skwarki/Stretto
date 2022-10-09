using Stretto.Helpers;
using Stretto.Models;
using Stretto.Services;

namespace Stretto
{

    public class Worker
    {
        private readonly IDataAccesssService _dataRetriever;
        private readonly IHouseService _houseService;
        private readonly IConsoleHelper _consoleHelper;

        private static readonly string _url = "http://net-poland-interview-stretto.us-east-2.elasticbeanstalk.com/api/flats/csv ";
        private static readonly string _tax = "http://net-poland-interview-stretto.us-east-2.elasticbeanstalk.com/api/flats/taxes?city=";

        public Worker(IDataAccesssService dataRetriever,
            IHouseService houseService,
            IConsoleHelper consoleHelper)
        {
            _dataRetriever = dataRetriever;
            _houseService = houseService;
            _consoleHelper = consoleHelper;
        }
        public void Run()
        {
            var houses = _dataRetriever.GetDataFromUrl(_url);

            Console.BufferHeight = 30000;
            Console.WriteLine("First Assignment",Console.ForegroundColor = ConsoleColor.Red);
            Console.WriteLine("", Console.ForegroundColor = ConsoleColor.White);
            FirstAssignmet();

            Console.WriteLine("");
            Console.WriteLine("Second Assignment", Console.ForegroundColor = ConsoleColor.Red);
            Console.WriteLine("", Console.ForegroundColor = ConsoleColor.White);
            SecondAssignmet(houses);

            Console.WriteLine("");
            Console.WriteLine("Third Assignment", Console.ForegroundColor = ConsoleColor.Red);
            Console.WriteLine("", Console.ForegroundColor = ConsoleColor.White);
            ThirdAssignmet(houses);

            Console.WriteLine("");
            Console.WriteLine("Fourth Assignment", Console.ForegroundColor = ConsoleColor.Red);
            Console.WriteLine("", Console.ForegroundColor = ConsoleColor.White);
            FourthAssignmet(houses);

            Console.WriteLine("");
            Console.WriteLine("Fifth Assignment", Console.ForegroundColor = ConsoleColor.Red);
            Console.WriteLine("", Console.ForegroundColor = ConsoleColor.White);
            FifthAssignmet(houses);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private void FirstAssignmet()
        {
            try
            {
                var result = _dataRetriever.GetDataFromUrlAsDataTable(_url);
                _consoleHelper.DisplayHousesInConsoleFromDataTable(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }

        private void SecondAssignmet(List<House> houses)
        {
            try
            {
                _consoleHelper.DisplayListOfObjectsInConsole(houses);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ThirdAssignmet(List<House> houses)
        {
            try
            {
                var result = _houseService.FindBiggestResidentalHouseForEachCity(houses);
                _consoleHelper.DisplayListOfObjectsInConsole(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void FourthAssignmet(List<House> houses)
        {
            try
            {
                var result = _houseService.FindCheapestHouseWithMostRooms(houses);
                _consoleHelper.DisplayObjectInConsole(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void FifthAssignmet(List<House> houses)
        {
            try
            {
                var result = _houseService.FindMostExpensiveHousesForEachCityTaxIncluded(houses, _tax);
                _consoleHelper.DisplayListOfObjectsInConsole(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
