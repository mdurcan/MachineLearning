using System;
using System.Collections.Generic;
using System.Linq;

namespace C_4_5.projectCode
{
    // the continouse Attribute 
    public class ContinuousAttribute : IAttributes
    {
        private string Name;
        private List<ContinuousData> Data = new List<ContinuousData>();
        private string PostiveResult;
        private string NegativeResult;
        private double Threshold;
       
        public ContinuousAttribute() { }

        public ContinuousAttribute(string name,string postiveResult, string negativeResult)
        {
            Name = name;
            PostiveResult = postiveResult;
            NegativeResult = negativeResult;
        }

        //Setter
        // add data to a list for the attribute
        public void AddData(int index, string value, string targetResult)
        {
            // convert string input into a double
            double Value = double.Parse(value);
            // create the continouse data 
            ContinuousData data = new ContinuousData(index,Value,targetResult);
            // store data in the list for the attribute
            Data.Add(data);
        }

        // Getters
        public string GetName()
        {
            return Name;
        }

        public string GetAttributeType()
        {
            return "continuous";
        }

        public List<ContinuousData> GetAttributesData()
        {
            return Data;
        }

        // get Threshold 
        public double GetThreshold()
        {
            return Threshold;
        }

        // get Information Gain
        public double GetInformationGain(List<int> index)
        {
            // intilize Threshold for getting next information gain
            Threshold = 0;
            double informationGain = 0;
            List<ContinuousData> orderedData = null;

            // get list of data that will be used to get Informatio Gain
            List<ContinuousData> dataToSort = new List<ContinuousData>();
            foreach (int i in index)
            {
                dataToSort.Add(Data[i]);
            }
            // order list of Data
            orderedData = dataToSort.OrderBy(data => data.GetValue()).ToList();
            
            // get the ordered index to get left and right index
            List<int> orderedIndex = new List<int>();
            foreach (ContinuousData data in orderedData)
            {
                orderedIndex.Add(data.GetIndex());
            }
            
            // the first result 
            string lastResult = orderedData[0].GetTarget();

            // the tresholds to get index of whats on either side of threshold
            int thresholdIndex = 0;

            // go through list and caculate treshold at each change
            foreach (ContinuousData data in orderedData)
            {

                // if change of target result, check to see if new threhold found
                if (data.GetTarget() != lastResult)
                {
                    // get left and right index
                    List<int> leftIndex = new List<int>();
                    List<int> rightIndex = new List<int>();
                    for (int i = 0; i < orderedIndex.Count(); i++)
                    {
                        if (i < thresholdIndex)
                        {
                            leftIndex.Add(orderedIndex[i]);
                        }
                        else
                        {
                            rightIndex.Add(orderedIndex[i]);
                        }
                    }

                    // get information gain
                    double gainForNewThrehold = InformationGain(index, leftIndex, rightIndex);

                    // if bigger then last information gain this threshold better then the last one
                    if (gainForNewThrehold > informationGain)
                    {
                        Threshold = data.GetValue();
                        informationGain = gainForNewThrehold;
                    }
                }
                //increase the possible threshold location
                thresholdIndex++;
            }


            // return information gain
            return informationGain;
        }




        // information gain
        private double InformationGain(List<int> index, List<int> leftSideIndex, List<int> rightSideIndex   )
        {
            double Gain;
            // get entropy of totaal for the variable with given list
            Gain = Entropy(GetNumPostiveResults(index), index.Count)+ Entropy(GetNumNegativeResults(index), index.Count);

            //get entropy for left side
            Gain = Gain - (double) leftSideIndex.Count/index.Count*(
                       Entropy(GetNumPostiveResults(leftSideIndex),
                           leftSideIndex.Count) + Entropy(GetNumNegativeResults(leftSideIndex),
                           leftSideIndex.Count));
            //get entropy for right side
            Gain = Gain - (double) rightSideIndex.Count/index.Count*(
                       Entropy(GetNumPostiveResults(rightSideIndex),
                           rightSideIndex.Count) + Entropy(GetNumNegativeResults(rightSideIndex),
                           rightSideIndex.Count));

            // return the gain
            return Gain;
        }
        

        // get Entropy
        private double Entropy(int value, int total)
        {
            if (value == 0)
            {
                return 0.0;
            }
            return -((double)value / total) * (Math.Log((double)value / total));
        }

        // getting number of Postives
        public int GetNumPostiveResults(List<int> index)
        {
            int num=0;
            foreach(int i in index)
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
        

        // give list of index below threshold
        public List<int> GetLessThanList(List<int> index, double threshold)
        {
            List<int> listToReturn = new List<int>();
            foreach (int i in index)
            {
                if (Data[i].GetValue() < threshold)
                {
                    listToReturn.Add(i);
                }
            }
            return listToReturn;
        }

        // give list of index greater than threshold
        public List<int> GetGreaterThanList(List<int> index, double threshold)
        {
            List<int> listToReturn = new List<int>();
            foreach (int i in index)
            {
                if (Data[i].GetValue() >= threshold)
                {
                    listToReturn.Add(i);
                }
            }
            return listToReturn;
        }

        //should not be called
        public string[] GetAttributeValues(){return null;}
        public List<int> GetAttributeValueList(List<int> index, string value){return null;}
        
    }
}
