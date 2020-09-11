using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProject
{
    public interface ICustomer
    {
        string CustName { get; set; }
        string ContNum { get; set; }
        string HomeTreat { get; set; }
        string CustType { get; set; }
        string Year { get; set; }
        string CustPropType { get; }
    }
}
