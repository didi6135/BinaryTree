using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBinaryTree.Classes
{
    internal class DNode
    {
        public DNode(int minseverity, int maxseverity, List<string> protections)
        {
            Minseverity = minseverity;
            Maxseverity = maxseverity;
            Protections = protections;
            rightDefence = null;
            leftDefence = null;
        }
        public DNode()
        {
            rightDefence = null;
            leftDefence = null;
        }

        public int Minseverity { get; set; }
        public int Maxseverity { get; set; }
        public List<string> Protections { get; set; }

        public DNode? rightDefence { get; set; }
        public DNode? leftDefence { get; set; }



    }
}
