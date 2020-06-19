using CrmBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUI
{
    public class CashBoxView
    {
        CashDesk cashDesk;
        public Label Label { get; set; }
        public NumericUpDown NumericUpDown { get; set; }

        public CashBoxView(CashDesk cashDesk, int number, int x, int y)
        {
            
            this.cashDesk = cashDesk;
            Label = new Label();
            Label.AutoSize = true;
            this.Label.Location = new System.Drawing.Point(x, y);
            this.Label.Name = "label" + number;
            this.Label.Size = new System.Drawing.Size(35, 13);
            this.Label.TabIndex = number;
            this.Label.Text = cashDesk.ToString();

            NumericUpDown = new NumericUpDown();
            this.NumericUpDown.Location = new System.Drawing.Point(x + 70, y);
            this.NumericUpDown.Name = "numericUpDown" + number;
            this.NumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.NumericUpDown.TabIndex = number;
            NumericUpDown.Maximum = 1000000000000000;

            cashDesk.CheckClosed += CashDesk_CheckClosed;
        }

        private void CashDesk_CheckClosed(object sender, Check e)
        {
            NumericUpDown.Invoke((Action) delegate { NumericUpDown.Value += e.Summary; });
        }
    }
}
