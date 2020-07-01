namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("bodega")]
    public partial class bodega
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bodega()
        {
            Solicitud = new HashSet<Solicitud>();
        }

        public int id { get; set; }

        public int id_cliente { get; set; }

        [Required]
        [StringLength(150)]
        public string descripcion { get; set; }

        public int id_comuna { get; set; }

        public int id_encargado { get; set; }

        [Required]
        [StringLength(150)]
        public string direccion { get; set; }

        [Required]
        [StringLength(50)]
        public string numero { get; set; }

        [Required]
        [StringLength(50)]
        public string telefono { get; set; }

        public virtual clientes clientes { get; set; }

        public virtual comunas comunas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitud> Solicitud { get; set; }
    }
}
