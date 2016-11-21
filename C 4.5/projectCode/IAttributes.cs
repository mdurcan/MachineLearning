using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace C_4_5.projectCode
{
    public interface IAttributes
    {
        void AddData(int index, string value, string targetResult);
        string GetName();
        double GetInformationGain(List<int> index);
        int GetNumNegativeResults(List<int> index);
        int GetNumPostiveResults(List<int> index);

        //know what type of attribute it is
        string GetAttributeType();

        //get attribute values for branchs, for discrete, shouldn't be called in continous
        string[] GetAttributeValues();
        List<int> GetAttributeValueList(List<int> index, string value);

        //get threshold, for continous, wont be called in discrete
        double GetThreshold();
        List<int> GetLessThanList(List<int> index ,double Threshold );
        List<int> GetGreaterThanList(List<int> index, double Threshold);
    }
}
