using Gateway.Contracts;
using Gateway.Data;

namespace Gateway.Entities;

public class Route : IEntity
{
    
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string RouteId { get; set; }
    public string Path { get; set; }

    public List<string>? Methods { get; set; }

    public int ClusterId { get; set; }
    public Cluster Cluster { get; set; }
    
}