using System;
using System.Collections.Generic;

namespace L05_Tree
{
    public class Tree<TreeType>
    {        
        public TreeNode<TreeType> CreateNode(TreeType data)
        {
            TreeNode<TreeType> newNode = new TreeNode<TreeType>
            {
                Data = data
            };
            return newNode;
        }
    }

    public class TreeNode<NodeType>
    {
        public NodeType Data;
        public TreeNode<NodeType> Parent;
        public List<TreeNode<NodeType>> Children = new List<TreeNode<NodeType>>();

        public void AppendChild(TreeNode<NodeType> child)
        {
            Children.Add(child);
        }

        public void RemoveChild(TreeNode<NodeType> child)
        {
            Children.Remove(child);
        }

        public void PrintTree()
        {
            int stepCounter = 1;
            Console.WriteLine(Data);
            RecPrint(stepCounter);
        }

        public void RecPrint(int stepCounter)
        {
            string depth = "";
            for(int i = 0; i < stepCounter; i++ )
                depth += "*";

            foreach(TreeNode<NodeType> child in Children)
            {
                Console.WriteLine(depth + child.Data);
                child.RecPrint(stepCounter +1);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var tree = new Tree<String>();
            var root = tree.CreateNode("root");
            var child1 = tree.CreateNode("child1");
            var child2 = tree.CreateNode("child2");
            root.AppendChild(child1);
            root.AppendChild(child2);
            var grand11 = tree.CreateNode("grand11");
            var grand12 = tree.CreateNode("grand12");
            var grand13 = tree.CreateNode("grand13");
            child1.AppendChild(grand11);
            child1.AppendChild(grand12);
            child1.AppendChild(grand13);
            var grand21 = tree.CreateNode("grand21");
            child2.AppendChild(grand21);
            child1.RemoveChild(grand12);

            root.PrintTree();  
        }
    }
}
