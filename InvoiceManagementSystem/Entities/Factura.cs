using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagementSystem.Entities
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FacturaId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Total {  get; set; }

        public ICollection<DetalleFactura> Detalles { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente Cliente { get; set;}
    }
}
