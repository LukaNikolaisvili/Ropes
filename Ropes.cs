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

            if (S != "" || i == -1)
            {
                // Split the node at index i
                Node rightSide = Split(this.root, i);

                // Create a new node to hole the string to be inserted
                Node newNode = new Node(S);

                // concatenate the current node with the new node
                Node leftSide = Concatenate(this.root, newNode);

                Console.WriteLine("The size of S is in IF:" + S.Length);
                // concatenate the left side of the tree with the tree exported from split
                this.root = Concatenate(leftSide, rightSide);


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
            // Split the rope from indices i to j
            // Make a node that stores up until index i
            Node leftPart = Split(root, i);
            // Make a node that holds the string to be deleted
            Node middlePart = Split(leftPart.Right, j - i);
            // Node to hold remainer
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
        int L = LengthRope();
        try
        {
            // If indices are out of range or invalid, return an empty string.
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
        try
        {
            if (root == null)
            {
                // No node so return empty string
                return "";
            }
            else
            {
                // Use stringbuilder and stack for recursion
                StringBuilder strBuild = new StringBuilder();
                Stack<Node> stack = new Stack<Node>();
                stack.Push(root);

                while (stack.Count > 0)
                {
                    Node curr = stack.Pop();

                    if (curr.Value != null)
                    {
                        // If value in current node add to the stringbuilder
                        strBuild.Append(curr.Value);
                    }
                    else
                    {
                        if (curr.Right != null)
                        {
                            // Add right child onto stack
                            stack.Push(curr.Right);
                        }

                        if (curr.Left != null)
                        {
                            // Add left child onto stack
                            stack.Push(curr.Left);
                        }
                    }
                }
                // Return the built string
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

            // Create indentation for node printing
            string indent = new String(' ', indentation * 2);

            if (node.Value != null)
            {
                // If the node has a string print with this format
                Console.WriteLine("{0}( Size: {1} | Value: '{2}' )", indent, node.Size, node.Value);
            }
            else
            {
                // If node is only size print using this format
                Console.WriteLine("{0}( Size: {1} )", indent, node.Size);
            }
            // Print children with same indent
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


    // Split the rope with root p at index i and return the root of the right subtree (9 marks).
    private Node Split(Node p, int i)
    {
        try
        {

            int LeftSize; // Store the size of the left child
            Node rightChild;
            Node result;

            // If the passed node is null return null.
            // If the passed index is out of bounds return null.
            if (p == null || i < 0)
            {
                Console.WriteLine("The passed node does not exist!");
                return null;
            }

            // Validate the right child exists as well.
            if (p.Right != null)
            {
                rightChild = p.Right;
            }
            else
            {
                rightChild = null;
            }

            // If left child isn't null save its value
            if (p.Left != null)
            {
                LeftSize = p.Left.Size;
            }
            // Otherwise set value to -1
            else
            {
                LeftSize = -1;
            }

            // If the position passed is smaller or equal to the size of the left subtree, it means
            // the split location is left. Recursively call for left node.
            if (i <= LeftSize)
            {
                result = Split(p.Left, i);
            }
            else
            {
                // Subtract the size of the left subtree from i, 
                // this way recursive if statements still work for the right side.
                i -= LeftSize;
                result = Split(rightChild, i);
            }

            //After all recursion is done and you are in the desired leaf node
            // Split!
            int length = p.Size; // Save the length
            int j;              // Int for looping
                                // Create and set left and right children
            Node leftsplit = new Node(null);
            Node rightsplit = new Node(null);
            p.Left = leftsplit;
            p.Right = rightsplit;

            // Set sizes of children
            leftsplit.Size = i + 1; // i + 1 as indices start at 0
            rightsplit.Size = length - (leftsplit.Size); // remaining length for the right node
            string word = p.Value;
            string left = word.Substring(0, leftsplit.Size);
            string right = word.Substring(leftsplit.Size);
            leftsplit.Value = left;
            rightsplit.Value = right;

            p.Value = null;

            // TODO make the red line!
        }
        catch (Exception ex)
        {
            // Handle the exception here, you can log it or perform any other necessary actions.
            Console.WriteLine("error occurred: " + ex.Message);
        }
        return null;
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
        // Testing nodes
        Node node = new Node("LUKA");
        Node node1 = new Node("NIKOLAISVILI");
        Rope rope = new Rope(node.Value + node1.Value);

        Console.WriteLine(rope.Split(node, 1));

        // Console.WriteLine(node.Value);

        Node node3 = new Node(""); // L U K A N I K O

        // node3 = rope.Concatenate(node, node1);

        // Console.WriteLine(node3.Value);

        // rope.PrintRope();

        // rope.Insert("777", 7); //insert method might have some issues, got to take a look at it in depth.


        // rope.Insert("LUKADATO", 5);

        //think split method has some issues, got to take care of that


        // rope.PrintRope();

        // Console.WriteLine(rope.CharAt(0)); //ideally should print the 0th index or the first character in the string

        // rope.Delete(0, 7);

        // rope.Reverse();

        // rope.PrintRope();






        bool flag = true;

        // Console.WriteLine(rope.ToString());


        Console.WriteLine(rope.IndexOf(node, 'L'));

        // Console.WriteLine(rope.Split(node, 1));

        // rope.PrintRope();


        while (flag)
        {
            // User interface
            Console.WriteLine("\nHello, you can perform any of these operations!");
            Console.WriteLine("-----------------");
            Console.WriteLine("1 - Insert\n2 - Split\n3 - Delete\n4 - Reverse\n5 - Print\n6 - ToString\n7 - Substring\n8 - Find\n9 - Remove\n0 - Build\nx - Exit");
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
                    Console.WriteLine(rope.Split(node, index));
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
                bool convertStopIndexToInt = Int32.TryParse(secondInput, out int stopIndex);

                if (startIndex != -1 && stopIndex != -1 && stopIndex! > startIndex)
                {

                    rope.Delete(startIndex, stopIndex);

                    Console.WriteLine("from starting position " + startIndex + " until the stop position " + stopIndex + " removed successfully!");
                }

                else
                {
                    Console.WriteLine("Oops, something went wrong try again!");
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
                Console.WriteLine("Printing...");
                rope.PrintRope();




            }

            // String conversion call
            else if (op == "6")
            {
                Console.WriteLine("you chose ToString!");
                Console.WriteLine("\nshowing what is rope made of:\n");
                Console.WriteLine(rope.ToString());

            }



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


            else if (op == "8")
            {
                Console.WriteLine("You chose Find! ");
                Console.WriteLine("\nPlease enter the string that you want to find:\n ");

                string findString = Console.ReadLine();

                if (findString != null)
                {
                    Console.WriteLine(rope.Find(findString));
                }

                else
                {
                    Console.WriteLine("Oops something went wrong!");
                }

            }


            else if (op == "9")
            {
                
                Console.WriteLine(rope.LengthRope());
            }

            // Program Exit call
            else if (op == "0")
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