using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace Panorama.Simulations;

/// <summary>
/// A tracking table for simulations that are running.
/// Start and end times are captured.
/// Upon ending, the record is softly deleted, and forms part of history tracking.
/// </summary>
[Table(nameof(SimulationRun), Schema = SchemaNames.Panorama)]
public class SimulationRun : FullAuditedEntity<long>
{
    /// <summary>
    /// The identifier of the simulation that is running.
    /// </summary>
    public long SimulationId { get; set; }
    [ForeignKey(nameof(SimulationId))]
    public virtual Simulation Simulation { get; set; }
    
    /// <summary>
    /// The time at which the simulation started running.
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// The time at which the simulation stopped running.
    /// </summary>
    public DateTime? EndTime { get; set; }
}