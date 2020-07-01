namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class clientes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public clientes()
        {
            bodega = new HashSet<bodega>();
            Cliente_Persona = new HashSet<Cliente_Persona>();
            ClienteDestino = new HashSet<ClienteDestino>();
            Solicitud = new HashSet<Solicitud>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(15)]
        public string rut { get; set; }

        [Required]
        [StringLength(150)]
        public string nombre { get; set; }

        [Required]
        [StringLength(250)]
        public string direccion { get; set; }

        [Required]
        [StringLength(10)]
        public string numero { get; set; }

        public int id_comuna { get; set; }

        public int id_formaPago { get; set; }

        [StringLength(50)]
        public string telefono { get; set; }

        [Required]
        [StringLength(50)]
        public string celular { get; set; }

        [StringLength(50)]
        public string correo { get; set; }

        [StringLength(150)]
        public string clave { get; set; }

        public int id_encargado_pago { get; set; }

        public int id_encargado_contrato { get; set; }

        [Required]
        [StringLength(50)]
        public string descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bodega> bodega { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cliente_Persona> Cliente_Persona { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClienteDestino> ClienteDestino { get; set; }

        public virtual comunas comunas { get; set; }

        public virtual formapago formapago { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitud> Solicitud { get; set; }
    }
}
