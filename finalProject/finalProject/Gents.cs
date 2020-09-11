using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class Gents :  Customer
    {
        private bool isHairMakeup;

        public bool IsHairMakeup { get => isHairMakeup; set => isHairMakeup = value; }
    
        public override string CustPropType
        {
            get
            {
                return isHairMakeup ? "Hair Makeup is Needed" : "Hair Makeup is  not Needed";
            }
        }
    }
}
