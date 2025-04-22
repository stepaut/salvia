using System;

namespace salvia.Data.Entities;

internal class TemperatureEntity
{
    public int Id { get; set; }

    public float Temperature { get; set; }
    public DateTime Date { get; set; }

    public int DiseaseId { get; set; }
    public DiseaseEntity Disease { get; set; }
}
