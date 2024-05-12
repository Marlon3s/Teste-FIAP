using Frontend.FIAP.Objects.Receive;
using System.Net.Http.Json;
using System.Text;

namespace Frontend.FIAP.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<ListAlunos> GetDataAPI(string url)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ListAlunos>(url);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter dados da API: " + ex.Message);
            }
        }

        public async Task<string> PostDataAPI(string url, object data)
        {
            var conteudo = new StringContent(System.Text.Json.JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, conteudo);

            if (response.IsSuccessStatusCode)
            {
                var resposta = await response.Content.ReadAsStringAsync();
                return resposta;
            }
            else
            {
                throw new Exception($"Erro ao enviar dados para a API: {response.StatusCode}");
            }
        }
    }
}
