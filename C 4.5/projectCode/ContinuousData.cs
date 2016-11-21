namespace C_4_5.projectCode
{
    // This is the data for continouse attributes 
    public class ContinuousData 
    {
        private int Index;
        private double AttributeValues;
        private string TargetResult;

        public ContinuousData(int index, double value, string target)
        {
            Index = index;
            AttributeValues = value;
            TargetResult = target;
        }

        // Getters
        public int GetIndex()
        {
            return Index;
        }

        public double GetValue()
        {
            return AttributeValues;
        }

        public string GetTarget()
        {
            return TargetResult;
        }
    }
}
