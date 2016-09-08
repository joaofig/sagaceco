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
                //CsvConfiguration config = new CsvConfiguration()
                //{
                //    CultureInfo = CultureInfo.InvariantCulture,
                //    Delimiter = ",",
                //    IsHeaderCaseSensitive = false,
                //    IgnoreQuotes = true
                //};

                WeeklyLogModel weeklyModel = new WeeklyLogModel(3.2);

                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    
                    if(char.IsNumber(line, 0))
                    {
                        string[] pair = line.Split(new char[] { ','} );
                        LogRecord record = new LogRecord() { Period = Int32.Parse(pair[0]), Value = Double.Parse(pair[1]) };

                        if (record.Period > 32 * 2016 && weeklyModel.IsOutlier(record))
                            Console.WriteLine("Outlier: {0} - {1} ({2})", record.Period, record.Value, weeklyModel.GetValue(record) );
                        weeklyModel.Update(record);
                    }
                }

                //CsvReader csvReader = new CsvReader(streamReader, config);

                //LogRecord[] records = csvReader.GetRecords<LogRecord>().ToArray(); 


                ////int count = records.Count();

                //foreach(LogRecord record in records)
                //{
                //    if(record.Period > 32 * 2016 && weeklyModel.IsOutlier(record))
                //        Console.WriteLine("Outlier: {0} - {1} ({2})", record.Period, record.Value, weeklyModel.GetValue(record) );
                //    weeklyModel.Update(record);
                //}
             }
        }
    }
}
