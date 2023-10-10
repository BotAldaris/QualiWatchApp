using QualiWatch;
using System.Text;

public class ImageManager
{
    public static async Task<string> UploadPhotoAsync(string base64, string apiUrl)
    {
        try
        {

            HttpClient client = ProdutoManager.GetClient();
            var content = new StringContent($"{{ \"base64_image\": \"{base64}\" }}", Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Foto enviada com sucesso para a API.");
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                Console.WriteLine($"Erro ao enviar a foto para a API. Status code: {response.StatusCode}");
                return "erro";

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar a foto para a API: {ex.Message}");
            return "erro";
        }
    }
}
