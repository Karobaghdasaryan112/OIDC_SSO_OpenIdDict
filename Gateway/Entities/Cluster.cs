using Gateway.Contracts;

namespace Gateway.Entities;

public class Cluster : IEntity
{
    
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string ClusterId { get; set; }

    public List<Destination> Destinations { get; set; } = new();
    
}