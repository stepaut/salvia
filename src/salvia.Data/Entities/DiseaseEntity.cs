using System;
using System.Collections.Generic;

namespace salvia.Data.Entities;

internal class DiseaseEntity
{
    public int Id { get; set; }

    public DateTime Start { get; set; }
    public DateTime? End { get; set; }

    public long UserId { get; set; }

    public ICollection<TemperatureEntity> Temperatures { get; set; } = [];
}
