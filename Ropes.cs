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
        public int size;
        public Node Left;
        public Node Right;

        public Node(string value)
        {
            Value = value;
            size = 0;
            Left = null;
            Right = null;
        }
    }

    private Node root;
    Node node = new Node(null);

 
    public Rope(string S)
    {
        root = Build(S, 0, S.Length);
    }

   // Insert string S at index i (5 marks).
    public void Insert(string S, int i)
    {
    
    }
   // Delete the substring S[i; j] (5 marks).
    public void Delete(int i, int j)
    {
      
    }

    // Return the substring S[i; j] (6 marks).
    public string Substring(int i, int j)
    {
        
        return "";
    }
    // Return the index of the rst occurrence of S; -1 otherwise (9 marks).
    public int Find(string S)
    {
      
        return -1;
    }

    // Return the character at index i (3 marks).
    public char CharAt(int i)
    {
  
        return ' ';
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

        if (node.size != 0){
            return node.size;
        }
        else{
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
     
    }

   //private methods

   // Recursively build a balanced rope for S[i; j] and return its root (part of the constructor).
    private Node Build(string s, int i, int j)
    {
       
        return null;
    }

    // Return the root of the rope constructed by concatenating two ropes with roots p and q (3 marks).
    private Node Concatenate(Node p, Node q)
    {
     
        return null;
    }

    // Split the rope with root p at index i and return the root of the right subtree (9 marks).
    private Node Split(Node p, int i)
    {
       
        return null;
    }
    
    // Rebalance the rope using the algorithm found on pages 1319-1320 of Boehm et al. (9 marks).
    private Node Rebalance()
    {
       
        return null;
    }
}