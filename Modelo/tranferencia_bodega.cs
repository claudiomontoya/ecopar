namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tranferencia_bodega
    {
        public int id { get; set; }

        public int id_emision { get; set; }

        public int id_sucursal { get; set; }

        public DateTime fecha_creacion { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [StringLength(250)]
        public string archivo { get; set; }

        public DateTime fecha_retiro { get; set; }

        public int cantidad { get; set; }

        [Required]
        [StringLength(50)]
        public string estado { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_envio { get; set; }

        [Required]
        [StringLength(50)]
        public string tipo { get; set; }

        public virtual emision emision { get; set; }

        public virtual sucursal sucursal { get; set; }
    }
}
