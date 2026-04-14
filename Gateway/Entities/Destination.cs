using Gateway.Contracts;
using Gateway.Data;

namespace Gateway.Entities;

public class Destination : IEntity
{
    
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Address { get; set; }

    public int ClusterId { get; set; }
    public Cluster Cluster { get; set; }
    
}