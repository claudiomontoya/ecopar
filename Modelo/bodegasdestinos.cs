namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class bodegasdestinos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bodegasdestinos()
        {
            tranferencia_destino = new HashSet<tranferencia_destino>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(150)]
        public string descripcion { get; set; }

        public int id_destino { get; set; }

        public int id_comuna { get; set; }

        public int id_encargado { get; set; }

        [Required]
        [StringLength(150)]
        public string direccion { get; set; }

        [Required]
        [StringLength(150)]
        public string numero { get; set; }

        [StringLength(50)]
        public string telefono { get; set; }

        public virtual comunas comunas { get; set; }

        public virtual destinos destinos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tranferencia_destino> tranferencia_destino { get; set; }
    }
}
