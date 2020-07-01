namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Solicitud")]
    public partial class Solicitud
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Solicitud()
        {
            emision = new HashSet<emision>();
            facturas = new HashSet<facturas>();
        }

        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        public int id_cliente { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string estado { get; set; }

        [Required]
        [StringLength(1000)]
        public string descripcion { get; set; }

        [StringLength(250)]
        public string archivo { get; set; }

        public int id_producto { get; set; }

        public int cantidad { get; set; }

        [Required]
        [StringLength(50)]
        public string tipo_despacho { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_requerida { get; set; }

        public int id_bodega { get; set; }

        public int id_sucursal { get; set; }

        public virtual bodega bodega { get; set; }

        public virtual clientes clientes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<emision> emision { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<facturas> facturas { get; set; }

        public virtual Productos Productos { get; set; }

        public virtual sucursal sucursal { get; set; }

        public virtual usuarios usuarios { get; set; }
    }
}
