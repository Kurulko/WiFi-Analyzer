using System.ComponentModel.DataAnnotations;

namespace WiFi_Analyzer.Models;

public interface IEntityBase
{
    [Key]
    long Id { get; set; }
}