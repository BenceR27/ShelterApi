using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShelterApi.Models;

public partial class Animal
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Species { get; set; } = null!;

    public int Age { get; set; }

    public double Weight { get; set; }

    public DateTime RegTime { get; set; }

    public string ShelterId { get; set; } = null!;

    [JsonIgnore]
    public virtual Shelter Shelter { get; set; } = null!;
}
