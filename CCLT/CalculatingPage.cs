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
        private List<MyRadialSlider> selectedItems = new List<MyRadialSlider>();

        // The sum of the number of the selected sliders that the user want to slide.
        private int selectedAmount = 0;
        private Entry[] chemicals;
        private int targetUnits;
        private double targetMVUpperBound;
        private double targetMVLowerBound;
        private FlowLayoutPanel lightChemicalsPanel = new FlowLayoutPanel();
        private FlowLayoutPanel standardChemicalsPanel = new FlowLayoutPanel();
        private FlowLayoutPanel condensedChemicalsPanel = new FlowLayoutPanel();
        private List<MyCheckBox> checkBoxes = new List<MyCheckBox>();

        private double getMV()
        {
            double result = 0;
            int units = 0;
            checkBoxes.ForEach(c => {
                result += c.Slider.Value * c.Slider.Chemical.MV;
                units += (int) c.Slider.Value;
            });
            result /= units;
            return result;
        }

        public CalculatingPage(List<Entry> chemicals, int targetUnits, double targetMVUpperBound, double targetMVLowerBound)
        {
            this.chemicals = chemicals.OrderBy(x => x.MV).ToArray();
            this.targetUnits = targetUnits;
            this.targetMVLowerBound = targetMVLowerBound;
            this.targetMVUpperBound = targetMVUpperBound;
            InitializeComponent();

            lightChemicalsPanel = new FlowLayoutPanel() { AutoSize = true, MinimumSize = new Size(flowLayoutPanel.Width, 0)};
            standardChemicalsPanel = new FlowLayoutPanel() { AutoSize = true, MinimumSize = new Size(flowLayoutPanel.Width, 0) };
            condensedChemicalsPanel = new FlowLayoutPanel() { AutoSize = true, MinimumSize = new Size(flowLayoutPanel.Width, 0) };
            flowLayoutPanel.Controls.AddRange(new Control[] { lightChemicalsPanel, standardChemicalsPanel, condensedChemicalsPanel });

            foreach (var chemical in chemicals)
            {
                FlowLayoutPanel thisPanel = new FlowLayoutPanel() {
                    Width = 110,
                    Height = 170
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
                    Value = chemical.SelectedUnits,
                    Enabled = false,
                    Cursor = Cursors.Hand
                };

                slider.ValueChanged += (o, e) => {
                    MyRadialSlider theOtherSlider = selectedItems.Where(x => x != slider).FirstOrDefault();
                    slider.Value = Math.Ceiling(slider.Value);
                    int tempValue = (int)(selectedAmount - slider.Value);
                    if (tempValue > theOtherSlider.MaximumValue) theOtherSlider.Value = theOtherSlider.MaximumValue;
                    else if (tempValue < 0) theOtherSlider.Value = 0;
                    else theOtherSlider.Value = tempValue;
                   
                    if (theOtherSlider.Value == 0) slider.Value = selectedAmount;
                    if (theOtherSlider.Value == theOtherSlider.MaximumValue) slider.Value = selectedAmount - (int)theOtherSlider.Value;

                    MVIndicator.Text = getMV().ToString();
                };

                MyCheckBox thisCheckBox = new MyCheckBox() { Slider = slider };
                checkBoxes.Add(thisCheckBox);
                thisCheckBox.Click += (o, e) =>
                {
                    if (thisCheckBox.Checked)
                    {
                        selectedItems.Add(thisCheckBox.Slider);
                        if (selectedItems.Count == 2) checkBoxes.ForEach(c =>
                        {
                            if (!selectedItems.Contains(c.Slider))
                                c.Enabled = false;
                            selectedAmount = 0;
                            selectedItems.ForEach(s => {
                                s.Enabled = true;
                                selectedAmount += (int) s.Value;
                            });
                        });
                    }
                    else
                    {
                        selectedItems.Remove(thisCheckBox.Slider);
                        thisCheckBox.Slider.Enabled = false;
                        if (selectedItems.Count == 1) checkBoxes.ForEach(c =>
                        {
                            if (!selectedItems.Contains(c.Slider))
                                c.Enabled = true;
                            selectedItems.ForEach(s => s.Enabled = false);
                        });
                    }
                };

                Label thisLabel = new Label() { Text = slider.Name, Width = 90, TextAlign = ContentAlignment.MiddleCenter };
                thisPanel.Controls.Add(thisCheckBox);
                thisPanel.Controls.Add(slider);
                thisPanel.Controls.Add(thisLabel);

                //if (chemical.MV > targetMVUpperBound) condensedChemicalsPanel.Controls.Add(thisPanel);
                //else if (chemical.MV < targetMVLowerBound) lightChemicalsPanel.Controls.Add(thisPanel);
                standardChemicalsPanel.Controls.Add(thisPanel);

                MVIndicator.Text = getMV().ToString();
            }
        }
    }

    class MyRadialSlider : RadialSlider
    {
        public Entry Chemical { get; set; }
    }

    class MyCheckBox : CheckBox
    {
        public MyRadialSlider Slider { get; set; }
    }
}
