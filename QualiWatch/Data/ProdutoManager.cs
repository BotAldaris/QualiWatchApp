using Newtonsoft.Json;
using System.Net.Http.Json;

namespace QualiWatch;

public class ProdutoManager
{
    static HttpClient client;
    static string Url => Preferences.Default.Get("url", "");
    public static HttpClient GetClient()
    {
        if (client != null)
        {
            return client;
        }
        client = new HttpClient();
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        return client;
    }
    public static async Task<IEnumerable<Produto>> GetProdutos()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return new List<Produto>();

        HttpClient client = GetClient();
        string result = await client.GetStringAsync($"{Url}/Produto");

        return JsonConvert.DeserializeObject<List<Produto>>(result);
    }
    public static async Task DeleteProduto(string partID)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return;
        HttpRequestMessage msg = new(HttpMethod.Delete, $"{Url}/Produto/{partID}");
        HttpClient client = GetClient();
        var response = await client.SendAsync(msg);
        response.EnsureSuccessStatusCode();
    }

    public static async Task<Produto> Add(string nome, string lote, DateTime validade)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return new Produto();
        Produto produto = new Produto()
        {
            Nome = nome,
            Lote = lote,
            Validade = validade,
            Id = 0
        };
        HttpClient client = GetClient();
        HttpRequestMessage msg = new(HttpMethod.Post, $"{Url}/Produto");
        msg.Content = JsonContent.Create<Produto>(produto);
        var response = await client.SendAsync(msg);
        response.EnsureSuccessStatusCode();
        var returnedJson = await response.Content.ReadAsStringAsync();
        var ProdutoAdicionado = JsonConvert.DeserializeObject<Produto>(returnedJson);
        return ProdutoAdicionado;
    }
    public static async Task Update(Produto produto)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return;
        HttpRequestMessage msg = new(HttpMethod.Put, $"{Url}/Produto/{produto.Id}");
        msg.Content = JsonContent.Create<Produto>(produto);
        HttpClient client = GetClient();
        var response = await client.SendAsync(msg);
        response.EnsureSuccessStatusCode();

    }
}
