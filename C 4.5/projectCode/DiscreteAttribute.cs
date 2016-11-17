using System;
using System.Collections.Generic;

namespace C_4_5.projectCode
{
    class DiscreteAttribute : Attributes
    {
        private string Name;
        private List<DiscreteData> Data;
        //result values
        private string PostiveResult;
        private string NegativeResult;
        //attribute possible values
        private string[] AttributeValues;

        public DiscreteAttribute(string name, string postiveResult, string negativeResult, string[] attributeValues)
        {
            Name = name;
            PostiveResult = postiveResult;
            NegativeResult = negativeResult;
            AttributeValues = attributeValues;
        }

        //Setter
        // add data to a list for the attribute
        public void AddData(int index, string value, string targetResult)
        {
            // create the Discrete data 
            DiscreteData data = new DiscreteData(index, value, targetResult);

            // store data in the list for the attribute
            Data.Add(data);
        }

        // Getters
        
        public string GetName()
        {
            return Name;
        }

        public List<DiscreteData> GetAttributesData()
        {
            return Data;
        }

        public string[] GetAttributeValues()
        {
            return AttributeValues;
        }

        // get information Gain
        public double GetInformationGain(List<int> index)
        {
            double Gain;
            // get entropy of totaal for the variable with given list
            Gain = Entropy(GetNumPostiveResults(index), GetNumNegativeResults(index), index.Count);

            // get gain for each attribute
            foreach (string value in AttributeValues)
            {
                Gain = Gain - ((double)NumberOfEntrysFor(index,value) / index.Count) * Entropy(GetNumPostiveResults(index,value), GetNumNegativeResults(index,value), NumberOfEntrysFor(index, value));
            }
            return Gain;
        }
        

        // get Entropy
        private double Entropy(int postives, int negatives, int total)
        {
            return -((double)postives / total) * (Math.Log((double)postives / total)) - ((double)negatives / total) * (Math.Log((double)negatives / total));
        }


        // getting number of Postives
        public int GetNumPostiveResults(List<int> index)
        {
            int num = 0;
            foreach (int i in index)
            {
                if (Data[i].GetTarget() == PostiveResult)
                {
                    num++;
                }
            }
            return num;
        }


        // getting number of negatives
        public int GetNumNegativeResults(List<int> index)
        {
            int num = 0;
            foreach (int i in index)
            {
                if (Data[i].GetTarget() == NegativeResult)
                {
                    num++;
                }
            }
            return num;
        }

        // getting number of Postives for an attribute value
        public int GetNumPostiveResults(List<int> index, string value)
        {
            int num = 0;
            foreach (int i in index)
            {
                if (Data[i].GetTarget() == PostiveResult && Data[i].GetValue() == value)
                {
                    num++;
                }
            }
            return num;
        }


        // getting number of negatives for an attribute value
        public int GetNumNegativeResults(List<int> index, string value)
        {
            int num = 0;
            foreach (int i in index)
            {
                if (Data[i].GetTarget() == NegativeResult && Data[i].GetValue() == value)
                {
                    num++;
                }
            }
            return num;
        }

        // number of entrys in a given attribute value
        public int NumberOfEntrysFor(List<int> index, string value)
        {
            int num = 0;
            foreach (int i in index)
            {
                if (Data[i].GetValue() == value)
                {
                    num++;
                }
            }
            return num;
        }

        // give list of index for an attribute value
        public List<int> GetAttributeValueList(List<int> index, string value)
        {
            List<int> listToReturn = null;
            foreach (int i in index)
            {
                if (Data[i].GetValue() == value)
                {
                    listToReturn.Add(i);
                }
            }
            return listToReturn;
        }
    }
}
