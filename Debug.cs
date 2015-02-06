using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NsOL
{
    public partial class Debug : Form
    {
        public int TrackValue1 = 0;
        public int TrackValue2 = 0;
        public int TrackValue3 = 0;
        private string TrackText1 = "TrackBar1:";
        private string TrackText2 = "TrackBar2:";
        private string TrackText3 = "TrackBar3:";

        public Debug()
        {
            InitializeComponent();
        }

        public void Initilize(string TrackString1,string TrackString2,string TrackString3)
        {
            TrackText1 = TrackString1;
            TrackText2 = TrackString2;
            TrackText3 = TrackString3;
        }

        private void CheckSender_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckSender.Checked) Sender.Start();
            else Sender.Stop();
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            Label1.Text = TrackText1 + " " + TrackBar1.Value.ToString();
            TrackValue1 = TrackBar1.Value;
        }

        private void TrackBar2_Scroll(object sender, EventArgs e)
        {
            Label2.Text = TrackText2 + " " + TrackBar2.Value.ToString();
            TrackValue2 = TrackBar2.Value;
        }

        private void TrackBar3_Scroll(object sender, EventArgs e)
        {
            Label3.Text = TrackText3 + " " + TrackBar3.Value.ToString();
            TrackValue3 = TrackBar3.Value;
        }
    }
}
