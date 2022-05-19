using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swordfish.ECS;

namespace Swordfish.Core
{
    internal static class Utils
    {
        public static IComponent[] QuickSortComponentsArray(IComponent[] array, int min, int max)
        {
            var i = min;
            var j = max;
            var pivot = array[max].Priority;

            while (i <= j)
            {
                while (array[i].Priority < pivot)
                {
                    i++;
                }

                while (array[j].Priority > pivot)
                {
                    j--;
                }

                if (i <= j)
                {
                    IComponent temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;

                }
            }

            if (min < j)
            {
                QuickSortComponentsArray(array, min, j);
            }
            if (i < max)
            {
                QuickSortComponentsArray(array, i, max);
            }

            return array;

        }
    }
}
