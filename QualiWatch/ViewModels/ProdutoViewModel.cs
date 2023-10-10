using System.Collections.ObjectModel;

namespace QualiWatch.ViewModels;

public class ProdutoViewModel
{
    private ObservableCollection<Produto> _produtos;
    public ObservableCollection<Produto> Produtos
    {
        get => _produtos;
        set => _produtos = value;
    }
    public ProdutoViewModel()
    {
        _produtos = new ObservableCollection<Produto>();
        Task.Run(LoadData);
    }
    private bool _isRefreshing = false;
    public event Action OnIsRefreshingChanged;

    public bool IsRefreshing
    {
        get => _isRefreshing;
        set
        {
            if (_isRefreshing == value)
                return;

            _isRefreshing = value;
            OnIsRefreshingChanged?.Invoke();

        }
    }
    public async Task LoadData()
    {
        try
        {
            IsRefreshing = true;
            var produtosColections = await ProdutoManager.GetProdutos();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Produtos.Clear();
                foreach (Produto produto in produtosColections)
                {
                    Produtos.Add(produto);
                }
            });

        }
        finally
        {
            IsRefreshing = false;
        }
    }
    public async Task DeletarProduto(string id)
    {
        await ProdutoManager.DeleteProduto(id);
        await LoadData();
    }
}
