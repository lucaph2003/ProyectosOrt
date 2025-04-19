namespace PresentacionMVC.Models
{
    public class AltaCabaniaModel
    {
        public CabaniaModel? Cabania { get; set; }
        public IEnumerable<TipoModel>? Tipos { get; set; }
        public IFormFile Foto { get; set; }
    }
}
