namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("emision")]
    public partial class emision
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public emision()
        {
            tranferencia_bodega = new HashSet<tranferencia_bodega>();
            tranferencia_destino = new HashSet<tranferencia_destino>();
        }

        public int id { get; set; }

        public int id_solicitud { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        [Required]
        [StringLength(50)]
        public string usuarioeco { get; set; }

        public int cantidad { get; set; }

        [Required]
        [StringLength(50)]
        public string estado { get; set; }

        public virtual Solicitud Solicitud { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tranferencia_bodega> tranferencia_bodega { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tranferencia_destino> tranferencia_destino { get; set; }
    }
}
