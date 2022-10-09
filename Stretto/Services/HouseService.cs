using Newtonsoft.Json;
using Stretto.Models;
using System.Data;

namespace Stretto.Services
{
    public interface IHouseService
    {
        List<HouseWithTax> FindMostExpensiveHousesForEachCityTaxIncluded(List<House> data, string url);
        House FindCheapestHouseWithMostRooms(List<House> data);
        List<House> FindBiggestResidentalHouseForEachCity(List<House> data);
    }

    public class HouseService : IHouseService
    {
        private IDataAccesssService _dataRetriever;
        public HouseService(IDataAccesssService dataRetriever)
        {
            _dataRetriever = dataRetriever;
        }

        public List<HouseWithTax> FindMostExpensiveHousesForEachCityTaxIncluded(List<House> data, string url)
        {
            var cities = data.GroupBy(d => d.City);

            var housesWithTaxedPrice = new List<HouseWithTax>();
            foreach (var city in cities)
            {
                var taxValue = _dataRetriever.GetTaxValue(url + city.Key);
                var house = city.OrderBy((h => GetPriceWithTaxValue(h.Price, taxValue))).Last();

                var serializedHouse = JsonConvert.SerializeObject(house);
                var houseWithTaxedPrice = JsonConvert.DeserializeObject<HouseWithTax>(serializedHouse);
                houseWithTaxedPrice.PriceWithTax = GetPriceWithTaxValue(houseWithTaxedPrice.Price, taxValue);

                housesWithTaxedPrice.Add(houseWithTaxedPrice);                
            }

            return housesWithTaxedPrice;
        }

        public House FindCheapestHouseWithMostRooms(List<House> data)
        {
            return data.Where(d => d.Baths > 0 && d.Beds > 0)
                .OrderBy(d => d.Price / (d.Beds + d.Baths))
                .First();
        }

        public List<House> FindBiggestResidentalHouseForEachCity(List<House> data)
        {
            return data.GroupBy(d => d.City)
                .Select(c => c.OrderBy(e => e.Surface).Last())
                .ToList();           
        }

        private double GetPriceWithTaxValue(double price, double taxValue)
        {
            return price + (price * taxValue);
        }
    }
}
