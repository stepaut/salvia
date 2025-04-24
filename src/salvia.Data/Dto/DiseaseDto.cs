using System;

namespace salvia.Data.Dto;

public class DiseaseDto
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
    public long UserId { get; set; }

    public override string ToString()
    {
        return $"{Id} : {Start.ToShortDateString()} - {End?.ToShortDateString()}";
    }
}
