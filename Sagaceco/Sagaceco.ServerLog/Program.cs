﻿using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                IEnumerable<LogRecord> records = csvReader.GetRecords<LogRecord>(); 

                WeeklyLogModel weeklyModel = new WeeklyLogModel(0.1, 3.5);

                foreach(LogRecord record in records)
                {
                    if(record.Period > 32 * 2016 && weeklyModel.IsOutlier(record))
                        Console.WriteLine("Outlier: {0} - {1} ({2})", record.Period, record.Value, weeklyModel.ExpectedValue(record));
                    weeklyModel.Update(record);
                }
            }
        }
    }
}
