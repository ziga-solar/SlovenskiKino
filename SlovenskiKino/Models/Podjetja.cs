using System;
using System.Collections.Generic;

namespace SlovenskiKino.Models
{
    public partial class Podjetja
    {
        public Podjetja()
        {
            AktualniFilmi = new HashSet<AktualniFilmi>();
            CenikVstopnic = new HashSet<CenikVstopnic>();
            Kinematografi = new HashSet<Kinematografi>();
            Napovedi = new HashSet<Napovedi>();
        }

        public int IdPodjetja { get; set; }
        public string Podjetje { get; set; }

        public ICollection<AktualniFilmi> AktualniFilmi { get; set; }
        public ICollection<CenikVstopnic> CenikVstopnic { get; set; }
        public ICollection<Kinematografi> Kinematografi { get; set; }
        public ICollection<Napovedi> Napovedi { get; set; }
    }
}
