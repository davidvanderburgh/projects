using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CorruptionChecksum.Logic
{
    public class CorruptionChecksumLogic
    {
        public int GetChecksum(List<int[]> worksheet)
        {
            int checksum = 0;
            foreach (int[] intArray in worksheet)
            {
                checksum += intArray.Max() - intArray.Min();
            }
            return checksum;
        }
    }
}
