using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using TBinaryTree.Models;
using TBinaryTree.Repositories;

namespace TBinaryTree.Classes
{
    internal class BinarySearchTree : IBinarySearchTree
    {

        public DNode Root {  get; set; }

        public BinarySearchTree()
        {
            Root = null;
        }


        /// <summary>
        /// Insert functions
        /// </summary>
        /// <param name="node"></param>
        public void Insert(DNode node)
        {
            if (Root == null) Root = node;
            else
            {
                InsertRecursive(node, Root);
            }
        }

        public DNode InsertRecursive(DNode newNode, DNode current)
        {
            
            if (current == null) return newNode;

            if (newNode.Minseverity < current.Minseverity)
            {
                current.leftDefence = InsertRecursive(newNode, current.leftDefence);
            }
            else if (newNode.Minseverity > current.Minseverity)
            {
                current.rightDefence = InsertRecursive(newNode, current.rightDefence);
            }

            return current;

        }


        // In order functions
        public void InOrder()
        {
            InOrderRecursive(Root);
        }

        public void InOrderRecursive(DNode root)
        {
            if (root == null) return;

            InOrderRecursive(root.leftDefence);

            Console.WriteLine($"Minseverity: {root.Minseverity}, Maxseverity: {root.Maxseverity}, Protections: {string.Join(", ", root.Protections)}");

            InOrderRecursive(root.rightDefence);
        }


        // Pre order functions
        public void PreOrder()
        {
            PreOrderRecursive(Root);
        }

        public void PreOrderRecursive(DNode root)
        {
            if (root == null) return;

            Console.WriteLine($"Minseverity: {root.Minseverity}, Maxseverity: {root.Maxseverity}, Protections: {string.Join(", ", root.Protections)}");

            PreOrderRecursive(root.leftDefence);

            PreOrderRecursive(root.rightDefence);
        }


        // Fixing the tree
        public void BalanceTree()
        {
            // 1. insert the tree inside sorted list
            List<DNode> nodes = FlattTheTree(Root);
            
            // 2. Building the balance tree from start
            Root = BuildBalanceTree(nodes);
        }

        public List<DNode> FlattTheTree(DNode root)
        {
            List<DNode> dNodes = new List<DNode>();
            FlattTheTreeRecursive(root, dNodes);
            return dNodes;
        }

        // Flatt the tree recursive
        public void FlattTheTreeRecursive(DNode node, List<DNode> nodes)
        {
            if(node == null) return;
            FlattTheTreeRecursive(node.leftDefence, nodes);
            nodes.Add(node);
            FlattTheTreeRecursive(node.rightDefence, nodes);

        }

        // Build the tree from start
        public DNode BuildBalanceTree(List<DNode> nodes)
        {
            return BuildBalanceTreeRecursive(nodes, 0, nodes.Count - 1); 
        }

        // Building the tree recursive
        public DNode BuildBalanceTreeRecursive(List<DNode> nodes, int start, int end)
        {
            if (start > end) return default;

            int middle = (start + end) / 2;
            DNode node = nodes[middle];

            node.leftDefence = BuildBalanceTreeRecursive(nodes, start, middle - 1);
            node.rightDefence = BuildBalanceTreeRecursive(nodes, middle + 1, end);
            return node;

        }


        // All function to save the all protection into a JSON
        public List<DNode> FlattenTreePreOrder(DNode root)
        {
            List<DNode> dNodes = new List<DNode>();
            FlattenTreePreOrderRecursive(root, dNodes);
            return dNodes;
        }

        public void FlattenTreePreOrderRecursive(DNode node, List<DNode> nodes)
        {
            if (node == null) return;

            nodes.Add(node);
            FlattenTreePreOrderRecursive(node.leftDefence, nodes);
            FlattenTreePreOrderRecursive(node.rightDefence, nodes);
        }

        
        public ProtectionList ConvertTreeToProtectionList(DNode root)
        {
            List<DNode> nodes = FlattenTreePreOrder(root);  
            List<Protection> protections = nodes.Select(node => new Protection
            {
                MinSeverity = node.Minseverity,
                MaxSeverity = node.Maxseverity,
                Defenses = node.Protections
            }).ToList();

            return new ProtectionList { AllProtections = protections };
        }

        
        public void WriteTreeToJson(DNode root, string filePath)
        {
            ProtectionList protectionList = ConvertTreeToProtectionList(root);  

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(protectionList, options);

            File.WriteAllText(filePath, jsonString);
            Console.WriteLine("Tree has been written to the JSON file in pre-order.");
        }
        
