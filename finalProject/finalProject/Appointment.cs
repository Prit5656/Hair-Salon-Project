using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class Appointment : IComparable
    {
        private string appTime;
        private Customer custData;

        public string AppTime { get => appTime; set => appTime = value; }
        public Customer CustData { get => custData; set => custData = value; }

        public int CompareTo(object obj)
        {
            return this.custData.CustName.CompareTo(((Appointment)obj).custData.CustName);
        }
    }
}