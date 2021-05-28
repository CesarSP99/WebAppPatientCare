using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppPatientCare.Models
{
    public partial class Paciente
    {
        public Paciente()
        {
            Lecturas = new HashSet<Lectura>();
        }

        public int IdPaciente { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPat { get; set; }
        public string ApellidoMat { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<Lectura> Lecturas { get; set; }
    }
}
