using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManagementSystem.Entities
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }

        [StringLength(100, ErrorMessage ="El nombre no puede tener mas de 100 caracteres")]
        public string Nombre { get; set; }

        [StringLength(11, ErrorMessage = "La Cedula/RNC debe tener un maximo de 11 caracteres")]
        public string Cedula { get; set; }

        [StringLength(100, ErrorMessage = "Tipo de documento invalido. Los valores permitidos son: 1(Cedula), 2(Pasaporte), 3(RNC)")]
        public TipoDocumento TipoDocumento { get; set; }
    }

    public enum TipoDocumento
    {
        Cedula = 1,
        Pasaporte = 2,
        RNC = 3
    }
}
