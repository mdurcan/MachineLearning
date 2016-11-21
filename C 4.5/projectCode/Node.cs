using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_4_5.projectCode
{
    public class Node
    {
        //type of node
        private string nodeType;
        private List<Branch> branchs=new List<Branch>();
        private IAttributes Attribute = new ContinuousAttribute();
        private Branch lessThanBranch;
        private Branch GreaterThanBranch;
        private double Threshold;
        // the result
        private string Result;
        
        

        // Construtors
        public Node(IAttributes attribute, List<int> index )
        {
            Attribute = attribute;

            //continuous
            if (Attribute.GetAttributeType() == "continuous")
            {
                Threshold = attribute.GetThreshold();

                // setting the Branchs
                lessThanBranch = new Branch("less", Attribute.GetLessThanList(index, Threshold));
                GreaterThanBranch = new Branch("greater", Attribute.GetGreaterThanList(index, Threshold));
                // add to branches
                branchs.Add(lessThanBranch);
                branchs.Add(GreaterThanBranch);

                // Sets the type of node
                nodeType = "Continuous";
            }
            if (Attribute.GetAttributeType() == "discrete")
            {
                
                //Create branches
                foreach (string value in Attribute.GetAttributeValues())
                {
                    branchs.Add(new Branch(value, Attribute.GetAttributeValueList(index, value)));
                }

                // sets the type of node
                nodeType = "Discrete";
            }
        }

        // the result node
        public Node(string result)
        {
            nodeType = "result";
            Result = result;
        }

        // Getters
        public string GetNodeType()
        {
            return nodeType;
        }

        public List<Branch> GetBranches()
        {
            return branchs;
        }

        public string GetAttributeName()
        {
            return Attribute.GetName();
        }

        public IAttributes GetAttribute()
        {
            return Attribute;
        }

        public double GetThreshold()
        {
            return Threshold;
        }

        public string PrintTree(int position)
        {
            //prints the result
            if (nodeType=="result") return Result;

            string toprint = Attribute.GetName();
            position++;
            //print each node of each branch
            foreach (Branch branch in branchs)
            {
                //next line
                toprint = toprint + "\n";
                // how far it has to tab
                for (int i = 0; i <= position; i++)
                {
                    toprint = toprint + " \t ";
                }
                
                //next node
                toprint = toprint + branch.GetNode().PrintTree(position);
            }
            
            return toprint;
        }
    }
}
