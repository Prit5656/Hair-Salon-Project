using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class Children : Customer
    {
        private bool isHaircutNeeded;

        public bool IsHaircutNeeded { get => isHaircutNeeded; set => isHaircutNeeded = value; }

        public override string CustPropType
        {
            get
            {
                return isHaircutNeeded ? "Hair Cut is required" : "Hair Cut is not required";
            }
        }
    }
}
