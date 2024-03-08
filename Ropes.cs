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
        public int Weight;
        public Node Left;
        public Node Right;

        public Node(string value)
        {
            Value = value;
            Weight = value.Length;
            Left = null;
            Right = null;
        }
    }

    private Node root;

 
    public Rope(string S)
    {
        root = Build(S, 0, S.Length);
    }

   
    public void Insert(string S, int i)
    {
    
    }

    public void Delete(int i, int j)
    {
      
    }

    public string Substring(int i, int j)
    {
        
        return "";
    }

    public int Find(string S)
    {
      
        return -1;
    }

    public char CharAt(int i)
    {
  
        return ' ';
    }

    public int IndexOf(char c)
    {
       
        return -1;
    }

    public void Reverse()
    {
     
    }

    public int Length()
    {
        
        return 0;
    }

    public override string ToString()
    {
       
        return "";
    }

    public void PrintRope()
    {
     
    }

   //private methods
    private Node Build(string s, int i, int j)
    {
       
        return null;
    }

    private Node Concatenate(Node p, Node q)
    {
     
        return null;
    }

    private Node Split(Node p, int i)
    {
       
        return null;
    }

    private Node Rebalance()
    {
       
        return null;
    }
}