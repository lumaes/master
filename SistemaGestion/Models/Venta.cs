namespace SistemaGestion.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public string Comentarios { get; set; }

        public int idUsuario { get; set; }

        public Venta()
        {
            Id = 0;
            Comentarios = "";
            idUsuario = 0;
        }
    }
}
