using Accord.Statistics.Distributions.Univariate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.TimeSeries.Patterns.Models
{
    /// <summary>
    /// Implements a simple linear regression model: ŷ = α + β x
    /// </summary>
    public class LinearRegressionModel : ISeriesModel
    {
        private int     count   = 0;
        private double  sumX    = 0.0;
        private double  sumY    = 0.0;
        private double  sumXY   = 0.0;
        private double  sumXX   = 0.0;
        private double  sumYY   = 0.0;
        private double  alpha   = 0.0;
        private double  beta    = 0.0;

        public void Update(double x, double y)
        {
            count++;
            sumX    += x;
            sumY    += y;
            sumXY   += x * y;
            sumXX   += x * x;
            sumYY   += y * y;

            double meanX    = sumX / count;
            double meanY    = sumY / count;
            double meanXY   = sumXY / count;
            double meanXX   = sumXX / count;

            beta = (meanXY - meanX * meanY) / (meanXX - meanX * meanX);
            alpha = meanY - beta * meanX;
        }

        /// <summary>
        /// Gets the estimated value for α
        /// </summary>
        public double Alpha
        {
            get { return alpha; }
        }

        /// <summary>
        /// Gets the estimated value for β
        /// </summary>
        public double Beta
        {
            get { return beta; }
        }

        public bool IsOutlier(double radius, double x, double y)
        {
            double yHat = GetValue( x );
            double studentizedResidue = Math.Abs(y - yHat) / Math.Sqrt( GetResidualVariance() );

            return studentizedResidue > radius;
        }

        public double GetValue(double x)
        {
            return alpha + beta * x;
        }

        //

        private double GetResidualVariance()
        {
            return GetRSS() / (count - 2);
        }

        private double GetBetaVariance()
        {
            return count * GetResidualVariance() / (count * sumXX - sumX * sumX);
        }

        private double GetAlphaVariance()
        {
            return GetBetaVariance() * sumXX / count;
        }

        private double GetYHatConfidenceRange(double x, double confidence = 0.95)
        {
            TDistribution t = new TDistribution(count - 2);
            double tValue = t.InverseDistributionFunction( confidence + (1.0 - confidence) / 2 );
            double meanX = sumX / count;
            double rss = GetRSS();

            return tValue * Math.Sqrt( rss / (count - 2) ) * Math.Sqrt( 1.0 / (double)count + (x - meanX) * (x - meanX) / ((count - 1) * (sumXX / count - sumX * sumX / (count * count))));
        }

        private double GetYHatPredictionRange(double x, double confidence = 0.95)
        {
            TDistribution t = new TDistribution(count - 2);
            double tValue = t.InverseDistributionFunction( confidence + (1.0 - confidence) / 2 );
            double meanX = sumX / count;
            double rss = GetRSS();

            return tValue * Math.Sqrt( rss / (count - 2) ) * Math.Sqrt( 1.0 + 1.0 / (double)count + (x - meanX) * (x - meanX) / ((count - 1) * (sumXX / count - sumX * sumX / (count * count))));
        }

        private double GetRSS()
        {
            return  beta * beta * sumXX
                    -2 * beta * sumXY
                    +2 * alpha * beta * sumX
                    +sumYY
                    -2 * alpha * sumY
                    +alpha * alpha * count;
                    
        }
    }
}
