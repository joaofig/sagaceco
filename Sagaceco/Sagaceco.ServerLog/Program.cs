using System;
using System.IO;

namespace Sagaceco.ServerLog
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader streamReader = File.OpenText("server-log-bad.csv"))
            {
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
             }
        }
    }
}
