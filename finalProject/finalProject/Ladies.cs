using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public class Ladies : Customer
    {
        private bool isHairColour;

        public bool IsHairColour { get => isHairColour; set => isHairColour = value; }

       public override string CustPropType
        {
            get
            {
                return isHairColour ? "Hair colour is required" : "Hair colour is not required";
            }
        }
            
        
    }
}
