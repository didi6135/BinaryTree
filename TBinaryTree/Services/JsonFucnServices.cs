using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using TBinaryTree.Classes;
using TBinaryTree.Models;

namespace TBinaryTree.Services
{



    internal static class JsonFucnServices
    {
        private static readonly string threatFile = "threats.json";
        private static readonly string defenceFile = "protections.json";


        // Get all threats from json
        public static ThreatList GetAllThreat()
        {
            string jsonData = File.ReadAllText(threatFile);
            ThreatList? allThreat = JsonSerializer.Deserialize<ThreatList>(jsonData);

            return allThreat;
        }

        // Get all protection from json
        public static List<DNode> GetAllDefences()
        {

            string jsonData = File.ReadAllText(defenceFile);
            ProtectionList? allDefence = JsonSerializer.Deserialize<ProtectionList>(jsonData);


            List<DNode> protectionNode = allDefence.AllProtections
                .Select(protection => new DNode
                    {
                        Minseverity = protection.MinSeverity,
                        Maxseverity = protection.MaxSeverity,
                        Protections = protection.Defenses,

                    }).ToList();
            

            return protectionNode;
        }

        


    }
}
