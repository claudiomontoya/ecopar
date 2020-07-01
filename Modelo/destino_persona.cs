namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class destino_persona
    {
        public int id { get; set; }

        public int id_destino { get; set; }

        public int id_persona { get; set; }

        public int id_tipo { get; set; }

        public virtual destinos destinos { get; set; }

        public virtual Personas Personas { get; set; }

        public virtual tipo_persona tipo_persona { get; set; }
    }
}
