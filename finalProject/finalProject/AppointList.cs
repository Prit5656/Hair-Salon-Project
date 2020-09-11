using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace finalProject
{
    [XmlRoot("AppointList")]
    [XmlInclude(typeof(Ladies))]
    [XmlInclude(typeof(Gents))]
    [XmlInclude(typeof(Children))]
    public class AppointList : IEnumerable<Appointment>, IDisposable
    {
        [XmlArray("Appointments")]
        [XmlArrayItem("Appointment", typeof(Appointment))]
        List<Appointment> appList = null;

        public AppointList()
        {
            appList = new List<Appointment>();
        }
        public void Add(Appointment appointment)
        {
            appList.Add(appointment);
        }
        public void Remove(int index)
        {
            appList.RemoveAt(index);
        }
        public IEnumerator<Appointment> GetEnumerator()
        {
            return ((IEnumerable<Appointment>)appList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Appointment>)appList).GetEnumerator();
        }
        public Appointment this[int index]
        {
            get
            { 
                return appList[index]; 
            }
            set { appList[index] = value; }
        }
        public int Count
        {
            get { return appList.Count; }
        }

        public void Sort()
        {
            appList.Sort();
        }

        public void Clear()
        {
            appList.Clear();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
