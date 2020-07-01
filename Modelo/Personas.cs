namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Personas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personas()
        {
            Cliente_Persona = new HashSet<Cliente_Persona>();
            ClienteDestino = new HashSet<ClienteDestino>();
            destino_persona = new HashSet<destino_persona>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(15)]
        public string rut { get; set; }

        [Required]
        [StringLength(150)]
        public string nombre { get; set; }

        [Required]
        [StringLength(150)]
        public string apellido { get; set; }

        [StringLength(20)]
        public string telefono { get; set; }

        [Required]
        [StringLength(20)]
        public string celular { get; set; }

        public bool estado { get; set; }

        [StringLength(250)]
        public string mail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cliente_Persona> Cliente_Persona { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClienteDestino> ClienteDestino { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<destino_persona> destino_persona { get; set; }
    }
}
