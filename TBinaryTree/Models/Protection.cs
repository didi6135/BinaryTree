using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBinaryTree.Models
{
    internal class Protection
    {
        public int MinSeverity { get; set; }
        public int MaxSeverity { get; set; }
        public List<string> Defenses { get; set; }
    }


    internal class ProtectionList
    {
        public List<Protection> AllProtections { get; set; }
    }
}
