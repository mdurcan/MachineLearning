using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_4_5.projectCode
{
    class Attributes
    {
        private string Name;

        protected Attributes()
        {
            throw new NotImplementedException();
        }

        public void AddData(int index, string value, string targetResult)
        {
        }

        public string GetName()
        {
            return Name;
        }

    }
}
