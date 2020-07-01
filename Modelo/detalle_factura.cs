namespace Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class detalle_factura
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? id_factura { get; set; }

        public int? id_item { get; set; }

        public int? precio { get; set; }

        public int? cantidad { get; set; }

        public virtual facturas facturas { get; set; }

        public virtual item item { get; set; }
    }
}
