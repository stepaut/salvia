using System;

namespace salvia.Data.Dto;

public class TemperatureDto
{
    public int Id { get; set; }
    public float Temperature { get; set; }
    public DateTime Date { get; set; }
    public int DiseaseId { get; set; }

    public override string ToString()
    {
        return $"{Date} : {Temperature}";
    }
}
