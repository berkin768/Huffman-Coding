using System.Collections.Generic;

namespace Huffman_Coding
{
    class Node
    {
        public HufmannLetter Letter { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public string ByteOfPath { get; set; }
        public List<Node> Childrens { get; set; }
        public Node(HufmannLetter nodeName)
        {
            ByteOfPath = "";
            Letter = nodeName;
            Left = null;
            Right = null;
            Childrens = new List<Node>();
        }
    }
}