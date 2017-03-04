using Syncfusion.Windows.Forms.Tools;
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
    public partial class CalculatingPage : Form
    {
        private Entry[] chemicals;
        private int targetUnits;
        private double targetMVUpperBound;
        private double targetMVLowerBound;

        public CalculatingPage(List<Entry> chemicals, int targetUnits, double targetMVUpperBound, double targetMVLowerBound)
        {
            this.chemicals = chemicals.OrderBy(x => x.MV).ToArray();
            this.targetUnits = targetUnits;
            this.targetMVLowerBound = targetMVLowerBound;
            this.targetMVUpperBound = targetMVUpperBound;
            InitializeComponent();



            foreach(var chemical in chemicals)
            {
                FlowLayoutPanel thisPanel = new FlowLayoutPanel() {
                    Width = 110,
                    Height = 150
                };

                MyRadialSlider slider = new MyRadialSlider()
                {
                    Chemical = chemical,
                    Size = new Size() { Height = 100, Width = 100 },
                    MaximumValue = chemical.Units,
                    MinimumValue = 0,
                    Name = chemical.Name + "(" + chemical.MV + ")",
                    SliderStyle = SliderStyles.Frame,
                    BackColor = Color.White,
                    ForeColor = Color.LightBlue,
                    SliderDivision = 1,
                    Value = chemical.SelectedUnits
                    //color
                };

                DomainUpDownExt adjustExt = new DomainUpDownExt()
                {

                };

                Label thisLabel = new Label() { Text = slider.Name, Width = 100, TextAlign = ContentAlignment.MiddleCenter };
                thisPanel.Controls.Add(slider);
                thisPanel.Controls.Add(thisLabel);
                this.flowLayoutPanel.Controls.Add(thisPanel);
            }
        }
    }

    class MyRadialSlider : RadialSlider
    {
        public Entry Chemical { get; set; }
    }
}
