using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Windows_Activator_App
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            // Get selected action and version
            string selectedAction = ((ComboBoxItem)actionComboBox.SelectedItem)?.Content.ToString();
            string selectedVersion = ((ComboBoxItem)windowsVersionComboBox.SelectedItem)?.Content.ToString();

            if (string.IsNullOrEmpty(selectedAction) || string.IsNullOrEmpty(selectedVersion))
            {
                MessageBox.Show("Please select both an action and a Windows version.");
                return;
            }

            // Perform actions based on selected option
            if (selectedAction == "Activate Windows")
            {
                ActivateWindows(selectedVersion);
            }
            else if (selectedAction == "Check Windows Activation Expiry")
            {
                CheckActivationExpiry();
            }
            else
            {
                MessageBox.Show("Invalid action selected.");
            }
        }

        private void ActivateWindows(string version)
        {
            string key = version switch
            {
                "Windows 11 Home" => "TX9XD-98N7V-6WMQ6-BX7FG-H8Q99",
                "Windows 11 Pro" => "W269N-WFGWX-YVC9B-4J6C9-T83GX",
                "Windows 11 Pro for Workstations" => "NRG8B-VKK3Q-CXVCJ-9G2XF-6Q84J",
                "Windows 11 Pro Education" => "6TP4R-GNPTD-KYYHQ-7B7DP-J447Y",
                "Windows 11 Education" => "NW6C2-QMPVW-D7KKK-3GKT6-VCFB2",
                "Windows 11 Enterprise" => "NPPR9-FWDCX-D2C8J-H872K-2YT43",
                "Windows 10 Home" => "TX9XD-98N7V-6WMQ6-BX7FG-H8Q99",
                "Windows 10 Pro" => "W269N-WFGWX-YVC9B-4J6C9-T83GX",
                "Windows 10 Pro for Workstations" => "NRG8B-VKK3Q-CXVCJ-9G2XF-6Q84J",
                "Windows 10 Education" => "NW6C2-QMPVW-D7KKK-3GKT6-VCFB2",
                "Windows 10 Pro Education" => "6TP4R-GNPTD-KYYHQ-7B7DP-J447Y",
                "Windows 10 Enterprise" => "NPPR9-FWDCX-D2C8J-H872K-2YT43",
                _ => null
            };

            if (key != null)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c slmgr /ipk {key} && slmgr /skms kms8.msguides.com && slmgr /ato",
                    UseShellExecute = true,  // Set this to true to use the shell and prompt for UAC elevation
                    Verb = "runas"          // This is important to prompt for administrative privileges
                });
                resultTextBlock.Text = "Windows Activation started.....WAIT FOR ALL PROCESS TO COMPLETE.....DO NOT FORCE CLOSE COMMAND PROMPT AND THIS APPLICATION.......";
            }
            else
            {
                resultTextBlock.Text = "Invalid Windows version selected.";
            }
        }

        private void CheckActivationExpiry()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c slmgr /xpr",
                UseShellExecute = true,  // Set this to true to use the shell and prompt for UAC elevation
                Verb = "runas"          // This is important to prompt for administrative privileges
            });
            resultTextBlock.Text = "Checking Windows activation expiry...";
        }
    }
}
