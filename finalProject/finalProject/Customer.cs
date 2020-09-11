using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class Customer : ICustomer    
    {
        private string custName;
        private string contNum;
        private string homeTreat;
        private string custType;
        private string year;
        //Delegate
        public string CustName { get => custName; set => custName = value; }
        public string ContNum { get => contNum; set => contNum = value; }
        public string HomeTreat{ get => homeTreat; set => homeTreat = value; }
        public string CustType { get => custType; set => custType = value; }
        public string Year { get => year; set => year = value; }
        public virtual string CustPropType { get; }
    }
}
