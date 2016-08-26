using System;

namespace Sagaceco.TimeSeries.Patterns.Models
{
    public class ExponentialMovingModel
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

        public double Average
        {
            get { return average; }
        }

        public double Variance
        {
            get { return variance; }
        }

        public void Update(double x)
        {
            if( average == 0.0 && variance == 0.0 )
            {
                average = x;
            }
            else
            {
                double  diff    = x - average;
                double  incr    = Weight * diff;

                average     = average + incr;
                variance    = (1 - Weight) * (variance + diff * incr);
            }
        }

        public bool IsOutlier( double radius, double x )
        {
            return Math.Abs( x - average ) > radius * Math.Sqrt( variance );
        }
    }
}
