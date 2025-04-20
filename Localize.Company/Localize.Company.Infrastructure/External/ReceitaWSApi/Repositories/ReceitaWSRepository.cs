using Localize.Company.Infrastructure.External.ReceitaWSApi.Contracts;
using Localize.Company.Infrastructure.External.ReceitaWSApi.Entities;
using System.Text.Json;

namespace Localize.Company.Infrastructure.External.ReceitaWSApi.Repositories
{
    public class ReceitaWSRepository : IReceitaWSRepository
    {

        public ReceitaWSRepository()
        {
        }

        public async Task<ReceitaWS> GetCompanyByCnpj(string cnpj)
        {
            try
            {
                var url = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";

                var response = await new HttpClient().GetAsync(url);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var company = JsonSerializer.Deserialize<ReceitaWS>(json, options);

                return company;
            }
            catch (Exception ex)
            {

                throw;
            }

            return new ReceitaWS();
        }
    }
}
