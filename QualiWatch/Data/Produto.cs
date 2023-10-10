using System.ComponentModel.DataAnnotations;

namespace QualiWatch;
[Serializable]
public class Produto
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Nome { get; set; }
    public string Lote { get; set; }
    [Required]
    public DateTime Validade { get; set; }

}
