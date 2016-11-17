using System.Collections.Generic;

namespace C_4_5.projectCode
{
    class Branch
    {
        private string value;
        private Node Child;
        private List<int> Index;

        //Constructor
        public Branch(string attributeValue, List<int> index )
        {
            value = attributeValue;
            Index = index;
        }

        //Setters
        public void SetNode(Node pointsTo)
        {
            Child = pointsTo;
        }

        // Getters
        public string GetValue()
        {
            return value;
        }

        public Node GetNode()
        {
            return Child;
        }

        public List<int> GetIndexList()
        {
            return Index;
        }
    }
}
