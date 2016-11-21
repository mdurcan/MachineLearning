using System;
using System.Collections.Generic;

namespace C_4_5.projectCode
{
    public class DiscreteAttribute : IAttributes
    {
        private string Name;
        private List<DiscreteData> Data=new List<DiscreteData>();
        //result values
        private string PostiveResult;
        private string NegativeResult;
        //attribute possible values
        private string[] AttributeValues;

        public DiscreteAttribute() { }

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

        public string GetAttributeType()
        {
            return "discrete";
        }

        // get information Gain
        public double GetInformationGain(List<int> index)
        {
            double Gain=0.0;
            // get entropy of totaal for the variable with given list
            Gain = Entropy(GetNumPostiveResults(index), index.Count)+ Entropy(GetNumPostiveResults(index), index.Count);

            // get gain for each attribute
            foreach (string value in AttributeValues)
            {
                Gain = Gain -
                       (double) NumberOfEntrysFor(index, value)/index.Count*(
                           Entropy(GetNumPostiveResults(index, value), NumberOfEntrysFor(index, value)) +
                           Entropy(GetNumNegativeResults(index, value), NumberOfEntrysFor(index, value)));
            }
            return Gain;
        }
        

        // get Entropy
        private double Entropy(int value, int total)
        {
            if (value == 0)
            {
                return 0.0;
            }
            double toReturn = -((double)value / total) * (Math.Log((double)value / total));
            return toReturn;
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
            List<int> listToReturn = new List<int>();
            foreach (int i in index)
            {
                if (Data[i].GetValue() == value)
                {
                    listToReturn.Add(i);
                }
            }
            return listToReturn;
        }

        // wont be used
        public double GetThreshold(){return 0;}
        public List<int> GetLessThanList(List<int> index, double Threshold){return null;}
        public List<int> GetGreaterThanList(List<int> index, double Threshold){return null;}
    }
}