        // print the all protection base on what you choose
        // (The optins is "pre-order" or "in-order", the initial is "pre-order")
        public void PrintTree(DNode node, string traversalType = "pre-order")
        {
            if (node == null)
            {
                Console.WriteLine("The tree is empty.");
                return;
            }
            if (traversalType.ToLower() == "pre-order")
            {
                Console.WriteLine("Printing tree in pre-order:");
                PrintTreePreOrder(node);
            }
            else if (traversalType.ToLower() == "in-order")
            {
                Console.WriteLine("Printing tree in in-order:");
                PrintTreeInOrder(node);
            }
            else
            {
                Console.WriteLine("Invalid traversal type. Please choose 'pre-order' or 'in-order'.");
            }
        }

        // Print the tree as pre order
        private void PrintTreePreOrder(DNode node, string indent = "", bool isRightChild = false)
        {
            if (node == null) return;
        
            string childPrefix = isRightChild ? "└──Right: " : "├──Left: ";
            string nodeInfo = $"[{node.Minseverity}-{node.Maxseverity}] Defenses: {string.Join(", ", node.Protections)}";
        
            Console.WriteLine($"{indent}{(indent == "" ? "Root: " : childPrefix)}{nodeInfo}");
        
            string newIndent = indent + (isRightChild ? "   " : "|  ");
        
            if (node.leftDefence != null)
            {
                PrintTreePreOrder(node.leftDefence, newIndent, false);
            }
        
            if (node.rightDefence != null)
            {
                PrintTreePreOrder(node.rightDefence, newIndent, true);
            }
        }
        
        // Print the tree as in order
        private void PrintTreeInOrder(DNode node, string indent = "", bool isRightChild = false)
        {
            if (node == null) return;
        
            string newIndent = indent + (isRightChild ? "   " : "|  ");
        
            if (node.leftDefence != null)
            {
                PrintTreeInOrder(node.leftDefence, newIndent, false);
            }
        
            string childPrefix = isRightChild ? "└──Right: " : "├──Left: ";
            string nodeInfo = $"[{node.Minseverity}-{node.Maxseverity}] Defenses: {string.Join(", ", node.Protections)}";
        
            Console.WriteLine($"{indent}{(indent == "" ? "Root: " : childPrefix)}{nodeInfo}");
        
            if (node.rightDefence != null)
            {
                PrintTreeInOrder(node.rightDefence, newIndent, true);
            }
        }



        // Get the all node in list base on in order
        public List<DNode> InOrderRecursiveS(DNode root)
        {
            if (root == null) return new List<DNode>();

            List<DNode> values = new List<DNode>();

            values.AddRange(InOrderRecursiveS(root.leftDefence)); 
            values.Add(root);                                    
            values.AddRange(InOrderRecursiveS(root.rightDefence)); 

            return values;
        }


        // Search base on pre order
        public DNode SearchBestThreat(int threatSeverity)
        {
            return SearchBestThreatRecursive(Root, threatSeverity);
        }

       
        public DNode SearchBestThreatRecursive(DNode node, int threatSeverity, DNode bestMatch = null)
        {
            if (node == null) return bestMatch;


            List<DNode> nodesInOrder = InOrderRecursiveS(Root);
            if (nodesInOrder.Count > 0 && threatSeverity < nodesInOrder[0].Minseverity)
            {
                Console.WriteLine($"Attack with severity {threatSeverity} is below the minimum threshold. Attack is ignored.");
                return null;
            }

            if (threatSeverity >= node.Minseverity && threatSeverity <= node.Maxseverity)
            {
                bestMatch = node;
            }

            bestMatch = SearchBestThreatRecursive(node.leftDefence, threatSeverity, bestMatch);

            bestMatch = SearchBestThreatRecursive(node.rightDefence, threatSeverity, bestMatch);

            return bestMatch;
        }

        // Delete function
        public DNode DeleteNode(DNode root, int severity)
        {
            if (root == null) return root;

            if (severity < root.Minseverity)
            {
                root.leftDefence = DeleteNode(root.leftDefence, severity);
            }
            else if (severity > root.Minseverity)
            {
                root.rightDefence = DeleteNode(root.rightDefence, severity);
            }
            else
            {
                if (root.leftDefence == null && root.rightDefence == null)
                {
                    return null;
                }
                else if (root.leftDefence == null)
                {
                    return root.rightDefence;
                }
                else if (root.rightDefence == null)
                {
                    return root.leftDefence;
                }

                DNode successor = FindMin(root.rightDefence);

                root.Minseverity = successor.Minseverity;
                root.Maxseverity = successor.Maxseverity;
                root.Protections = successor.Protections;

                root.rightDefence = DeleteNode(root.rightDefence, successor.Minseverity);
            }

            return root;
        }

        // Find the minimum value node in the tree (leftmost node)
        public DNode FindMin(DNode node)
        {
            while (node.leftDefence != null)
            {
                node = node.leftDefence;
            }
            return node;
        }

    }
}
