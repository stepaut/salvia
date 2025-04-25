using Microsoft.VisualBasic.FileIO;
using salvia.Core.Temperature;
using salvia.Data.Dto;
using System.Globalization;
using System.Text;

namespace salvia.Core.Disease;

internal class DiseaseParser : IDisposable
{
    private readonly ICollection<Row> _rows = new List<Row>();

    public void Dispose()
    {
        _rows.Clear();
    }

    public void Parse(Stream stream)
    {
        using var parser = new TextFieldParser(stream, Encoding.UTF8);
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");

        // pattern: date/time/value

        while (!parser.EndOfData)
        {
            //Processing row
            var fields = parser.ReadFields();
            if (fields is null || fields.Length < 3)
            {
                throw new Exception("fields is null or incorrect");
            }

            if (!DateOnly.TryParse(fields[0], out var date))
            {
                throw new Exception("failed to parse date");
            }

            if (!TimeOnly.TryParse(fields[1], out var time))
            {
                throw new Exception("failed to parse time");
            }

            if (!float.TryParse(fields[2].Replace(',', '.'), CultureInfo.InvariantCulture, out var value))
            {
                throw new Exception("failed to parse value");
            }

            if (!ITemperatureService.ValidateTemperature(value))
            {
                throw new Exception("value not validated");
            }

            _rows.Add(new Row()
            {
                DateTime = new DateTime(date, time),
                Date = date,
                Time = time,
                Value = value,
            });
        }
    }

    public DiseaseDto GetDisease()
    {
        var start = _rows.Min(x => x.DateTime);
        var end = _rows.Max(x => x.DateTime);

        return new DiseaseDto()
        {
            Start = start,
            End = end,
        };
    }

    public ICollection<TemperatureDto> GetTemperatures()
    {
        return _rows.Select(x => new TemperatureDto()
        {
            Date = x.DateTime,
            Temperature = x.Value,
        }).ToList();
    }

    class Row
    {
        public DateTime DateTime { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public float Value { get; set; }
    }
}
