using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationInterview
{
    public class CsvFileProcessor
    {
        public string InputFilePath { get; }
        public string OutputFilePath { get; }

        public CsvFileProcessor(string inputFilePath, string outputFilePath)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
        }
        public void Process()
        {
            using (StreamReader input = File.OpenText(InputFilePath))
            using (CsvReader csvReader = new CsvReader(input, CultureInfo.InvariantCulture))
            using (StreamWriter writer = new StreamWriter(OutputFilePath))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                //Console.WriteLine("Reading...");
                var headerLine = input.ReadLine();

                //var csv = new CsvReader(input, CultureInfo.InvariantCulture);

                csvReader.Configuration.HasHeaderRecord = false;
                csvReader.Configuration.MissingFieldFound = null;
                csvReader.Configuration.Delimiter = ";";

                IEnumerable<Person> records = csvReader.GetRecords<Person>();

                try
                {
                    var outputForRecordByCountry =
                        records.GroupBy(a => a.Country)
                        .Select
                            (a => new
                            {
                                Country = a.Key,
                                AverageScore = a.Average(c => Int32.Parse(c.Score)),
                                MaxScore = a.Max(b => Int32.Parse(b.Score)),
                                MedianScore = a.OrderBy(b => Int32.Parse(b.Score)).Count() % 2 == 1
                                ? a.Skip(a.Count() / 2 ).Select(r => Int32.Parse(r.Score)).First()
                                : a.Skip(a.Count() / 2 - 1).Take(2).Select(r => Int32.Parse(r.Score)).Average(),
                                MaxScorePerson = a.OrderByDescending(s => Int32.Parse(s.Score)).First().Name,
                                MinScore = a.Min(b => Int32.Parse(b.Score)),
                                MinScorePerson = a.OrderBy(s => Int32.Parse(s.Score)).First().Name,
                                RecordCount = a.Count()
                            })
                            .OrderByDescending(a => a.AverageScore)
                            .ToList();
                    
                    csvWriter.WriteRecords(outputForRecordByCountry);
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Writing in file went wrong...", ex);
                }
            }
        }

    }
}
