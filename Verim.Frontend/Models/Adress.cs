using System;

namespace Verim.Frontend.Models;

public class TypeDetails
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class ProvinceDetails
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class DistrictDetails
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class NeighborhoodDetails
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class BlockDetails
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class ParcelDetails
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class PlantedDetails
{
    public int Id { get; set; }
    public required string Name { get; set; }
}