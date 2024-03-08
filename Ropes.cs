/*
COIS-3020 ASSIGNMENT 2
CONTRIBUTORS:
LUKA NIKOLAISVILI
FARZAD IMRAN 
FREDERICK NKWONTA
DUE DATE: MAR 10, 2024


Additional Optimizations
The ideal rope maintains a height of O(log n), but that can be quite a challenge. To help limit the height
of the tree, try to implement the following optimizations.
1. After a Split, compress the path back to the root to ensure that binary tree is full, i.e. each non-leaf
node has two non-empty children (4 marks).
2. Combine left and right siblings into one node whose total string length is 5 or less (4 marks).

*/


using System.Net;
using System.Text;

public class Rope
{
    private class Node
    {
        public string Value;
        public int Size;
        public Node Left;
        public Node Right;

        public Node(string value)
        {
            Value = value;
            Size = 0;
            Left = null;
            Right = null;
        }
    }

    private Node root;
    Node node = new Node(null);


    public Rope(string S)
    {
        // String st = new("Hi");
        root = Build(S, 0, S.Length);
    }

    // Insert string S at index i (5 marks).
    public void Insert(string S, int i)
    {
        try
        {
            Node rightSide = Split(this.root, i);

            Node newNode = new Node(S);

            Node leftSide = Concatenate(this.root, newNode);

            this.root = Concatenate(leftSide, rightSide);

        }
        catch (Exception ex)
        {
            Console.WriteLine("error occurred: " + ex.Message);

        }


        //root.value
        // Console.WriteLine(newNode.Value); // it should be the root.value but the build method is not working for the Rope class!


    }
    // Delete the substring S[i; j] (5 marks).
    public void Delete(int i, int j)
    {

        try
        {
            // Split the rope at indices i and j
            Node leftPart = Split(root, i);
            Node middlePart = Split(leftPart.Right, j - i);
            Node rightPart = middlePart.Right;

            // Discard the middle part (substring to be deleted)
            middlePart = null;

            // Concatenate the left and right parts
            root = Concatenate(leftPart, rightPart);
        }
        catch (Exception ex)
        {
            Console.WriteLine("error occurred: " + ex.Message);

        }


    }

