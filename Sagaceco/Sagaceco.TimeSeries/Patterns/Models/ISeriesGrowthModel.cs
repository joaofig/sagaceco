using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.TimeSeries.Patterns.Models
{
    interface ISeriesGrowthModel
    {
        double Update(double x);
    }
}
