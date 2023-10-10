namespace QualiWatch;

public class UserConfig
{
    public bool Tema { get; private set; }
    public string Url { get; set; }
    public Produto Produto { get; set; } = new Produto();
    public event Action OnThemeChanged;
    public void SalvarTema(bool tema)
    {
        Tema = tema;
        Preferences.Default.Set("tema", tema);
        OnThemeChanged?.Invoke();
    }
    public void SalvarUrl(string url)
    {
        Url = url;
        Preferences.Default.Set("url", url);
    }
}
