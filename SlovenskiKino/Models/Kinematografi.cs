using System;
using System.Collections.Generic;

namespace SlovenskiKino.Models
{
    public partial class Kinematografi
    {
        public Kinematografi()
        {
            InfoOdvoranah = new HashSet<InfoOdvoranah>();
            PodrobnoKinematografi = new HashSet<PodrobnoKinematografi>();
        }

        public int IdKinematograf { get; set; }
        public int IdPodjetja { get; set; }
        public string Kinematograf { get; set; }

        public Podjetja IdPodjetjaNavigation { get; set; }
        public ICollection<InfoOdvoranah> InfoOdvoranah { get; set; }
        public ICollection<PodrobnoKinematografi> PodrobnoKinematografi { get; set; }
    }
}
