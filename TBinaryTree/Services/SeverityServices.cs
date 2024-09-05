using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBinaryTree.Models;

namespace TBinaryTree.Services
{
    internal static class SeverityServices
    {

        public static int CalculateSeverity(Threat threat)
        {
            int targetValue = threat.Target switch
            {
                "Web Server" => 10,
                "Database" => 15,
                "User Credentials" => 20,
                _ => 5
            };

            return threat.Volume * threat.Sophistication + targetValue;
        }


    }
}
