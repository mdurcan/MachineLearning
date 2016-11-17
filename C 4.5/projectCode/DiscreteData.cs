namespace C_4_5.projectCode
{
    // This is the data for discrete attributes 
    class DiscreteData 
    {
        private int Index;
        private  string AttributeValues;
        private string TargetResult;

        public DiscreteData(int index, string value, string target)
        {
            Index = index;
            AttributeValues = value;
            TargetResult = target;
        }

        public int GetIndex()
        {
            return Index;
        }

        public string GetValue()
        {
            return AttributeValues;
        }

        public string GetTarget()
        {
            return TargetResult;
        }
    }
}
