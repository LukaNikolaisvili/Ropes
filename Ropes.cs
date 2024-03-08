/*
COIS-3020 ASSIGNMENT 2
CONTRIBUTORS:
LUKA NIKOLAISVILI
FARZAD IMRAN 
FREDERICK <--- check the spelling 
DUE DATE: MAR 10, 2024
*/



/*

Additional Optimizations
The ideal rope maintains a height of O(log n), but that can be quite a challenge. To help limit the height
of the tree, try to implement the following optimizations.
1. After a Split, compress the path back to the root to ensure that binary tree is full, i.e. each non-leaf
node has two non-empty children (4 marks).
2. Combine left and right siblings into one node whose total string length is 5 or less (4 marks).

*/

using System;

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

        Node rightSide = Split(this.root, i);

        Node newNode = new Node(S);

        Node leftSide = Concatenate(this.root, newNode);

        this.root = Concatenate(leftSide, rightSide);
        //root.value
        // Console.WriteLine(newNode.Value); // it should be the root.value but the build method is not working for the Rope class!


    }
    // Delete the substring S[i; j] (5 marks).
    public void Delete(int i, int j)
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

    // Return the substring S[i; j] (6 marks).
    public string Substring(int i, int j)
    {

        return "";
    }
    // Return the index of the rst occurrence of S; -1 otherwise (9 marks).
    public int Find(string S)
    {

        Node current = this.root; //starting from the root so my node will have the root 
        while (current != null) //checking the condition that will do the action while this condition is satisfied
        {

            return 1;
        }

        return -1;
    }
    // Return the character at index i (3 marks).
    public char CharAt(int i)
    {
        Node current = this.root; //starting from the root so my node will have the root 
        while (current != null) //checking the condition that will do the action while this condition is satisfied
        {
            //checking if current pointer.left is not null and if the index is less than the left side size
            if (current.Left != null && i < current.Left.Size)
            {
                // if fond index here move to the left child and move the cursor the current to the left to make it point now to left
                current = current.Left;
            }
            else
            {
                //checking if the left side is not equal to null
                if (current.Left != null)
                {
                    i -= current.Left.Size;
                }

                //checking if the current value is not null and the index is less that the length of the current pointers value
                if (current.Value != null && i < current.Value.Length)
                {
                    //then we will return the current value at index of i (character at i)
                    return current.Value[i];
                }

                // continue to the right side tree
                current = current.Right;
            }
        }

        //then the out of bounds and we will return the null character <---- here we have to make sure we will output -1 
        return '\0';
    }

    // Return the index of the rst occurrence of character c (4 marks).
    public int IndexOf(char c)
    {

        return -1;
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

        return "";
    }

    // Print the augmented binary tree of the current rope (4 marks).
    public void PrintRope()
    {
        if (this.root == null)
        {
            Console.WriteLine("The rope is empty.");
            return;
        }

        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(this.root);

        while (queue.Count > 0)
        {
            int levelSize = queue.Count;
            while (levelSize > 0)
            {
                Node current = queue.Dequeue();
                Console.Write($"{current.Value ?? "Node"} ");

                if (current.Left != null)
                {
                    queue.Enqueue(current.Left);
                }
                if (current.Right != null)
                {
                    queue.Enqueue(current.Right);
                }

                levelSize--;
            }
            Console.WriteLine(); // Newline for new level
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

    // Split the rope with root p at index i and return the root of the right subtree (9 marks).
    private Node Split(Node p, int i)
    {
        int goLeft;

        if (p == null)
        {
            // try{
            return null;

            // }catch{
            //     Exception(e)
            // }


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
            i -= goLeft; // + p.Value.Length;
            Node rightSide = Split(p.Right, i);

            p.Right = rightSide;

            return rightSide;
        }



    }


    // Rebalance the rope using the algorithm found on pages 1319-1320 of Boehm et al. (9 marks).
    private Node Rebalance()
    {

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

        rope.PrintRope();

        // rope.Insert("777", 7); //insert method might have some issues, got to take a look at it in depth.


        rope.Insert("LUKADATO", 5);

        //think split method has some issues, got to take care of that


        rope.PrintRope();

        Console.WriteLine(rope.CharAt(0)); //ideally should print the 0th index or the first character in the string


    }






}