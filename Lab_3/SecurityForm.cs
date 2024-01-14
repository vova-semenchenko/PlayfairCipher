using System;
using System.Windows.Forms;
using System.Management;
using Microsoft.VisualBasic.Devices;
using System.Resources;
using System.Threading;

namespace Lab_3
{
    public partial class SecurityForm : Form
    {

        private readonly string correctCharacteristics =
            "Sorry, but your system requirements do not match the requirements to run the application:\n" +
            "Motherboard manufacturer name is need to be - Timi\n" +
            "Size of RAM memory is need to be - 16GB\n" +
            "CPU manufacturer name is need to be - GenuineIntel";

        public SecurityForm()
        {
            InitializeComponent();
            //Environment.SetEnvironmentVariable("INSTALATION_COUNT", null, EnvironmentVariableTarget.Machine);
        }

        private void button1_Click(object sender, EventArgs e)
        {                            
            this.Hide();
            Form1 mainForm = new Form1();
            mainForm.ShowDialog();
            this.Close();
        }

        private string GetMotherBoardInfo()
        {
            string result = string.Empty;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Manufacturer FROM Win32_BaseBoard");
                ManagementObjectCollection results = searcher.Get();

                foreach (ManagementObject obj in results)
                { 
                    result = (string)obj["Manufacturer"];
                }                          
            }
            catch (ManagementException ex)
            {
                MessageBox.Show("Error retrieving motherboard information: {0}", ex.Message);
            }

            return result;
        }

        private ulong GetRAMInfo()
        {
            ulong result = 0;

            try
            {
                long totalRAM = (long)new ComputerInfo().TotalPhysicalMemory;
                double totalMb = totalRAM / (1024.0 * 1024.0 * 1024.0);
                result = Convert.ToUInt64(totalMb);
            }
            catch (ManagementException ex)
            {
                MessageBox.Show("Error retrieving RAM information: {0}", ex.Message);
            }

            return result;
        }

        private string GetCPUInfo()
        {
            string result = string.Empty;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                ManagementObjectCollection results = searcher.Get();


                foreach (ManagementObject obj in results)
                {
                    result = (string)obj["Manufacturer"];
                }
            }
            catch (ManagementException ex)
            {
                MessageBox.Show("Error retrieving CPU information: {0}", ex.Message);
            }

            return result;
        }

        private bool IsInstallationLimitExceeded()
        {
            bool result = false;
            
            string value  = Environment.GetEnvironmentVariable("INSTALATION_COUNT", EnvironmentVariableTarget.Machine) ?? "1";

            int installatioCount = Convert.ToInt32(value);
            installatioCount++;
            Environment.SetEnvironmentVariable("INSTALATION_COUNT", installatioCount.ToString(), EnvironmentVariableTarget.Machine);

            if (installatioCount > 4)
            {
                result = true;                
            }

            return result;
        }

        private void SecurityForm_Load(object sender, EventArgs e)
        {
            if (IsInstallationLimitExceeded())
            {
                MessageBox.Show("You cannot install app. You have more then 4 installation!");
                this.Close();
            }

            if (!((GetMotherBoardInfo() == "Timi") &&
                (GetCPUInfo() == "GenuineIntel") &&
                (GetRAMInfo() == 16)))
            {
                MessageBox.Show(correctCharacteristics);
                this.Close();
            }                       
        }
    }
}
