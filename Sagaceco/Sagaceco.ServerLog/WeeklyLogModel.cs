using Sagaceco.TimeSeries.Patterns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.ServerLog
{
    public class WeeklyLogModel
    {
        private ISeriesModel[] models = new ISeriesModel[2016];
        private double radius = 1.6;

        public WeeklyLogModel()
        {
            for(int i = 0; i < models.Length; i++)
                models[i] = ModelBuilder();
        }

        public WeeklyLogModel(double radius) : this()
        {
            this.radius = radius;
        }

        public void Update(LogRecord record)
        {
            int index = record.Period % models.Length;

            models[index].Update(record.Period, record.Value);
        }

        public bool IsOutlier(LogRecord record)
        {
            int index = record.Period % models.Length;

            return models[index].IsOutlier(radius, record.Period, record.Value);
        }

        public double GetValue(LogRecord record)
        {
            int index = record.Period % models.Length;

            return models[index].GetValue(record.Period);
        }

        //

        private ISeriesModel ModelBuilder()
        {
            return new LinearRegressionModel();
        }
    }
}
