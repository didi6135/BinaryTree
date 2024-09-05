using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBinaryTree.Classes;
using TBinaryTree.Models;

namespace TBinaryTree.Repositories
{
    internal interface IBinarySearchTree
    {
        // Insert functions
        void Insert(DNode node);
        DNode InsertRecursive(DNode newNode, DNode root);

        // InOrder functions
        void InOrder();
        void InOrderRecursive(DNode root);

        // PreOrder functoins
        void PreOrder();
        void PreOrderRecursive(DNode root);

        // Balanced functions
        void BalanceTree();
        List<DNode> FlattTheTree(DNode root);
        void FlattTheTreeRecursive(DNode node, List<DNode> nodes);
        DNode BuildBalanceTree(List<DNode> nodes);
        DNode BuildBalanceTreeRecursive(List<DNode> nodes, int start, int end);

        // Converting the tree and insert into json
        ProtectionList ConvertTreeToProtectionList(DNode root);
        void WriteTreeToJson(DNode root, string filePath);

        // Print the tree
        //void PrintTree(DNode node, string indent = "", bool isRightChild = false);
        void PrintTree(DNode node, string traversalType = "pre-order");

        // Searching in the tree base on PreOrder
        DNode SearchBestThreat(int threatSeverity);

    }
}
