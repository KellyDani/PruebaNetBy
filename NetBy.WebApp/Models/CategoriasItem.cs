namespace NetBy.WebApp.Models
{
    public class CategoriasItem
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Anulado { get; set; }
        public string Estado { get; set; }

        public CategoriasItem Clone()
        {
            return new CategoriasItem
            {
                Id = this.Id,
                Nombre = this.Nombre,
                Descripcion = this.Descripcion,
                Anulado = this.Anulado,
                Estado = this.Estado
            };
        }
    }
}
