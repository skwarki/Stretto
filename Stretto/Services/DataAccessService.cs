using CsvHelper;
using Stretto.Models;
using System.Data;
using System.Globalization;

namespace Stretto.Services
{
    public interface IDataAccesssService
    {
        double GetTaxValue(string url);
        List<House> GetDataFromUrl(string url);
        DataTable GetDataFromUrlAsDataTable(string url);

    }
    public class DataAccessService : IDataAccesssService
    {
        private IHttpClientFactory _client;
        public DataAccessService(IHttpClientFactory client)
        {
            _client = client;
        }

        public double GetTaxValue(string url)
        {
            var response = _client.CreateClient().GetAsync(url).Result;

            var taxValue = response.Content.ReadAsStringAsync().Result;

            return Convert.ToDouble(taxValue);
        }

        public List<House> GetDataFromUrl(string url)
        {
            _client.CreateClient();
            HttpResponseMessage response = _client.CreateClient().GetAsync(url).Result;

            using (Stream stream = response.Content.ReadAsStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    using (CsvReader reader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        return reader.GetRecords<House>().ToList();
                    }
                }
            }
        }

        public DataTable GetDataFromUrlAsDataTable(string url)
        {
            HttpResponseMessage response = _client.CreateClient().GetAsync(url).Result;

            using (Stream stream = response.Content.ReadAsStream())
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    using (CsvReader reader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        using (var dataReader = new CsvDataReader(reader))
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(dataReader);

                            return dataTable;
                        }
                    }
                }
            }
        }
    }
}
