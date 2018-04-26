using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickOnceRecovery
{
    public delegate void CallBackDelegate();
    public delegate void StatusDelegate(string message);
    public partial class Form1 : Form
    {

        private string process;
        public Form1(string arg)
        {

            process = arg.Trim().ToLower();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ApplicationDataClass dataObject = getDataObject();
            if (process.Equals("backup"))
            {
                Backup.Run(dataObject,new CallBackDelegate(CallBack), new StatusDelegate(Status));
            }else
            {
                Recover.Run(dataObject,new CallBackDelegate(CallBack), new StatusDelegate(Status));
            }

            
        }

        private ApplicationDataClass getDataObject()
        {
            string path = Path.Combine(Application.StartupPath, "applicationdata.tkn");

            string xmlContent = File.ReadAllText(path);
            ApplicationDataClass dataObject = new ApplicationDataClass();
            dataObject = (ApplicationDataClass) XmlFunctions.ObjectFromXMLString(dataObject.GetType(), xmlContent);

            return dataObject;
        }

        public void CallBack()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {

                CallBackDelegate d = new CallBackDelegate(CallBack);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.Close();
            }
        }
        public void Status(string message)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {

                StatusDelegate d = new StatusDelegate(Status);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.AppendText(message);
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.ScrollToCaret();
                
                
            }
        }
    }
}
