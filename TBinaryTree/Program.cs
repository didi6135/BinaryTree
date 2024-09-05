using TBinaryTree.Classes;
using TBinaryTree.Models;
using TBinaryTree.Repositories;
using TBinaryTree.Services;

namespace TBinaryTree
{
    internal class Program
    {
        static async Task Main(string[] args)
        {


            // This code build the binary tree and insert the all defences into it
            // and print the tree as in-order
            // ---------------------------------
            // THIS CODE YOU HAVE TO ACTIVE 
            // ---------------------------------
            BinarySearchTree binarySearchTree = BuildBinarySearchTree();
            Console.WriteLine("Created tree and printed without order");
            Console.WriteLine();
            Console.WriteLine();


            // This code balanced the tree
            // and print the tree as in-order & pre-order
            //
            binarySearchTree.BalanceTree();
            binarySearchTree.PrintTree(binarySearchTree.Root, "in-order");
            Console.WriteLine();
            Console.WriteLine();
            binarySearchTree.PrintTree(binarySearchTree.Root, "pre-order");


            // This code balanced the tree and print it base on "in-order" 
            // you can change this to "pre-order" and it's will print like this
            //
            OrderAndInsertToJSON(binarySearchTree);
            binarySearchTree.PrintTree(binarySearchTree.Root, "in-order");
            Console.WriteLine();
            Console.WriteLine();


            // This code get the all threats from the JSON
            // and print to the console with delay of 2 seconds
            //
            await GetAllThreats(binarySearchTree);

        }


       

        // Order the tree & insert into JSON file
        public static void OrderAndInsertToJSON(BinarySearchTree binarySearchTree)
        {
            binarySearchTree.WriteTreeToJson(binarySearchTree.Root, "defenceStrategiesBalanced");
        }

        // Building the binray tree 
        public static BinarySearchTree BuildBinarySearchTree()
        {
            List<DNode> allProt = JsonFucnServices.GetAllDefences();

            BinarySearchTree binarySearchTree = new BinarySearchTree();

            foreach (var protection in allProt)
            {
                binarySearchTree.Insert(protection);
            }
            binarySearchTree.PrintTree(binarySearchTree.Root, "in-order");
            
            return binarySearchTree;
        }

        // Get all threats & print the defence base on search in the binary trees
        public static async Task GetAllThreats(BinarySearchTree binarySearchTree)
        {
            ThreatList allThreats = JsonFucnServices.GetAllThreat();

            foreach (var threat in allThreats.AllThreats)
            {
                int threatSeverity = SeverityServices.CalculateSeverity(threat);
                Console.WriteLine(threatSeverity);
                DNode bestNode = binarySearchTree.SearchBestThreat(threatSeverity);

                if (bestNode == null)
                {
                    Console.WriteLine($"No suitable defense was found for attack with severity {threatSeverity}. Brace for impact!");
                }
                else
                {
                    Console.WriteLine($"Best Match for severity {threatSeverity}: Minseverity: {bestNode.Minseverity}, Maxseverity: {bestNode.Maxseverity}");
                    Console.WriteLine($"Protections: {string.Join(", ", bestNode.Protections)}");
                }

                await Task.Delay(2000);
            }
        }
    }
}
