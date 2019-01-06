using System;
using System.Collections.Generic;

namespace SlovenskiKino.Models
{
    public partial class InfoOdvoranah
    {
        public int IdInfo { get; set; }
        public int IdKinematograf { get; set; }
        public string Dvorana { get; set; }
        public int? SteviloSedezov { get; set; }
        public int? SteviloVrst { get; set; }
        public int? InvalidskiSedezi { get; set; }
        public string Podpora3D { get; set; }

        public Kinematografi IdKinematografNavigation { get; set; }
    }
}
