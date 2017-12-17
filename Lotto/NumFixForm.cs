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
    public partial class NumFixForm : Form
    {
        public bool[] Fixed = new bool[45];
        public bool[] Excluded = new bool[45];
        public NumFixForm(bool[] fix, bool[] exclude)
        {
            InitializeComponent();

            fix.CopyTo(Fixed, 0);
            exclude.CopyTo(Excluded, 0);
            for (int i = 1; i <= 45; i++)
            {
                Fix.Items.Add(i.ToString().PadRight(5), Fixed[i-1]);
                Exclude.Items.Add(i.ToString().PadRight(5), Excluded[i - 1]);
            }
        }

        private void Button_Ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        
        private void Fix_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int fixedCount = Fixed.Sum(x => x ? 1 : 0);

            if (e.NewValue == CheckState.Unchecked)
            {
                Fixed[e.Index] = false;
                return;
            }

            if (fixedCount == 6)
            {
                e.NewValue = e.CurrentValue;
            }
            else
            {
                if (Excluded[e.Index])
                {
                    Exclude.SetItemCheckState(e.Index, CheckState.Unchecked);
                }
                Fixed[e.Index] = true;
            }
        }

        private void Exclude_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int excludedCount = Excluded.Sum(x => x ? 1 : 0);
            if (e.NewValue == CheckState.Unchecked)
            {
                Excluded[e.Index] = false;
                return;
            }

            if (excludedCount == 39)    // max
            {
                e.NewValue = e.CurrentValue;
            }
            else
            {
                if (Fixed[e.Index])
                {
                    Fix.SetItemCheckState(e.Index, CheckState.Unchecked);
                }
                Excluded[e.Index] = true;

            }
        }
    }
}
