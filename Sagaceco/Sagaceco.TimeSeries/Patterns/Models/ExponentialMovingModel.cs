using System;

namespace Sagaceco.TimeSeries.Patterns.Models
{
    public class ExponentialMovingModel : ISeriesModel
    {
        private double average = 0.0;
        private double variance = 0.0;

        public ExponentialMovingModel()
        {
            Weight = 0.1;
        }

        public ExponentialMovingModel(double weight)
        {
            Weight = weight;
        }

        public double Weight { get; set; }

        public void Update(double x, double y)
        {
            if( average == 0.0 && variance == 0.0 )
            {
                average = y;
            }
            else
            {
                double  diff    = y - average;
                double  incr    = Weight * diff;

                average     = average + incr;
                variance    = (1 - Weight) * (variance + diff * incr);
            }
        }

        public bool IsOutlier( double radius, double x, double y )
        {
            return Math.Abs( y - average ) > radius * Math.Sqrt( variance );
        }

        public double GetValue(double x)
        {
            return average;
        }
    }
}
