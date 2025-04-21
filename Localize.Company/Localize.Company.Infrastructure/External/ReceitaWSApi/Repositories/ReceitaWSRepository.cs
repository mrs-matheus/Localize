using Localize.Company.Domain.Notifications;
using Localize.Company.Infrastructure.External.ReceitaWSApi.Contracts;
using Localize.Company.Infrastructure.External.ReceitaWSApi.Entities;
using System.Text.Json;

namespace Localize.Company.Infrastructure.External.ReceitaWSApi.Repositories
{
    public class ReceitaWSRepository : IReceitaWSRepository
    {
        private readonly NotificationContext _notification;
        public ReceitaWSRepository(NotificationContext notification)
        {
            _notification = notification;
        }

        public async Task<ReceitaWS?> GetCompanyByCnpj(string cnpj)
        {
            try
            {
                var url = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";

                var response = await new HttpClient().GetAsync(url);

                var json = await response.Content.ReadAsStringAsync();

                var error = response.StatusCode.ToString();
                if (response.IsSuccessStatusCode == false && error == "TooManyRequests")
                {
                    _notification.AddNotification("ReceitaWS", "Error, To Many Requests");
                    return null;
                }
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.TryGetProperty("status", out var status) && status.GetString() == "ERROR")
                {
                    _notification.AddNotification("ReceitaWS", "Error, not in cache");
                    return null;
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var company = JsonSerializer.Deserialize<ReceitaWS>(json, options);

                return company;
            }
            catch (Exception ex)
            {
                _notification.AddNotification("ReceitaWS", "ERROR, not in cache");
                return null;
            }
        }
    }
}
