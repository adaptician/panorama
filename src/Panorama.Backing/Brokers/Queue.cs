namespace Panorama.Backing.Brokers;

public class Queue(string name, bool isDurable = false, bool isExclusive = false, bool willAutoDelete = true)
{
    public string Name { get; set; } = name;
    
    // Transient queues will be deleted on node boot.
    // They therefore will not survive a node restart, by design.
    // Messages in transient queues will also be discarded.
    public bool IsDurable { get; set; } = isDurable;
    
    // An exclusive queue can only be used (consumed from, purged, deleted, etc) by its declaring connection.
    // In RabbitMQ a queue is exclusive by default, but
    // auto-provisioned queues should not be marked as exclusive.
    public bool IsExclusive { get; set; } = isExclusive;
    
    public bool WillAutoDelete { get; set; } = willAutoDelete;
}