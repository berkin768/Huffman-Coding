namespace Huffman_Coding
{
    class HuffmanTree
    {
        public Node Root;

        public HuffmanTree()
        {
            Root = null;
        }
        
        public void Insert(HufmannLetter id, int position, HufmannLetter parent) //id = which node we will insert?, pos = left or right, parent = connect to which node?
        {
            var newNode = new Node(id);

            if (Root == null)
                Root = newNode;
            else
            {
                Node parentNode = FindNode(Root, parent.LetterName);  //Find our node's parent in the list
                if (parentNode != null)  //if we found it
                {
                    if (position == 0)   //insert to left
                    {
                        parentNode.Left = newNode;                       
                        newNode.ByteOfPath = parentNode.ByteOfPath + "0";                 //add 0 to its path
                    }
                    else
                    {
                        parentNode.Right = newNode;
                        newNode.ByteOfPath = parentNode.ByteOfPath + "1";                      
                    }
                    parentNode.Childrens.Add(newNode);  //set it's parent
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