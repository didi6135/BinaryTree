using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBinaryTree.Models
{
    internal class Threat
    {
        public Threat(string threatType, int volume, int sophistication, string target)
        {
            ThreatType = threatType;
            Volume = volume;
            Sophistication = sophistication;
            Target = target;
        }

        public string ThreatType { get; set; }
        public int Volume { get; set; }
        public int Sophistication { get; set; }
        public string Target { get; set; }


        
        public override string ToString()
        {
            return $"ThreatType: {ThreatType}, Volume: {Volume}, Sophistication: {Sophistication}, Target: {Target}";

        }
    }


    internal class ThreatList
    {
        public List<Threat> AllThreats { get; set; }    
    }
}
