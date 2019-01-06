using System;
using System.Collections.Generic;

namespace SlovenskiKino.Models
{
    public partial class PodrobnoKinematografi
    {
        public int IdPodrobno { get; set; }
        public int IdKinematograf { get; set; }
        public string Kraj { get; set; }
        public string Naslov { get; set; }
        public int? Posta { get; set; }
        public string TelStevilka { get; set; }

        public Kinematografi IdKinematografNavigation { get; set; }
    }
}
