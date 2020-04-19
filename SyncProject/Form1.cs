using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjectTransferService;

namespace SyncProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            //test callAPI = new test();
            //callAPI.CreateProjectTask();

            ProjectTransfer projectTransfer = new ProjectTransfer();
            projectTransfer.GetToBeSyncedProject();
        }
    }
}
