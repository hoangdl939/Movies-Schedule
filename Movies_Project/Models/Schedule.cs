using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movies_Project.Models
{
    public partial class Schedule
    {

        public int Id { get; set; }

        
        public int MovieId { get; set; }

        
        public int RoomId { get; set; }

        
        public int? TimeSlotId { get; set; }

        [Required(ErrorMessage = "Start Date Required!")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date Required!")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Note Required!")]
        public string Note { get; set; }

        
        public virtual Movie Movie { get; set; }

       
        public virtual Room Room { get; set; }

        
        public virtual TimeSlot TimeSlot { get; set; }
    }
}
