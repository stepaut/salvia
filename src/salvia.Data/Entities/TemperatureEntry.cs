using System;

namespace salvia.Data.Entities;

internal class TemperatureEntry
{
    public int Id { get; set; }

    public float Temperature { get; set; }
    public DateTime Date { get; set; }

    public int DiseaseId { get; set; }
    public DiseaseEntry Disease { get; set; }
}
