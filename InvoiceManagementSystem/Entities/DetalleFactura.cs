using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagementSystem.Entities
{
    public class DetalleFactura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetalleFacturaId { get; set; }

        [Required]
        public int FacturaId { get; set; }

        public int ProductoId {  get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="La cantidad debe ser mayor que cero.")]
        public int Cantidad { get; set; }

        [Column(TypeName ="decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor que cero.")]
        public decimal PrecioUnitario { get; set; }

        public decimal Total { get; set; }

        public Factura Factura { get; set; }
        public DetalleFactura() { }
    }
}
