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

public class Node
{

    Node node = new Node();

    Node Concatenate(Node p, Node q)
    {

        return node;

    }


    Node Split(Node p, int i)
    {
        return node;
    }


    Node Rebalance()
    {

        return node;
    }

}




public class Ropes
{


    Ropes(string S)
    {               //not sure if the build should go in this constructor or we have to have the other constructor for the Node itself!
                    //double check this!

        Node Build(string s, int i, int j)
        {
            Node nd = new Node();

            return nd;
        }

    }


    void Insert(string S, int i)
    {

    }

    void Delete(int i, int j)
    {

    }

    string Substring(int i, int j)
    {
        i = 20;
        j = 2;

        string c = Substring(i, j);

        return c;
    }

    int Find(string S)
    {

        if (S == "name")
        {

            return 1;
        }
        else
        {
            return 0;
        }

    }

    char CharAt(int i)
    {

        string word = "word";
        char[] charArr = word.ToCharArray();

        char a;
        for (int j = 0; j < charArr.Length; j++)
        {
            //...
        }



        return 'a';
    }

    int IndexOf(char c)
    {

        return 1;
    }

    void Reverse()
    {

    }

    int Length()
    {
        try
        {

        }
        catch (System.Exception)
        {

            throw;
        }

        return 0;
    }

    string ToString()
    {

        return "1";
    }

    void PrintRope()
    {
        Console.WriteLine("hello world! :D ");
    }





    public static void Main(string[] args)
    {

        Console.WriteLine("Hello world");
        Ropes rp = new Ropes("hello");
        rp.Substring(20, 10);
        Node nd = new Node();

    }


}




