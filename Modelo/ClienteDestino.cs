namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClienteDestino")]
    public partial class ClienteDestino
    {
        public int id { get; set; }

        public int id_cliente { get; set; }

        public int id_destino { get; set; }

        public int id_contacto { get; set; }

        [StringLength(10)]
        public string estado { get; set; }

        public virtual clientes clientes { get; set; }

        public virtual destinos destinos { get; set; }

        public virtual Personas Personas { get; set; }
    }
}
