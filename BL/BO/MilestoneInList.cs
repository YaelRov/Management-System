﻿
namespace BO;
/// <summary>
/// class for details about milestone in list
/// </summary>
public class MilestoneInList
{
    public int Id { get; init; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public Status? Status { get; set; }
    public double? CompletionPercentage { get; set; }
    public override string ToString() => this.ToStringProperty();
    string ToStringProperty()
    {
        return $" Id: {Id}, Description: {Description}, Alias: {Alias}, Status: {Status}, CompletionPercentage: {CompletionPercentage}";
    }
}
