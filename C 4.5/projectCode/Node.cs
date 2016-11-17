using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_4_5.projectCode
{
    class Node
    {
        //type of node
        private string nodeType;
        private List<Branch> branchs;

        // variables for continuous attributes
        private ContinuousAttribute continuousAttribute;
        private Branch lessThanBranch;
        private Branch GreaterThanBranch;
        private double Threshold;

        // variables for discrete attributes
        private DiscreteAttribute discreteAttribute;
        

        // Construtors
        // Continous
        public Node(ContinuousAttribute attribute, List<int> index )
        {
            continuousAttribute = attribute;
            Threshold = attribute.GetThreshold();

            // setting the Branchs
            lessThanBranch = new Branch("less", continuousAttribute.GetLessThanList(index, Threshold));
            GreaterThanBranch = new Branch("greater", continuousAttribute.GetGreaterThanList(index, Threshold));
            // add to branches
            branchs.Add(lessThanBranch);
            branchs.Add(GreaterThanBranch);

            // Sets the type of node
            nodeType = "Continuous";
        }

        // Discrete
        public Node(DiscreteAttribute attribute, List<int> index)
        {
            discreteAttribute = attribute;

            //Create branches
            foreach (string value in discreteAttribute.GetAttributeValues())
            {
                branchs.Add(new Branch(value, discreteAttribute.GetAttributeValueList(index,value)));
            }

            // sets the type of node
            nodeType = "Discrete";
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

        public string GetAttribute()
        {
            string attribute = null;

            switch (nodeType)
            {
                case "Continuous":
                    attribute = continuousAttribute.GetName();
                    break;
                case "Discrete":
                    attribute = discreteAttribute.GetName();
                    break;
            }

            return attribute;
        }

        public double GetThreshold()
        {
            return Threshold;
        }
        
    }
}
