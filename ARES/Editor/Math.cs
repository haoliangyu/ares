using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARES.Editor
{
    /// <summary>
    /// Provide access to static functions for raster value statistic.
    /// </summary>
    static class Math
    {
        #region Methods

        /// <summary>
        /// Calculate the statistic of given values.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Dictionary<string, double> CalStat(double[] values, double noDataValue)
        {
            /* The default statistic includes:
             * 1. Count
             * 2. Min
             * 3. Max
             * 4. Range
             * 5. Median
             * 6. Mean
             * 7. Variance
             * 8. Std
             */

            Dictionary<string, double> stat = new Dictionary<string, double>();

            #region Count

            List<double> valueList = new List<double>();
            foreach (double value in values)
            {
                if (value != noDataValue)
                {
                    valueList.Add(value);    
                }
            }
            stat.Add("Count", valueList.Count);

            if (valueList.Count == 0)
            {
                return stat;
            }

            #endregion

            #region Min, Max, Range, Median
            
            valueList.Sort();
            stat.Add("Min", valueList[0]);
            stat.Add("Max", valueList[valueList.Count - 1]);
            stat.Add("Range", stat["Max"] - stat["Min"]);

            if (valueList.Count % 2 == 0)
            {
                int pos = Convert.ToInt32(valueList.Count / 2);
                stat.Add("Median", (valueList[pos] + valueList[pos - 1]) / 2);
            }
            else 
            {
                stat.Add("Median", valueList[Convert.ToInt32((valueList.Count - 1) / 2)]);
            }

            #endregion

            #region Mean

            double sum = 0;
            foreach (double value in valueList)
            {
                if (value != noDataValue)
                    sum += value;
            }
            stat.Add("Mean", sum / valueList.Count);

            #endregion

            #region Variance, Std

            double pSum = 0;
            foreach (double value in valueList)
            {
                pSum += System.Math.Pow(value - stat["Mean"], 2);
            }
            stat.Add("Variance", pSum / stat["Count"]);
            stat.Add("Std", System.Math.Sqrt(stat["Variance"]));

            #endregion

            return stat;
        }

        #endregion
    }
}
