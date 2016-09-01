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
            using (StreamReader streamReader = File.OpenText("server-log-bad.csv"))
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

                WeeklyLogModel weeklyModel = new WeeklyLogModel(3.2);

                //int count = records.Count();

                foreach(LogRecord record in records)
                {
                    if(record.Period > 32 * 2016 && weeklyModel.IsOutlier(record))
                        Console.WriteLine("Outlier: {0} - {1} ({2})", record.Period, record.Value, weeklyModel.GetValue(record) );
                    weeklyModel.Update(record);
                }
             }
        }
    }
}
