using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.TimeSeries.Patterns.Models
{
    public interface ISeriesModel
    {
        void Update(double x, double y);

        bool IsOutlier(double radius, double x, double y);

        double GetValue(double x);
    }
}
