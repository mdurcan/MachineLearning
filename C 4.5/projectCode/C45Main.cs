using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace C_4_5.projectCode
{
    
    public class C45Main
    {
        //attributes for tree
        private List<IAttributes> attributes= new List<IAttributes>();
        // index of entrys 
        private List<int> Index = new List<int>();

        // the target results
        private string TargetName;
        private string PostiveResult;
        private string NegativeResult;

        // the three
        private Node StartingNode=new Node(null) ;
        


        // Getters
        public string GetTargetName()
        {
            return TargetName;
        }

        public string GetPostiveResult()
        {
            return PostiveResult;
        }

        public string GetNegativeResult()
        {
            return NegativeResult;
        }

        public List<IAttributes> GetAttributes()
        {
            return attributes;
        }


        // read in txt file
        public void ReadInData(string filePath)
        {
            //saves the file to a string
            string[] txt = System.IO.File.ReadAllLines(@filePath);
            List<string> data = new List<string>();
            bool entrysFound = false;
            int numberOfEntrys = 0;

            //display contents and replace brackets
            Console.WriteLine("Contents of File");
            foreach (string line in txt)
            {
                Console.WriteLine("\t" + line);
                data.Add(Regex.Replace(line.Replace('{', ',').Replace('}', ' '), @"\s",""));
            }
            
            // split up the data
            foreach (var line in data)
            {
                Console.WriteLine(line);
                //split line
                var values = line.Split(',');
                
                // find target
                if (Equals("@target", values[0].ToLowerInvariant()))
                {
                    TargetName = values[1];
                    PostiveResult = values[2];
                    NegativeResult = values[3];
                }
                // Set up the attributes
                else if(Equals("@attribute", values[0].ToLowerInvariant())) 
                {
                    // if the attribute is a discrete
                    if (Equals("discrete", values[1].ToLowerInvariant()))
                    {
                        // get the possible discrete attribute values
                        string[] attributeValues = new string[values.Count()-3];;
                        int position = 0;
                        for (int i = 3; i < values.Count(); i++)
                        {
                            attributeValues[position] = values[i];
                            position++;
                        }
                        
                        //create and store attribute
                        attributes.Add(new DiscreteAttribute(values[2], PostiveResult, NegativeResult, attributeValues));
                    }
                    //if the attribute is a continous
                    else if (Equals("continuous", values[1].ToLowerInvariant()))
                    {
                        attributes.Add(new ContinuousAttribute(values[2], PostiveResult, NegativeResult));
                    }
                }
                //check for entrys, will follow after @entries
                else if (Equals("@entries", values[0].ToLowerInvariant()))
                {
                    entrysFound = true;
                }
                // input the entrys
                else if (entrysFound)
                {
                    for (int i = 0; i < attributes.Count; i++)
                    {
                        attributes[i].AddData(numberOfEntrys,values[i],values[attributes.Count]);
                    }
                    Index.Add(numberOfEntrys);
                    numberOfEntrys++;
                }  
            }
        }

        // create the tree
        public void CreateTree()
        {
            IAttributes attribute = GetAttributeWithHighestInformationGain(Index);
            StartingNode = new Node(attribute, Index);

            foreach (Branch branch in StartingNode.GetBranches())
            {
                if (StartingNode.GetAttribute().GetNumPostiveResults(branch.GetIndexList()) > branch.GetIndexList().Count * 0.7)
                {
                    branch.SetNode(new Node(PostiveResult));
                }
                else if (StartingNode.GetAttribute().GetNumNegativeResults(branch.GetIndexList()) > branch.GetIndexList().Count * 0.7)
                {
                    branch.SetNode(new Node(NegativeResult));
                }
                else
                {
                    branch.SetNode(CreateNodes(branch.GetIndexList()));
                }
            }
        }

        // create Node 
        public Node CreateNodes(List<int> index)
        {
            IAttributes attribute = GetAttributeWithHighestInformationGain(index);
            Node node = new Node(attribute, index);

            // add nodes to the Branchs 
            foreach (Branch branch in node.GetBranches())
            {
                if (node.GetAttribute().GetNumPostiveResults(branch.GetIndexList()) > branch.GetIndexList().Count*0.7)
                {
                    branch.SetNode(new Node(PostiveResult));
                }
                else if (node.GetAttribute().GetNumNegativeResults(branch.GetIndexList()) > branch.GetIndexList().Count*0.7)
                {
                    branch.SetNode(new Node(NegativeResult));
                }
                else
                {
                    branch.SetNode(CreateNodes(branch.GetIndexList()));
                }
            }
            return node;
        }

        //get attribute of highest information Gain
        public IAttributes GetAttributeWithHighestInformationGain(List<int> index )
        {
            IAttributes attributeChosen = new ContinuousAttribute();
            double highestGain = 0;

            //go through attributes to get one with highest gain to divide 
            foreach (IAttributes attribute in attributes)
            {
                if (attribute.GetInformationGain(index) > highestGain)
                {
                    attributeChosen = attribute;
                    highestGain = attribute.GetInformationGain(index);
                }
            }
            return attributeChosen;
        }

        
        // print the tree
        public string PrintTree()
        {
            int position = 0;
            string toPrint= "Tree \n" + StartingNode.PrintTree(position);

            Console.WriteLine(toPrint);

            return toPrint;
        }


    }
}
