using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLT
{
    class DFS
    {
        private List<Entry> condensedChemicals = new List<Entry>();
        private List<Entry> lightChemicals = new List<Entry>();
        private List<Entry> standardChemicals = new List<Entry>();
        private int targetUnits;
        private double targetMVUpperBound;
        private double targetMVLowerBound;

        public DFS(List<Entry> chemicals, int targetUnits, double targetMVUpperBound, double targetMVLowerBound)
        {
            chemicals.ForEach(c =>
            {
                if (c.MV > targetMVUpperBound) condensedChemicals.Add(c);
                else if (c.MV < targetMVLowerBound) lightChemicals.Add(c);
                else standardChemicals.Add(c);
            });

            condensedChemicals = condensedChemicals.OrderBy(c => c.MV).ToList();
            lightChemicals = lightChemicals.OrderByDescending(c => c.MV).ToList();
            this.targetUnits = targetUnits; this.targetMVLowerBound = targetMVLowerBound;
            this.targetMVUpperBound = targetMVUpperBound;
        }

        public void Calculate()
        {
            int addUnits = 0;
            double totalAmount = 0;

            // Select all units of chemicals with MV between upper bound and lower bound.
            standardChemicals.ForEach(c =>
            {
                if (c.Units < targetUnits)
                {
                    c.SelectedUnits = c.Units;
                    targetUnits -= c.Units;
                    addUnits += c.Units;
                    totalAmount += c.Units * c.MV;
                }
                else
                {
                    c.SelectedUnits = targetUnits;
                    targetUnits = 0;
                    addUnits += targetUnits;
                    totalAmount += c.Units * c.MV;
                }
            });

            int condensedIndex = 0;
            int lightIndex = 0;

            for (int i = 0; i < targetUnits; i++)
            {
                double averageMV = totalAmount / addUnits;
                if (averageMV < targetMVLowerBound && condensedIndex < condensedChemicals.Count)
                {
                    totalAmount += condensedChemicals[condensedIndex].MV;
                    condensedChemicals[condensedIndex].SelectedUnits++;
                    if (condensedChemicals[condensedIndex].SelectedUnits == condensedChemicals[condensedIndex].Units)
                    {
                        condensedIndex++;
                    }
                }
                else if (averageMV > targetMVUpperBound && lightIndex < lightChemicals.Count)
                {
                    totalAmount += lightChemicals[lightIndex].MV;
                    lightChemicals[lightIndex].SelectedUnits++;
                    if (lightChemicals[lightIndex].SelectedUnits == lightChemicals[lightIndex].Units)
                    {
                        lightIndex++;
                    }
                }
                else
                {
                    if (lightIndex < lightChemicals.Count)
                    {
                        totalAmount += lightChemicals[lightIndex].MV;
                        lightChemicals[lightIndex].SelectedUnits++;
                        if (lightChemicals[lightIndex].SelectedUnits == lightChemicals[lightIndex].Units)
                        {
                            lightIndex++;
                        }
                    }
                    else
                    {
                        totalAmount += condensedChemicals[condensedIndex].MV;
                        condensedChemicals[condensedIndex].SelectedUnits++;
                        if (condensedChemicals[condensedIndex].SelectedUnits == condensedChemicals[condensedIndex].Units)
                        {
                            condensedIndex++;
                        }
                    }
                }
            }
        }
    }
}
