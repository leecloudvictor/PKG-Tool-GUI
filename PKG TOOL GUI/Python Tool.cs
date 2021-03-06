﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Tools;
using System.Diagnostics;
using ConsoleControl;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;

namespace PKG_TOOL_GUI
{
    public partial class Python_Tool : Form
    {

        static string pip = @"get-pip.py";
        static string xlsx = @"xlsx.bat";

        static string cmd1, arg, py;

        public Python_Tool()
        {
            InitializeComponent();
        }
        
        private void CheckPython278()
        {
            
            timer1.Enabled = true;
            timer1.Start();

            //check if python 2.7.8 installed. this path works only for 86-bit version
            if (Tool.CheckInstalledSoft("hklm", @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall", "DisplayName", "Python 2.7.8") == true)
            {
                Ipy.Enabled = false;
                MessageBox.Show("Python is already installed.");

            }
            else
            {

                Ipy.Enabled = true;
                Process p = new Process();
                p.StartInfo.FileName = @"python-2.7.8.msi";
                p.Start();
                p.WaitForExit();
                



            }

        }

        private void Ipy_Click(object sender, EventArgs e)
        {
            CheckPython278();
        }

        private void btnPIP_Click(object sender, EventArgs e)
        {
            if (Form1.Isconnected == true)
            {

                if (Directory.Exists(@"E:\Python27\Lib\site-packages\pip") || Directory.Exists(@"C:\Python27\Lib\site-packages\pip"))
                {
                    MessageBox.Show("PIP already installed.");

                    btnPIP.Enabled = false;

                }
                else
                {
                    //console.WriteOutput("Checking..\n", System.Drawing.Color.Gray);

                    cmd1 = " ";
                    py = "python";
                    arg = (cmd1 + pip);

                    //Old method
                    //var process = System.Diagnostics.Process.Start(py, arg);

                    ProcessStartInfo startInfo = new ProcessStartInfo(py, arg);
                    startInfo.WindowStyle = ProcessWindowStyle.Minimized; //make program run hidden
                    Process wait = Process.Start(startInfo); //set new var for waitforexit()
                    wait.WaitForExit();

                    btnPIP.Enabled = false;
                }

                // E: is my directory >.<
                if (Directory.Exists(@"E:\Python27\Lib\site-packages\xlsxwriter") || Directory.Exists(@"C:\Python27\Lib\site-packages\xlsxwriter"))
                {
                    MessageBox.Show("XLSX Writer already installed.", "Info");

                }
                else
                {

                    //console.WriteOutput("*If xlsxwriter installations fail, click on 'xlsx.bat' to install manually.*\n", System.Drawing.Color.Gray);

                    // old method
                    //var process = System.Diagnostics.Process.Start(xlsx, null);
                    //process.WaitForExit();*/

                    ProcessStartInfo startInfo = new ProcessStartInfo(xlsx, null);
                    startInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    Process.Start(startInfo);

                    //console.StartProcess(xlsx, null);
                    MessageBox.Show("XlSX Writer installed.");


                }
            }
            else
            {
                MessageBox.Show("No internet connection detected. Please check your connection.", "Connection error");
            }
        }
    }
}
