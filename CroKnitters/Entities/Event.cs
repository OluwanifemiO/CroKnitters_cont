using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CroKnitters.Entities;

public class Event
{
    public int EventId { get; set; }

    [Required(ErrorMessage = "Pattern name is required")]
    public string EventTitle { get; set; } = null!;

    [Required(ErrorMessage = "A description for this pattern is required")]
    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public int OwnerId { get; set; }

    public ICollection<EventUser> EventUsers { get; set; } = new List<EventUser>();

    public User? Owner { get; set; } = null!;
}
