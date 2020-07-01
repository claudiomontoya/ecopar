using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecopartSistem.Models
{
    public class PersonaModel
    {
        public string rut { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string celular { get; set; }
        public string tipo { get; set; }
        public int id { get; set; }
    }
}