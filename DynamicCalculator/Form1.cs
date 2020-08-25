using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicCalculator
{
    public partial class DynamicznyKalkulator : Form
    {
        public DynamicznyKalkulator()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string equation = textBox1.Text;
            DynaCode dynaCode = new DynaCode(equation);
            string result = "";            
            result = dynaCode.Execute().ToString();            
            textBox2.Text = result.ToString();            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = listBox1.SelectedItem;
            string s = selected.ToString();
            textBox1.Text = s;
        }
    }
}
