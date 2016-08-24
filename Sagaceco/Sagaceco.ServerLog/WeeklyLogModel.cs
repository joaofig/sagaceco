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
        private ExponentialMovingModel[] models = new ExponentialMovingModel[2016];
        private double radius = 3.0;

        public WeeklyLogModel()
        {
            for(int i = 0; i < models.Length; i++)
                models[i] = new ExponentialMovingModel();
        }

        public WeeklyLogModel(double weight, double radius)
        {
            this.radius = radius;

            for(int i = 0; i < models.Length; i++)
                models[i] = new ExponentialMovingModel(weight);
        }

        public void Update(LogRecord record)
        {
            int index = record.Period % models.Length;

            models[index].Update(record.Value);
        }

        public bool IsOutlier(LogRecord record)
        {
            int index = record.Period % models.Length;

            return models[index].IsOutlier(radius, record.Value);
        }

        public double ExpectedValue(LogRecord record)
        {
            int index = record.Period % models.Length;

            return models[index].Average;
        }
    }
}
