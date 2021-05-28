using System;
using System.Collections.Generic;

#nullable disable

namespace WebAppPatientCare.Models
{
    public partial class Lectura
    {
        public int IdLectura { get; set; }
        public int? IdPaciente { get; set; }
        public float RitmoCardiaco { get; set; }
        public float SaturacionOxigeno { get; set; }
        public DateTime? FechaMedicion { get; set; }

        public virtual Paciente IdPacienteNavigation { get; set; }
    }
}
