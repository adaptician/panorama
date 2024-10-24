using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Panorama.Authorization.Users;

namespace Panorama.Simulations;

/// <summary>
/// A tracking table for users participating in a running simulation.
/// User entry and exit times are captured.
/// Upon user exit, the record is softly deleted, and forms part of history tracking.
/// </summary>
[Table(nameof(SimulationRunParticipant), Schema = SchemaNames.Panorama)]
public class SimulationRunParticipant : FullAuditedEntity<long>, IMustHaveTenant
{
    /// <summary>
    /// The identifier of the running simulation being participated in.
    /// </summary>
    public long SimulationRunId { get; set; }
    [ForeignKey(nameof(SimulationRunId))]
    public virtual SimulationRun SimulationRun { get; set; }
    
    /// <summary>
    /// The identifier of the user that is participating.
    /// </summary>
    public long UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }
    
    /// <summary>
    /// The time at which the user began to participate in the running simulation.
    /// </summary>
    public DateTime EntryTime { get; set; }
    
    /// <summary>
    /// The time at which the user ended their participation in the running simulation.
    /// </summary>
    public DateTime? ExitTime { get; set; }
    
    public int TenantId { get; set; }
}