    // Return the substring S[i; j] (6 marks).
    public string Substring(int i, int j)
    {

        return "";
    }
    // Return the index of the rst occurrence of S; -1 otherwise (9 marks).
    public int Find(string S)
    {

        try
        {
            Node current = this.root; //starting from the root so my node will have the root 
            while (current != null) //checking the condition that will do the action while this condition is satisfied
            {

                return 1;
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine("error occurred: " + ex.Message);

        }



        return -1;
    }
    // Return the character at index i (3 marks).
    public char CharAt(int i)
    {
        try
        {
            Node current = this.root;

            while (current != null)
            {
                if (current.Left != null && i < current.Left.Size)
                {
                    current = current.Left;
                }
                else
                {
                    if (current.Left != null)
                    {
                        i -= current.Left.Size;
                    }

                    if (current.Value != null && i < current.Value.Length)
                    {
                        return current.Value[i];
                    }

                    current = current.Right;
                }
            }

            // Return empty char to indicate index out of range.
            return '\0';
        }
        catch (Exception ex)
        {
            // Handle the exception here, you can log it or perform any other necessary actions.
            Console.WriteLine("An error occurred: " + ex.Message);
            return '\0'; // Return 'empty char' to indicate an error.
        }
    }


    // Return the index of the rest occurrence of character c (4 marks).
    // Return the index of the first occurrence of character c (4 marks).
    public int IndexOf(char c)
    {
        return IndexOf(root, c);
    }

    // Recursive helper function to find the index of character c in the rope rooted at node.
    private int IndexOf(Node node, char c)
    {
        try
        {
            if (node == null)
                return -1;

            // Search in the left subtree
            int leftIndex = IndexOf(node.Left, c);
            if (leftIndex != -1)
                return leftIndex;

            // Check the current node
            if (node.Value != null)
            {
                int indexInValue = node.Value.IndexOf(c);
                if (indexInValue != -1)
                    return (node.Left != null ? node.Left.Size : 0) + indexInValue;
            }

            // Search in the right subtree
            int rightIndex = IndexOf(node.Right, c);
            if (rightIndex != -1)
                return (node.Left != null ? node.Left.Size : 0) + (node.Value != null ? node.Value.Length : 0) + rightIndex;

            return -1; // Character not found in the rope
        }
        catch (Exception ex)
        {
            // Handle the exception here, you can log it or perform any other necessary actions.
            Console.WriteLine("An error occurred: " + ex.Message);
            return -1; // Return -1 to indicate an error.
        }
    }

    // Reverse the string represented by the current rope (5 marks).
    public void Reverse()
    {

    }

    // Return the length of the string (1 mark).
    public int Length()
    {

        if (node.Size != 0)
        {
            return node.Size;
        }
        else
        {
            return 0;
        }
    }

    // Return the string represented by the current rope (4 marks).
    public override string ToString()
    {
        try
        {
            if (root == null)
            {
                return "";
            }
            else
            {
                StringBuilder strBuild = new StringBuilder();
                Stack<Node> stack = new Stack<Node>();
                stack.Push(root);

                while (stack.Count > 0)
                {
                    Node curr = stack.Pop();

                    if (curr.Value != null)
                    {
                        strBuild.Append(curr.Value);
                    }
                    else
                    {
                        if (curr.Right != null)
                        {
                            stack.Push(curr.Right);
                        }

                        if (curr.Left != null)
                        {
                            stack.Push(curr.Left);
                        }
                    }
                }

                return strBuild.ToString();
            }
        }
        catch (Exception ex)
        {
            // Handle the exception here, you can log it or perform any other necessary actions.
            Console.WriteLine("An error occurred: " + ex.Message);
            return ""; // Return an empty string or any default value to indicate an error.
        }
    }


    // Print the augmented binary tree of the current rope (4 marks).
    public void PrintRope()
    {
        PrintNode(this.root, 0);
    }

    private void PrintNode(Node node, int indentation)
    {
        try
        {
            if (node == null)
            {
                return;
            }

            string indent = new String(' ', indentation * 2);

            if (node.Value != null)
            {
                Console.WriteLine("{0}( Size: {1} | Value: '{2}' )", indent, node.Size, node.Value);
            }
            else
            {
                Console.WriteLine("{0}( Size: {1} )", indent, node.Size);
            }

            PrintNode(node.Left, indentation + 2);
            PrintNode(node.Right, indentation + 2);
        }
        catch (Exception ex)
        {
            // Handle the exception here, you can log it or perform any other necessary actions.
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }


    //private methods

    // Recursively build a balanced rope for S[i; j] and return its root (part of the constructor).
    private Node Build(string s, int i, int j)
    {
        // If the substring is zero or -, return null.
        if (i >= j)
        {
            return null;
        }


        // create and return a leaf node If the substring length is small enough,.
        if (j - i == 1)
        {
            return new Node(s.Substring(i, j - i)) { Size = j - i };
        }

        // calculating the mid point to use it and to split in half (so it will split it in half)
        int mid = i + (j - i) / 2;

        // using the recursion to build the left and right sides of the tree (using the runtime stack)
        Node left = Build(s, i, mid);
        Node right = Build(s, mid, j);

        // Create a new internal node (non-leaf) as the parent of the two subtrees.
        Node internalNode = new Node(null)
        {
            Left = left,
            Right = right,
            // The weight of an internal node is the total length of strings in its left subtree.
            // This assumes that all string content is stored in leaf nodes.
            Size = (left != null ? left.Size : 0) + (right != null ? right.Size : 0)
        };

        return internalNode;
    }

    // Return the root of the rope constructed by concatenating two ropes with roots p and q (3 marks).
    private Node Concatenate(Node p, Node q)
    {
        try
        {
            Node root = new Node(null);

            if (p == null || q == null)
            {
                return null;
            }
            else
            {
                root.Left = p;
                root.Right = q;
                root.Size = p.Size + q.Size;

                if (p.Left == null && p.Right == null && q.Left == null && q.Right == null)
                {
                    root.Value = p.Value + q.Value;
                }
            }

            if (this.root != null)
            {
                Console.WriteLine(this.root.Value);
            }
            else
            {
                Console.WriteLine("Root is null.");
            }

            return root;
        }
        catch (Exception ex)
        {
            // Handle the exception here, you can log it or perform any other necessary actions.
            Console.WriteLine("An error occurred: " + ex.Message);
            return null;
        }
    }


    // Split the rope with root p at index i and return the root of the right subtree (9 marks).
    private Node Split(Node p, int i)
    {
        try
        {
            int goLeft;

            if (p == null)
            {
                return null;
            }

            if (p.Left != null)
            {
                goLeft = p.Left.Size;
            }
            else
            {
                goLeft = 0;
            }

            if (i <= goLeft)
            {
                p.Left = Split(p.Left, i);
                return p;
            }
            else
            {
                i -= goLeft;
                Node rightSide = Split(p.Right, i);
                p.Right = rightSide;
                return rightSide;
            }
        }
        catch (Exception ex)
        {
            // Handle the exception here, you can log it or perform any other necessary actions.
            Console.WriteLine("error occurred: " + ex.Message);
            return null;
        }
    }


    // Rebalance the rope using the algorithm found on pages 1319-1320 of Boehm et al. (9 marks).
    private Node Rebalance()
    {
        try
        {
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("error occurred: " + ex.Message);

        }

        return null;
    }


    public static void Main(string[] args)
    {


        Node node = new Node("LUKA");
        Node node1 = new Node("NIKOLAISVILI");
        Rope rope = new Rope(node.Value + node1.Value);

        Console.WriteLine(rope.Split(node, 1));

        // Console.WriteLine(node.Value);

        Node node3 = new Node(""); // L U K A N I K O

        node3 = rope.Concatenate(node, node1);

        // Console.WriteLine(node3.Value);

        // rope.PrintRope();

        // rope.Insert("777", 7); //insert method might have some issues, got to take a look at it in depth.


        rope.Insert("LUKADATO", 5);

        //think split method has some issues, got to take care of that


        // rope.PrintRope();

        Console.WriteLine(rope.CharAt(0)); //ideally should print the 0th index or the first character in the string

        rope.Delete(0, 7);

        rope.PrintRope();

        bool flag = true;

        Console.WriteLine(rope.ToString());


        Console.WriteLine(rope.IndexOf(node, 'L'));

        while (flag)
        {
            Console.WriteLine("1 - add\n2- remove\n3- build\nx- exit\n");
            Console.WriteLine("Enter corresponding number for operation you want to perform");

            string op = Console.ReadLine();


            if (op == "1")
            {
                Console.WriteLine("\nyou chose add\n");
            }

            else if (op == "2")
            {
                Console.WriteLine("\nyou chose remove\n");
            }

            else if (op == "3")
            {
                Console.WriteLine(rope.ToString());
                Console.WriteLine("\nyou chose build\n");
            }

            else if (op == "4")
            {
                Console.WriteLine("\nyou chose build\n");


            }

            else if (op == "x")
            {
                Console.WriteLine("Exiting...");
                flag = false;

                if (flag == false)
                {
                    Console.WriteLine("program exited succesfully...");
                }


            }


        }



    }






}