using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCLT
{
    public partial class StartingPage : Form
    {
        List<Entry> chemicals = new List<Entry>();
        double lowerBound;
        double upperBound;
        int targetAmount;

        public StartingPage()
        {
            InitializeComponent();
            BindSource();
        }

        public void BindSource()
        {
            var bindingList = new BindingList<Entry>(chemicals);
            var source = new BindingSource(null, null);
            Entries.DataSource = source;
            source = new BindingSource(bindingList, null);
            Entries.DataSource = source;
            Entries.Columns[0].HeaderText = "配料名";
            Entries.Columns[1].HeaderText = "MV";
            Entries.Columns[2].HeaderText = "数量";
            Entries.Columns[3].Visible = false;
        }

        private void Entries_SelectionChanged(object sender, EventArgs e)
        {
            if (Entries.CurrentRow != null && Entries.SelectedRows.Count > 0)
                DeleteButton.Enabled = true;
            else
                DeleteButton.Enabled = false;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (Entries.CurrentRow != null)
                Entries.Rows.Remove(Entries.CurrentRow);
        }

        private void Entries_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Entries.CurrentCell.ColumnIndex == 0 && chemicals.Where(entry => entry.Name == (string)Entries.CurrentCell.Value).Count() > 1)
            {
                MessageBox.Show(
                    "你已经输入了" + Entries.CurrentCell.Value + "的信息", 
                    "注意",
                    MessageBoxButtons.OKCancel, 
                    MessageBoxIcon.Error);
                Entries.CurrentCell.Value = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Add a new row and rebind the data source.
            chemicals.Add(new Entry());
            BindSource();
        }

        private void Entries_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            double value;
            if (e.ColumnIndex != 0 && !double.TryParse((string)e.FormattedValue, out value))
            {
                MessageBox.Show(
                    "请输入数字",
                    "注意",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if(upperBound == 0)
            {
                MessageBox.Show(
                   "您未输入目标量的上限",
                   "注意",
                   MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Error);
                return;
            }
            if (lowerBound == 0)
            {
                MessageBox.Show(
                   "您未输入目标量的下限",
                   "注意",
                   MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Error);
                return;
            }
            if (targetAmount == 0)
            {
                    MessageBox.Show(
                       "您未输入需要的MV",
                       "注意",
                       MessageBoxButtons.OKCancel,
                       MessageBoxIcon.Error);
                    return;

            }
            if(chemicals.Any(c => c.Units == 0 || c.MV == 0 || c.Name == "") || chemicals.Count < 2)
            {
                    MessageBox.Show(
                       "您未完成所有原料信息的输入",
                       "注意",
                       MessageBoxButtons.OKCancel,
                       MessageBoxIcon.Error);
                    return;
            }

            DFS dfsAlgorithm = new DFS(chemicals, targetAmount, upperBound, lowerBound);
            dfsAlgorithm.Calculate();
            CalculatingPage calculatingPage = new CalculatingPage(chemicals, targetAmount, upperBound, lowerBound);
            calculatingPage.Left = Left;
            calculatingPage.Top = Top;
            Hide();
            calculatingPage.Show();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UpperBound_Validating(object sender, CancelEventArgs e)
        {
            double value;
            if (!double.TryParse((string) UpperBound.Text, out value))
            {
                MessageBox.Show(
                    "请输入数字",
                    "注意",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
                e.Cancel = true;
            }
            upperBound = value;
        }

        private void LowerBound_Validating(object sender, CancelEventArgs e)
        {
            double value;
            if (!double.TryParse((string)LowerBound.Text, out value))
            {
                MessageBox.Show(
                    "请输入数字",
                    "注意",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
                e.Cancel = true;
            }
            lowerBound = value;
        }

        private void ExpectedMV_Validating(object sender, CancelEventArgs e)
        {
            int value;
            if (!int.TryParse((string)ExpectedMV.Text, out value))
            {
                MessageBox.Show(
                    "请输入数字",
                    "注意",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
                e.Cancel = true;
            }
            targetAmount = value;
        }
    }
}
