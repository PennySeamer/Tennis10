﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tennis10.Models
{
    public partial class Schedule
    {
        public Schedule()
        {
            ScheduleMembers = new HashSet<ScheduleMembers>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        public DateTime When { get; set; }
        public string Description { get; set; }
        public string CoachEmail { get; set; }
        public string Location { get; set; }

        [InverseProperty("Schedule")]
        public virtual ICollection<ScheduleMembers> ScheduleMembers { get; set; }
    }
}
