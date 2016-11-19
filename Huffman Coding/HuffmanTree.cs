namespace Huffman_Coding
{
    class HuffmanTree
    {
        public Node Root;

        public HuffmanTree()
        {
            Root = null;
        }
        
        public void Insert(HufmannLetter id, int position, HufmannLetter parent)
        {
            var newNode = new Node(id) {Letter = id};

            if (Root == null)
                Root = newNode;
            else
            {
                Node parentNode = FindNode(Root, parent.LetterName);
                if (parentNode != null)
                {
                    if (position == 0)
                    {
                        parentNode.Left = newNode;                       
                        newNode.ByteOfPath = parentNode.ByteOfPath + "0";                     
                    }
                    else
                    {
                        parentNode.Right = newNode;
                        newNode.ByteOfPath = parentNode.ByteOfPath + "1";                      
                    }
                    parentNode.Childrens.Add(newNode);
                    id.LetterPath = newNode.ByteOfPath;
                }              
            }
        }

        Node FindNode(Node rootNode, string stringToFind)
        {
            if (rootNode.Letter.LetterName == stringToFind)
                return rootNode;
            foreach (var child in rootNode.Childrens)
            {
                var n = FindNode(child, stringToFind);
                if (n != null) return n;
            }
            return null;
        }      
    }
}