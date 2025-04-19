namespace PresentacionMVC.Models
{
    public class CabaniaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public TipoModel? Tipo { get; set; }
        public int TipoId { get; set; }
        public bool Jacuzzi { get; set; }
        public bool HabilitadaReservas { get; set; }
        public int NumHabitacion { get; set; }
        public int CantidadMaxPersonas { get; set; }
        public string? Foto { get; set; }
        
    }
}
