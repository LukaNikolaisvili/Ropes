﻿/*
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



using System.Text;

// Program
public class Rope
{
    // Node class to use in other methods
    private class Node
    {
        // Properties
        public string Value;
        public int Size;
        public Node Left;
        public Node Right;

        // Node Constructor
        public Node(string value)
        {
            Value = value;
            if (value != null)
            {
                Size = value.Length;
            }
            else
            {
                Size = 0;
            }
            Left = null;
            Right = null;
        }
    }

    private Node root; // Node called root to use in other operations
    Node node = new Node(null); // Public Node to use for testing.


    // Method thart starts the rope making process
    // Calls Build method to build the rope
    public Rope(string S)
    {
        root = Build(S, 0, S.Length);
    }

    // Insert string S at index i (5 marks).
    public void Insert(string S, int i)
    {
        try
        {
            //vaildated user input 
            if (S != "" || i == -1)
            {
                if (i == 1)
                {

                    // Split the node at index i
                    Node rightSide = (this.root);

                    // Create a new node to hole the string to be inserted
                    Node newNode = new Node(S);

                    Console.WriteLine("The size of S is in IF:" + S.Length);

                    // concatenate the new node with the existing rope
                    this.root = Concatenate(newNode, rightSide);


                }
                else
                {
                    // left part of the node  
                    string L = Substring(0, i - 1);
                    Node leftside = new Node(L);

                    // Split the node at index i
                    Node rightSide = Split(this.root, i);

                    // Create a new node to hole the string to be inserted
                    Node newNode = new Node(S);

                    Console.WriteLine("The size of S is in IF:" + S.Length);

                    // concatenate the right side with the new node 
                    Node v1 = Concatenate(newNode, rightSide);
                    // concatenate the left side with the right side and new node 
                    this.root = Concatenate(leftside, v1);
                }

            }
        }


        catch (Exception ex)
        {
            Console.WriteLine("error occurred: " + ex.Message);

        }
    }

    // Delete the substring S[i; j] (5 marks).
    public void Delete(int i, int j)
    {


        try
        {
            if (i == 0)
            {
                // Split the rope from indices i to j
                // stores up the left side of the node 
                // Node leftPart = Split(root, i - 1);
                // Make a node that holds the string to be deleted
                Node middlePart = Split(root, j - i);
                // store up to the right side of the node 
                Node rightPart = middlePart.Right;

                // Discard the middle part (substring to be deleted)
                // leftPart = null;


                // Concatenate the left and right parts
                root = Concatenate(middlePart, rightPart);

            }
            else
            {
                // Split the rope from indices i to j
                // stores up the left side of the node 
                Node leftPart = Split(root, i);
                // Make a node that holds the string to be deleted
                Node middlePart = Split(leftPart.Right, j - i);
                // store up to the right side of the node 
                Node rightPart = middlePart.Right;

                // Discard the middle part (substring to be deleted)

                middlePart = null;

                // Concatenate the left and right parts
                root = Concatenate(leftPart, rightPart);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("error occurred: " + ex.Message);
        }
    }

    // Return the substring S[i; j] (6 marks).
    public string Substring(int i, int j)
    {
        int L = LengthRope();
        try
        {
            // validated user input 
            if (i < 0 || j < 0 || i > j || j > L)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder(); // StringBuilder to store the substring
            for (int index = i; index < j + 1; index++)
            {
                char c = CharAt(index); // Get character at the current index
                sb.Append(c); // Append character to StringBuilder
            }

            return sb.ToString(); // Return the constructed substring
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            return ""; // Return an empty string to indicate an error.
        }
    }

    // helper method for substring 
    public int LengthRope()
    {
        try
        {
            // Check if root is not null
            if (root != null)
            {
                // Length is the size value stored in the root node
                return root.Size; //start from 0
            }
            else
            {
                // If root is null, rope has length 0
                return 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred:" + ex.Message);
            return 0;
        }
    }

    // Return the index of the first occurrence of S; -1 otherwise (9 marks).
    public int Find(string S)
    {
        try
        {
            // Start traversing the rope character by character
            for (int i = 0; i <= root.Size - S.Length; i++)
            {
                // Check if the substring matches starting from the current position
                bool match = true;
                for (int j = 0; j < S.Length; j++)
                {
                    // Compare characters at each position
                    if (CharAt(i + j) != S[j])
                    {
                        match = false;
                        break;
                    }
                }

                // If a match is found, return the index of the first character of the substring
                if (match)
                    return i;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occurred: " + ex.Message);
        }

        return -1; // Return -1 if the substring is not found
    }


    // Return the character at index i (3 marks).
    public char CharAt(int i)
    {
        try
        {
            // Set current node as passed node
            Node current = this.root;

            // If not a null node proceed
            while (current != null)
            {
                // If left child is not null and i is less than the size
                if (current.Left != null && i < current.Left.Size)
                {
                    // Swap to the left child
                    current = current.Left;
                }
                else // Otherwise
                {
                    if (current.Left != null) // If left exists but i is greater
                    {
                        // Subtract size of left tree
                        i -= current.Left.Size;
                    }

                    // If there is a value in the current node
                    // and i is within the node, return the character
                    if (current.Value != null && i < current.Value.Length)
                    {
                        return current.Value[i];
                    }

                    // Recursively call the function on the right child otherwise
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

    // Return the index of the first occurrence of character c (4 marks).
    public int IndexOf(char c)
    {
        return IndexOf(root, c);
    }

    // private Recursive function to find the index of character c in the rope rooted at node.
    private int IndexOf(Node node, char c)
    {
        try
        {
            // Error if node is null
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
        try
        {
            // If the node is null, exit
            if (this.root == null) { return; }

            // Make a stack to hold the nodes for traveral
            Stack<Node> stack = new Stack<Node>();
            // Put passed node onto stack
            stack.Push(this.root);

            // If nodes are in the stack keep running
            while (stack.Count > 0)
            {
                // Make current node the latest value popped off the stack
                Node current = stack.Pop();

                // If the node is not a leaf, it has children to be processed
                if (current.Left != null || current.Right != null)
                {
                    // Swap the children nodes
                    Node temp = current.Left;
                    current.Left = current.Right;
                    current.Right = temp;

                    // Push children if they exist
                    if (current.Left != null)
                    {
                        stack.Push(current.Left);
                    }

                    if (current.Right != null)
                    {
                        stack.Push(current.Right);
                    }
                }
                else
                {
                    // Directly reverse the string for leaf nodes
                    char[] charArray = current.Value.ToCharArray();
                    Array.Reverse(charArray);
                    StringBuilder reversedString = new StringBuilder(charArray.Length);
                    for (int i = charArray.Length - 1; i >= 0; i--)
                    {
                        reversedString.Append(charArray[i]);
                    }

                    current.Value = reversedString.ToString();
                }
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine("An error occurred" + ex.Message);

        }
    }

    // Return the length of the string (1 mark).
    public int Length()
    {
        try
        {
            // Check if node is not null and size is not zero
            if (node != null && node.Size != 0)
            {
                // Length is the size value stored in the node
                return node.Size;
            }
            else
            {
                // Nothing there so size is 0
                return 0;
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine("An error occurred:" + ex.Message);


            return 0;
        }
    }

    // Return the string represented by the current rope (4 marks).
    public override string ToString()
    {
        return ToString(root);
    }

    
    private string ToString(Node node)
    {
        if (node == null) return "";
        if (node.Value != null)
        {
            return node.Value;
        }
        return ToString(node.Left) + ToString(node.Right);
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
                // Skip printing "NULL" to avoid cluttering the output.
                return;
            }
            

            int childIndentation = indentation + 4;

            PrintNode(node.Right, childIndentation);

            string indent = new String(' ', indentation);

            if (node.Value != null)
            {

                Console.WriteLine(indent + " └ (Size: " + node.Size + " | Value: '" + node.Value + "')");
            }
            else
            {

                Console.WriteLine(indent + "└ (Size: " + node.Size + ")");
            }


            PrintNode(node.Left, childIndentation);
        }
        catch (Exception ex)
        {

            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }



    //private methods

    // Recursively build a balanced rope for S[i; j] and return its root (part of the constructor).
    private Node Build(string str, int start, int end)
    {
        if (start >= end) return null;
        if (end - start <= 10) return new Node(str.Substring(start, end - start));

        int mid = start + (end - start) / 2;
        Node left = Build(str, start, mid);
        Node right = Build(str, mid, end);

        Node parent = new Node(null)
        {

            Left = left,
            Right = right

        };
        int leftSize = 0;
        int rightSize = 0;

        if (left != null)
        {
            leftSize = left.Size;
        }

        if (right != null)
        {
            rightSize = right.Size;
        }

        parent.Size = leftSize + rightSize;
        return parent;
    }

    // Return the root of the rope constructed by concatenating two ropes with roots p and q (3 marks).
    private Node Concatenate(Node p, Node q)
    {
        try
        {
            Node root = new Node(null);

            // If any of the passed nodes are null, exit.
            if (p == null || q == null)
            {
                return null;
            }
            else
            {
                // Otherwise set the nodes to the left and right children
                root.Left = p;
                root.Right = q;
                // Sum their size for the parent node's size
                root.Size = p.Size + q.Size;

                // If passed two leaf nodes, set parent node to hold value
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


            //Optimization adding left and right together if the total size is less than or equal to 5
            if (root.Left.Size + root.Right.Size <= 5)
            {

                root.Value = p.Value + q.Value;

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


    // Split the rope with root p at index i and return the root of the right subtree.
    private Node Split(Node node, int index)
    {
        if (node == null) return null;

        int leftSize = node.Left?.Size ?? 0;
        int valueSize = node.Value?.Length ?? 0;

        if (index < leftSize)
        {
            // Split needs to happen in the left subtree.
            Node splitRight = Split(node.Left, index);
            node.Left = splitRight; // Update the left subtree after the split.
        }
        else if (index > leftSize && node.Right != null)
        {
            // Adjust index relative to the right subtree.
            index -= (leftSize + valueSize);
            Node splitRight = Split(node.Right, index);
            node.Right = splitRight; // Update the right subtree after the split.
        }
        else if (node.Value != null && index - leftSize < valueSize)
        {
            // If the index is within the current node's value, split the string.
            string currentValue = node.Value;
            string leftPart = currentValue.Substring(0, index - leftSize);
            string rightPart = currentValue.Substring(index - leftSize);

            // Create a new node for the right part of the split.
            Node rightNode = new Node(rightPart) { Size = rightPart.Length };

            // Update the current node for the left part.
            node.Value = leftPart;
            node.Size = leftPart.Length;

            // Adjust the tree if needed.
            if (node.Right != null)
            {
                rightNode.Right = node.Right; // Attach the existing right subtree to the new node.
            }
            node.Right = rightNode; // Attach the new node as the right child of the current node.
        }

        // No explicit call to Rebalance here - depending on your rope's usage patterns, you might decide to rebalance periodically or after certain operations.
        return node; // Return the modified tree.
    }


    // Rebalance the rope using the algorithm found on pages 1319-1320 of Boehm et al. (9 marks).
    private Node Rebalance()
    {
        // Collect all leaves into a list.
        List<string> leaves = new List<string>();
        CollectLeaves(root, leaves);

        // Rebuild the tree from the collected leaves.
        return RebuildTree(leaves, 0, leaves.Count);
    }

    private void CollectLeaves(Node node, List<string> leaves)
    {
        if (node == null) return;

        // If this is a leaf node (has a value), add it to the list.
        if (node.Value != null)
        {
            leaves.Add(node.Value);
        }
        else
        {
            // Otherwise, recursively collect leaves from both subtrees.
            CollectLeaves(node.Left, leaves);
            CollectLeaves(node.Right, leaves);
        }
    }

    private Node RebuildTree(List<string> leaves, int start, int end)
    {
        // Base case: when the range is invalid.
        if (start >= end) return null;

        // Base case: when there's only one element in the range, create a leaf node.
        if (start == end - 1) return new Node(leaves[start]);

        // Recursive case: split the leaves into two roughly equal halves and build subtrees.
        int mid = start + (end - start) / 2;
        Node left = RebuildTree(leaves, start, mid);
        Node right = RebuildTree(leaves, mid, end);

        // Create a new parent node for the two subtrees.
        Node parent = new Node(null)
        {
            Left = left,
            Right = right,
            Size = (left != null ? left.Size : 0) + (right != null ? right.Size : 0)
        };

        return parent;
    }

    // Then, to rebalance the rope, you would call the Rebalance method and update the root.
    public void PerformRebalance()
    {
        root = Rebalance();
    }


    public static void Main(string[] args)
    {
        // Testing nodes
        // Node node = new Node("LUKA");
        Node node1 = new Node("NIKOLAISVILI");
        Node node2 = new Node("LUKA");
        Rope rope = new Rope(node1.Value);


        // Console.WriteLine(rope.Split(node, 1));

        // Console.WriteLine(node.Value);

        // node3 = new Node(null); // L U K A N I K O

        // node3 = rope.Concatenate(node, node1);

        // Console.WriteLine(node3.Value);

        // rope.PrintRope();

        // rope.Insert("777", 7); //insert method might have some issues, got to take a look at it in depth.


        // rope.Insert("LUKADATO", 5);

        //think split method has some issues, got to take care of that


        // rope.PrintRope();

        // Console.WriteLine(rope.CharAt(0)); //ideally should print the 0th index or the first character in the string

        //  rope.Delete(0, 7);

        // rope.Reverse();

        // rope.PrintRope();


        // rope.Delete(0,2);




        bool flag = true;

        // Console.WriteLine(rope.ToString());


        // Console.WriteLine(rope.IndexOf(node, 'L'));

        // Console.WriteLine(rope.Split(node, 1));

        // rope.PrintRope();




        while (flag)
        {
            // User interface
            Console.WriteLine("\nHello, you can perform any of these operations!");
            Console.WriteLine("-----------------");
            Console.WriteLine("1 - Insert\n2 - Split\n3 - Delete\n4 - Reverse\n5 - Print\n6 - ToString\n7 - Substring\n8 - Find\n9 - CharAt\n10 - IndexOf\n0 - Rebalance\nx - Exit()\n");
            Console.WriteLine("-----------------");
            Console.WriteLine("Enter the UID of the operation: ");

            string op = Console.ReadLine();


            // Insertion test call
            if (op == "1")
            {
                Console.WriteLine("\nyou chose Insert!\n");
                Console.WriteLine("Type the word that you want to add: ");
                string st = Console.ReadLine();
                Console.WriteLine("\nType index where you want to add it: ");
                string input = Console.ReadLine();
                bool convertStartIndexToInt = Int32.TryParse(input, out int index);

                if (st.Length > 0 && index != -1 && convertStartIndexToInt == true)
                {
                    rope.Insert(st, index);
                    Console.WriteLine("\n" + "[" + st + "]" + " succesfully inserted!");
                }

                else if (st == "")
                {
                    Console.WriteLine("You can not pass the empty string! ");
                }

                else if (index == -1)
                {
                    Console.WriteLine("You can not pass the negative location index! ");
                }


            }

            // Split test call
            else if (op == "2")
            {
                Console.WriteLine("\nyou chose Split!\n");

                Console.WriteLine("Enter index where you want to split");
                string input = Console.ReadLine();
                bool convertStartIndexToInt = Int32.TryParse(input, out int index);

                if (convertStartIndexToInt == true && index != -1)
                {
                    Console.WriteLine(rope.Split(node1, index));
                    Console.WriteLine("\nSplitted succesfully at index " + index);
                }


                else
                {
                    Console.WriteLine("wrong input!");
                }



            }

            // Deletion test call
            else if (op == "3")
            {
                Console.WriteLine("\nyou chose delete!\n");
                Console.WriteLine("Enter the start position");
                string input = Console.ReadLine();
                bool convertStartIndexToInt = Int32.TryParse(input, out int startIndex);
                Console.WriteLine("Enter the stop position");
                string secondInput = Console.ReadLine();
                bool convertStopIndexToIntT = Int32.TryParse(secondInput, out int stopIndex);

                if (convertStartIndexToInt == true && convertStopIndexToIntT == true)
                {
                    rope.Delete(startIndex, stopIndex);
                    Console.WriteLine("from starting position " + startIndex + " until the stop position " + stopIndex + " removed successfully!");
                }


            }

            // Reverse call
            else if (op == "4")
            {
                Console.WriteLine("\nyou chose Reverse!\n");

                rope.Reverse();

                Console.WriteLine(rope.ToString());



                Console.WriteLine("\nReversed succesfully!");


            }

            // Print Call
            else if (op == "5")
            {
                Console.WriteLine("you chose print!");
                Console.WriteLine("Printing...\n");
                rope.PrintRope();




            }

            // String conversion call
            else if (op == "6")
            {
                Console.WriteLine("you chose ToString!");
                Console.WriteLine("\nshowing what is rope made of:\n");
                Console.WriteLine(rope.ToString());

            }


            // substring call
            else if (op == "7")
            {
                Console.WriteLine("You chose substring...\n");

                Console.WriteLine("Enter the start index:\n ");
                string input = Console.ReadLine();
                bool convertStartIndexToInt = Int32.TryParse(input, out int startIndex);
                Console.WriteLine("Enter the stop index\n");
                string secondInput = Console.ReadLine();
                bool convertStopIndexToInt = Int32.TryParse(secondInput, out int stopIndex);

                if (startIndex != -1 && stopIndex != -1 && stopIndex! > startIndex)
                {

                    Console.WriteLine("from starting position " + startIndex + " until the stop position " + stopIndex + " removed successfully!");
                    Console.WriteLine("The resultant string after applying substring is:\n ");
                    Console.WriteLine(rope.Substring(startIndex, stopIndex));
                }

                else
                {
                    Console.WriteLine("\nOOps something went wrong! ");
                }





            }

            //Find Call
            else if (op == "8")
            {
                Console.WriteLine("You chose Find...\n");
                Console.Write("Enter the string to find: ");
                string input = Console.ReadLine();
                int Index = rope.Find(input);

                if (Index != -1)
                {
                    Console.WriteLine($"String '{input}' found at index: {Index}");
                }
                else
                {
                    Console.WriteLine($"String '{input}' not found: {Index}");
                }
            }


            // ChatAT call 
            else if (op == "9")
            {
                Console.WriteLine("You chose ChatAt...\n");
                Console.Write("Enter the index of the character: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int index))
                {
                    char character = rope.CharAt(index);
                    if (character != '\0')
                    {
                        Console.WriteLine($"Character at index {index}: '{character}'");
                    }
                    else
                    {
                        Console.WriteLine("Index out of range.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer index.");
                }
            }

            //Indexof Call
            else if (op == "10")
            {
                Console.WriteLine("You chose IndexOf...\n");
                Console.Write("Enter the character: ");
                string input = Console.ReadLine();

                if (input.Length == 1)
                {
                    char character = input[0];
                    int index = rope.IndexOf(character);

                    if (index != -1)
                    {
                        Console.WriteLine($"Index of '{character}': {index}");
                    }
                    else
                    {
                        Console.WriteLine($"Character '{character}' not found in the rope.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a single character.");
                }
            }
            // Program Exit call
            else if (op == "0")
            {
                Console.WriteLine("You chose rebalance tree! ");

                rope.PerformRebalance();

                Console.WriteLine("Rebalanced Succesfully! ");



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