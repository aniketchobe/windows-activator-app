using System;
using System.Diagnostics;  // Needed for Process
using System.Windows;      // Needed for Window elements
using System.Windows.Controls;


namespace WindowsActivatorApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            // Ensure you get the selected action from the ComboBox
            string selectedAction = ((ComboBoxItem)actionComboBox.SelectedItem)?.Content.ToString();
            string selectedVersion = ((ComboBoxItem)windowsVersionComboBox.SelectedItem)?.Content.ToString();

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
                MessageBox.Show("Please select a valid action.");
            }
        }

        private void ActivateWindows(string version)
        {
            string key = version switch
            {
                "Windows 11 Home" => "TX9XD-98N7V-6WMQ6-BX7FG-H8Q99",
                "Windows 11 Pro" => "W269N-WFGWX-YVC9B-4J6C9-T83GX",
                // Add other version mappings here
                _ => null
            };

            if (key != null)
            {
                Process.Start("slmgr", $"/ipk {key}");
                Process.Start("slmgr", "/skms kms8.msguides.com");
                Process.Start("slmgr", "/ato");
                resultTextBlock.Text = "Windows Activation started...";
            }
            else
            {
                resultTextBlock.Text = "Invalid Windows version selected.";
            }
        }

        private void CheckActivationExpiry()
        {
            Process.Start("slmgr", "/xpr");
            resultTextBlock.Text = "Checking Windows activation expiry...";
        }
    }
}
