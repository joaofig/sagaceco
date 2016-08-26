using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Sagaceco.ServerLog
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader streamReader = File.OpenText("server-log.csv"))
            {
                CsvConfiguration config = new CsvConfiguration()
                {
                    CultureInfo = CultureInfo.InvariantCulture,
                    Delimiter = ",",
                    IsHeaderCaseSensitive = false,
                    IgnoreQuotes = true
                };

                CsvReader csvReader = new CsvReader(streamReader, config);

                LogRecord[] records = csvReader.GetRecords<LogRecord>().ToArray(); 

                WeeklyLogModel weeklyModel = new WeeklyLogModel(0.1, 3.5);

                //int count = records.Count();

                foreach(LogRecord record in records)
                {
                    if(record.Period > 32 * 2016 && weeklyModel.IsOutlier(record))
                        Console.WriteLine("Outlier: {0} - {1} ({2}, {3})", record.Period, record.Value, weeklyModel.GetAverage(record), Math.Sqrt(weeklyModel.GetVariance(record)) );
                    weeklyModel.Update(record);
                }
            }
        }
    }
}
