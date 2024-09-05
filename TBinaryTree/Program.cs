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
            Console.WriteLine("\n\n\n_______________CREATE TREE_______________\n\n\n");
            BinarySearchTree binarySearchTree = BuildBinarySearchTree();
            Console.WriteLine();
            Console.WriteLine();



            await Task.Delay(4000);

            // Print the tree as unbalance 
            Console.WriteLine("\n\n\n_______________PRINT TREE UNBALANCED_______________\n\n\n");
            binarySearchTree.PrintTree(binarySearchTree.Root);


            await Task.Delay(4000);
            // This code balanced the tree
            // and print the tree as in-order & pre-order
            //
            Console.WriteLine("\n\n\n_______________BALANCED THE TREE_______________\n\n\n");
            binarySearchTree.BalanceTree();
            binarySearchTree.PrintTree(binarySearchTree.Root, "in-order");



            // Print the tree as balanced
            await Task.Delay(4000);
            Console.WriteLine("\n\n\n_______________PRINT TREE BALANCED_______________\n\n\n");
            List<DNode> allNode = binarySearchTree.InOrderRecursiveS(binarySearchTree.Root);
            for(int i = 0; i < allNode.Count; i++)
            {
                Console.Write($"Node number:{i} ");
                Console.WriteLine($"node details: MIN: {allNode[i].Minseverity}, MAX: {allNode[i].Maxseverity}");
                Console.WriteLine();
            }


            await Task.Delay(4000);
            // This code balanced the tree and print it base on "in-order" 
            // you can change this to "pre-order" and it's will print like this
            //
            Console.WriteLine("\n\n\n_______________SAVE THE TREE TO JSON_______________\n\n\n");
            OrderAndInsertToJSON(binarySearchTree);
            binarySearchTree.PrintTree(binarySearchTree.Root, "in-order");
            Console.WriteLine();
            Console.WriteLine();



            await Task.Delay(4000);
            // This code get the all threats from the JSON
            // and print to the console with delay of 2 seconds
            //
            Console.WriteLine("\n\n\n_______________GET ALL THREATS_______________\n\n\n");
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
            binarySearchTree.PrintTree(binarySearchTree.Root, "pre-order");
            
            return binarySearchTree;
        }


        // Get all threats & print the defence base on search in the binary trees
        public static async Task GetAllThreats(BinarySearchTree binarySearchTree)
        {
            ThreatList allThreats = JsonFucnServices.GetAllThreat();

            foreach (var threat in allThreats.AllThreats)
            {
                int threatSeverity = SeverityServices.CalculateSeverity(threat);
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
                Console.WriteLine();
                await Task.Delay(2000);
            }
        }


        
    }
}
