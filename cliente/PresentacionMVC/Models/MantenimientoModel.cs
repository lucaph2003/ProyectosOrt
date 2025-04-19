namespace PresentacionMVC.Models
{
    public class MantenimientoModel
    {
        public int? Id { get; set; }
        public DateTime FechaMantenimiento { get; set; }
        public string Descripcion { get; set; }
        public int CostoMantenimiento { get; set; }
        public string Nombre { get; set; }
        public CabaniaModel? Cabania { get; set; }
        public int CabaniaId { get; set; }
    }
}
