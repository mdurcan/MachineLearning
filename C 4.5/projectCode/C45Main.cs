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
        private List<Attributes> attributes;
        // index of entrys 
        private List<int> Index = new List<int>();

        // the target results
        private string TargetName;
        private string PostiveResult;
        private string NegativeResult;
        

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
                        string[] attributeValues = null;
                        for (int i = 3; i < values.Count(); i++)
                        {
                            attributeValues[i - 3] = values[i];
                        }

                        //create and store attribute
                        attributes.Add(new DiscreteAttribute(values[2].ToLowerInvariant(),PostiveResult,NegativeResult,attributeValues));
                    }
                    //if the attribute is a continous
                    else if (Equals("continuous", values[1].ToLowerInvariant()))
                    {
                        attributes.Add(new ContinuousAttribute(values[2].ToLowerInvariant(), PostiveResult, NegativeResult));
                    }
                }
                //check for entrys, will follow after @entries
                else if (Equals("@entries", values[0].ToLowerInvariant()))
                {
                    entrysFound = true;
                }
                // input the entrys
                else if (entrysFound && string.IsNullOrEmpty(line))
                {
                    for (int i = 0; i < attributes.Count; i++)
                    {
                        attributes[i].AddData(numberOfEntrys,values[i],values[attributes.Count]);
                    }
                    numberOfEntrys++;
                }  
            }
        }

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
    }

    
}
