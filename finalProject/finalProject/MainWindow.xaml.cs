using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace finalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        Hashtable Data = null;
        AppointList appointList = null;
        ObservableCollection<Appointment> Collection = null;

        string Time = string.Empty;
        string custName = string.Empty;
        string contNumber = string.Empty;
        string Age = string.Empty;
        int ageint = 0;
        bool homeTreatment;
        string custType = string.Empty;
        bool requirement;
        bool result = true;
        string xmlfile = "Data.xml";

       
        public MainWindow()
        {
            InitializeComponent();

            Validation.AddErrorHandler(this.AppointBox, TimeValidationError);
            Validation.AddErrorHandler(this.CustomerNameText, NameValidationError);
            Validation.AddErrorHandler(this.CustomerNumberText, NumberValidationError);
            Validation.AddErrorHandler(this.txtAge, AgeValidationError);

            Data = new Hashtable();
            Data = AddAppointment();
            DataContext = this;
            appointList = new AppointList();
            Collection = new ObservableCollection<Appointment>();
            ArrayList arrayList = new ArrayList(Data.Keys);
            for (int i = 0; i < arrayList.Count; i++) {
                AppointBox.Items.Add(Data[i]);
            }
           Display();
           
        }

       


        private void Addbutton_Click(object sender, RoutedEventArgs e)
        {
            ErrorLabel.Content = "";
            ErrorLabel.Foreground = Brushes.Black;
            AppointBox.ClearValue(TextBox.BorderBrushProperty);
            CustomerNameText.ClearValue(TextBox.BorderBrushProperty);
            CustomerNumberText.ClearValue(TextBox.BorderBrushProperty);
            comboBoxTypeOfCustomer.ClearValue(TextBox.BorderBrushProperty);

            Time = AppointBox.Text.ToString();
            custName = CustomerNameText.Text;
            contNumber = CustomerNumberText.Text;
            custType = comboBoxTypeOfCustomer.Text;
            Age = txtAge.Text;

            ///validation for appointment combo box
            do
            {
                if (AppointBox.SelectedIndex == -1)
                {
                    result = false;
                    MessageBox.Show("Please select Appointment");
                    break;
                }
                else
                {

                    AppointBox.BorderBrush = Brushes.Black;
                    break;
                }
            } while (true);


            //Validation for Name
            do
            {
                if (custName.Trim().Length != 0)
                {

                    CustomerNameText.Foreground = Brushes.Black;
                    char[] customerNameChar = custName.ToArray();
                    foreach (char index in customerNameChar)
                    {
                        if (!char.IsLetter(index))
                        {
                            result = false;
                            ErrorLabel.Content = "Please enter valid customer name";
                            ErrorLabel.Foreground = Brushes.Red;
                            CustomerNameText.Foreground = Brushes.Red;
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    result = false;
                    ErrorLabel.Content = "Please enter valid customer name";
                    ErrorLabel.Foreground = Brushes.Red;
                    CustomerNameText.Foreground = Brushes.Red;
                    break;
                }
            } while (true);

            ///validation for Number
            do
            {
                if (contNumber.Trim().Length != 0)
                {

                    CustomerNumberText.Foreground = Brushes.Black;
                    char[] contactNumberChar = contNumber.ToArray();
                    foreach (char index in contactNumberChar)
                    {
                        if (!char.IsNumber(index))
                        {
                            result = false;
                            ErrorLabel.Content = "Please enter valid contact name";
                            ErrorLabel.Foreground = Brushes.Red;
                            CustomerNumberText.Foreground = Brushes.Red;
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    result = false;
                    ErrorLabel.Content = "Please enter valid contact name";
                    ErrorLabel.Foreground = Brushes.Red;
                    CustomerNumberText.Foreground = Brushes.Red;
                    break;
                }
            } while (true);

            ///validation for the Age
            do {

                if (Age.Trim().Length == 0)
                {
                    result = false;
                    txtAge.Foreground = Brushes.Red;
                    break;
                }
                else if (int.TryParse(Age, out ageint) && ageint > 1 && ageint < 100)
                {
                    ErrorLabel.Content = "";

                    txtAge.Foreground = Brushes.Black;
                    break;
                }
                else {
                    result = false;
                    ErrorLabel.Content = "Please enter valid age [1-100]";
                    ErrorLabel.Foreground = Brushes.Red;
                    txtAge.Foreground = Brushes.Red;
                    break;
                }
            } while (true);

            custType = comboBoxTypeOfCustomer.Text.ToString();

            if (result)
            {
                try
                {
                    Time = (string)Data[AppointBox.SelectedIndex];
                    Data.Remove(AppointBox.SelectedIndex);
                    AppointBox.Items.Clear();
                    ArrayList arrayList = new ArrayList(Data.Keys);
                    for (int i = 0; i < arrayList.Count; i++)
                    {
                        AppointBox.Items.Add(Data[i]);
                    }


                    ///input fields will be cleared
                    CustomerNameText.Text = "";
                    CustomerNumberText.Text = "";
                    txtAge.Text = "";
                    comboBoxTypeOfCustomer.SelectedIndex = 0;

                    //Saving data 
                    Customer type = null;
                    switch (comboBoxTypeOfCustomer.SelectedIndex)
                    {
                        case 0:
                            {
                                type = new Ladies()
                                {
                                    CustName = custName,
                                    ContNum = contNumber,
                                    HomeTreat = homeTreatment ? "Home treatment is Required" : " Home treatment is Not Required",
                                    CustType = custType,
                                    Year = Age,
                                    IsHairColour = requirement
                                };
                                break;
                            }
                        case 1:
                            {
                                type = new Gents()
                                {
                                    CustName = custName,
                                    ContNum = contNumber,
                                    HomeTreat = homeTreatment ? "Home treatment is Required" : "Home treatment is Not Required",
                                    CustType = custType,
                                    Year = Age,
                                    IsHairMakeup = requirement
                                };
                                break;
                            }
                        case 2:
                            {
                                type = new Children()
                                {
                                    CustName = custName,
                                    ContNum = contNumber,
                                    HomeTreat = homeTreatment ? "Home treatment is Required" : "Home treatment is Not Required",
                                    CustType = custType,
                                    Year = Age,
                                    IsHaircutNeeded = requirement
                                };
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    Appointment Obj = new Appointment()
                    {
                        AppTime = Time,
                        CustData = type
                    };

                    appointList.Add(Obj);
                    Insert();
                }
                catch (Exception exc)
                {
                    
                }
                
            }

        }

        public void Insert() {            
            XmlSerializer serializer = new XmlSerializer(typeof(AppointList));
            TextWriter textWriter = new StreamWriter(xmlfile);
            serializer.Serialize(textWriter, appointList);
            textWriter.Close();
            appointList.Clear();
            MessageBox.Show("Appointment boooked successfully!");
        }

        public Hashtable AddAppointment() {
            Hashtable appointmentData = new Hashtable();
            int hour = 9;
            string timeString = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                timeString = hour.ToString() + ":00";
                appointmentData.Add(i, timeString);
                hour++;
            }
            return appointmentData;
        }

        private void RadioPropYes_Checked(object sender, RoutedEventArgs e)
        {
            requirement = true;
        }

        private void RadioPropNo_Checked(object sender, RoutedEventArgs e)
        {
            requirement = false;
        }

        private void RadioServiceYes_Checked(object sender, RoutedEventArgs e)
        {
            homeTreatment = true;
        }

        private void RadioServiceNo_Checked(object sender, RoutedEventArgs e)
        {
            homeTreatment = false;
        }

        private void ComboBoxCustomerType(object sender, SelectionChangedEventArgs e)
        {
            try {
                switch (comboBoxTypeOfCustomer.SelectedIndex)
                {
                    case 0:
                        {
                            HairColorLabel.Content = "Do you want Hair colour ?";
                            break;
                        }
                    case 1:
                        {
                            HairColorLabel.Content = "Do you want to Hair Makeup?";
                            break;
                        }
                    case 2:
                        {
                            HairColorLabel.Content = "Do you want Hair Cut?";
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception ex) 
            {

            }
           
        }

       private void Displaybutton_Click(object sender, RoutedEventArgs e)
        {
           Display();
        }

        public void Display() {
            Grid.DataContext = null;
            if (File.Exists(xmlfile)) {
                appointList.Clear();

                Grid.Items.Refresh();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppointList));
                StreamReader streamReader = new StreamReader(xmlfile);
                appointList = (AppointList)xmlSerializer.Deserialize(streamReader);
                streamReader.Close();

                Collection.Clear();
               
                    foreach (Appointment appointment in appointList)
                    {
                    
                    Collection.Add(appointment);
                    AppointBox.Items.Remove(appointment.AppTime);
                    
                    }


                Grid.ItemsSource = Collection;

            }           
        }
        
        private void Deletebutton_Click(object sender, RoutedEventArgs e)
        {
            if (Grid.SelectedItem == null)
            {
                MessageBox.Show("Please select an appointment from result list!");
            }
            else
            {
                ///delete the appoint from appointList
                Appointment appDel = Grid.SelectedItem as Appointment;
                if(appDel != null)
                {
                    for (int i = 0; i < appointList.Count(); i++)
                    {
                        if (appointList[i].AppTime == appDel.AppTime)
                        {
                            appointList.Remove(i);
                            AppointBox.Items.Add(appointList[i]);
                            break;
                        }
                    }
                    //save data to .xml by serialization
                    XmlSerializer serializer = new XmlSerializer(typeof(AppointList));
                    TextWriter tw = new StreamWriter(xmlfile);
                    serializer.Serialize(tw, appointList);
                    tw.Close();
                    Collection.Clear();
                  //  Display();
                    MessageBox.Show("Deleted successfully!");
                }
                
            }
        }

        ///Search Combo Box 
        private void ComboBoxFields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AppointList newFilterList = new AppointList();
            switch (comboBoxFields.SelectedIndex) {
                case 0: {
                        var customer = from appointment in appointList
                                    orderby appointment.CustData.CustType descending
                                    select appointment;

                        foreach (Appointment appt in customer)
                        {
                            newFilterList.Add(appt);
                        }
                        Grid.ItemsSource = newFilterList;
                        break;
                    }
                case 1: {
                        var Name = from appointment in appointList
                                         orderby appointment.CustData.CustName descending
                                         select appointment;
                        foreach (Appointment appt in Name)
                        {
                            newFilterList.Add(appt);
                        }
                        Grid.ItemsSource = newFilterList;
                        break;
                    }
            }
        }

        private void txtCustomerName_GotFocus(object sender, RoutedEventArgs e)
        {
            CustomerNameText.Foreground = Brushes.Black;
        }

        private void txtContactNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            CustomerNumberText.Foreground = Brushes.Black;
        }

        private void txtAge_GotFocus(object sender, RoutedEventArgs e)
        {
            txtAge.Foreground = Brushes.Black;
        }

        private void AgeValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                txtAge.ToolTip = (string)e.Error.ErrorContent.ToString();
            }
            if (e.Action == ValidationErrorEventAction.Removed)
            {
                txtAge.ToolTip = null;
            }
        }

        private void NumberValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                CustomerNumberText.ToolTip = (string)e.Error.ErrorContent.ToString();
            }
            if (e.Action == ValidationErrorEventAction.Removed)
            {
                CustomerNumberText.ToolTip = null;
            }
        }

        private void NameValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                CustomerNameText.ToolTip = (string)e.Error.ErrorContent.ToString();
            }
            if (e.Action == ValidationErrorEventAction.Removed)
            {
                CustomerNameText.ToolTip = null;
            }
        }

        private void TimeValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                AppointBox.ToolTip = (string)e.Error.ErrorContent.ToString();
            }
            if (e.Action == ValidationErrorEventAction.Removed)
            {
                AppointBox.ToolTip = null;
            }
        }

    }
}
