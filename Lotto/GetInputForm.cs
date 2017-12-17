using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lotto
{
    public partial class GetInputForm : Form
    {
        public Byte[] bytes { get; set; } = new Byte[6];
        public GetInputForm()
        {
            InitializeComponent();
        }

        private void num1_ValueChanged(object sender, EventArgs e)
        {
            bytes[0] = (Byte)num1.Value;
            num2.Minimum = num1.Value + 1;
        }

        private void num2_ValueChanged(object sender, EventArgs e)
        {
            bytes[1] = (Byte)num2.Value;

            num3.Minimum = num2.Value + 1;
        }

        private void num3_ValueChanged(object sender, EventArgs e)
        {
            bytes[2] = (Byte)num3.Value;

            num4.Minimum = num3.Value + 1;

        }

        private void num4_ValueChanged(object sender, EventArgs e)
        {
            bytes[3] = (Byte)num4.Value;

            num5.Minimum = num4.Value + 1;

        }

        private void num5_ValueChanged(object sender, EventArgs e)
        {
            bytes[4] = (Byte)num5.Value;

            num6.Minimum = num5.Value + 1;

        }

        private void num6_ValueChanged(object sender, EventArgs e)
        {
            bytes[5] = (Byte)num6.Value;

        }

        private void OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

        }
    }
}
