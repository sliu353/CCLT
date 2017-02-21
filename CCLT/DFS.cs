using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLT
{
    class DFS
    {
        private Entry[] chemicals;
        private int targetUnits;
        private double targetMV;

        public DFS(List<Entry> chemicals, int targetUnits, double targetMV){
            this.chemicals = chemicals.OrderBy(x => x.MV).ToArray();
            this.targetUnits = targetUnits;
            this.targetMV = targetMV;
        }
        
        public List<int[]> Calculate()
        {
            List<int[]> result = new List<int[]>();
            Stack<stackItem> memoStack = new Stack<stackItem>();
            int[] counters = new int[chemicals.Length];

            int chemicalIndex = 0;
            while (chemicals[chemicalIndex].MV <= targetMV)
            {
                memoStack.Push(new stackItem(chemicalIndex, -1));
                counters[chemicalIndex]++;
                while (memoStack.Count > 0)
                {
                    while(memoStack.Count != targetUnits)
                    {
                        stackItem thisItem = memoStack.Peek();
                        memoStack.Push(new stackItem(thisItem.VisitedChildNum + 1, -1));
                        thisItem.VisitedChildNum++;
                        counters[thisItem.VisitedChildNum]++;
                    }
                    result.Add(counters);
                    counters.CopyTo(counters, 0);
                    stackItem lastItem = memoStack.Pop();
                    counters[lastItem.ItemNum]--;
                    while (memoStack.Peek().VisitedChildNum == counters.Length - 1)
                    {
                        stackItem popedItem = memoStack.Pop();
                        counters[popedItem.ItemNum]--;
                    }
                }
            }
            return result;
        }
    }

    struct stackItem
    {
        public int ItemNum { get; set; }
        public int VisitedChildNum { get; set; }

        public stackItem(int itemNum, int visitedChildNum)
        {
            this.ItemNum = itemNum;
            this.VisitedChildNum = visitedChildNum;
        }
    }
}
