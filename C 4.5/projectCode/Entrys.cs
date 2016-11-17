using System;

namespace C_4_5.projectCode
{
    // the entrys of data used for testing and training 
    class Entrys
    {
        private int Index;
        private Array Values;
        
        public Entrys(int index, Array values)
        {
            Index = index;
            Values = values;
        }

        // Getters
        public int GetIndex()
        {
            return Index;
        }

        public Array GetArray()
        {
            return Values;
        }
        
    }
}
