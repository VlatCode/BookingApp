﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HostelBookingSystem.Models
{
    public class Hostel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        // public string Address { get; set; } - implement property
        // public string Description { get; set; } - implement property
        public List<Room> Rooms = new List<Room>();
    }
}
