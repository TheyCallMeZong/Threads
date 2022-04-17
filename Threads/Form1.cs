using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Threads
{
    public partial class Form1 : Form
    {
        Collections<int> collections = new Collections<int>();
        public Form1()
        {
            InitializeComponent();

            Thread thread = new Thread(new ThreadStart(Fibonachi));
            thread.Start();
            //thrad.Abort(); //-- исключение: ThreadAbortException
        }

        private void Fibonachi()
        {
            int number = 0;
            int outNumber = 1;
            while (true)
            {
                if (this.InvokeRequired)
                {
                    int i;
                    Invoke(new Action(() =>
                    {
                        textBox1.AppendText($" {number + outNumber}");
                    }));
                    i = number;
                    number += outNumber;
                    outNumber = i;
                    Thread.Sleep(2000);

                }
            }
        }

        private void ViewCollection()
        {
            if (collections.GetList().Count > 0)
            {
                Invoke(new Action(() =>
                {
                    textBox8.Text = string.Empty;
                    var list = collections.GetList();
                    foreach (var item in list)
                    {
                        textBox8.AppendText(item + " ");
                    }
                }));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(AddItem));
            thread.Start();
        }

        private void AddItem()
        {
            string text = textBox4.Text;
            if (int.TryParse(text, out int number))
            {
                collections.AddItem(number);
            }
            Invoke(new Action(() =>
            {
                textBox4.Text = string.Empty;
            }));
            
            Thread thread = new Thread(new ThreadStart(ViewCollection));
            thread.Start();
        }

        private void DeleteItem()
        {
            string text = textBox6.Text;
            if (int.TryParse(text, out int number))
            {
                if (collections.GetList().Count >= number)
                {
                    collections.DeleteItem(number);
                }
            }
            Invoke(new Action(() =>
            {
                textBox6.Text = string.Empty;
            }));
            Thread thread = new Thread(new ThreadStart(ViewCollection));
            thread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(DeleteItem));
            thread.Start();
        }
    }
}
