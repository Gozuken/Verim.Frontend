using System;

namespace Verim.Frontend.Models;

public class Option
{
    required public int Id {get; set;}
    required public string Name {get; set;}
}

public class ComboData
{
    public List<Option>? Types { get; set; }
    public List<Option>? Provinces { get; set; }
    public List<Option>? Districts { get; set; }
    public List<Option>? Neighborhoods { get; set; }
    public List<Option>? Planteds { get; set; }
}