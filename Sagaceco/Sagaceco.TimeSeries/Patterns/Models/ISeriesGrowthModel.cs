using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.TimeSeries.Patterns.Models
{
    interface ISeriesGrowthModel
    {
        // Nothing to see here. Move along!

        double Update(double x);
    }
}
