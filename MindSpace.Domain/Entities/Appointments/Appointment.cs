﻿using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Domain.Entities.Appointments
{
    public class Appointment : BaseEntity
    {
        // 1 Student - M Appointments
        public int StudentId { get; set; }
        public Student Student { get; set; }

        // 1 Psychologist - M Appointments
        public int PsychologistId { get; set; }
        public Psychologist Psychologist { get; set; }

        // 1 Appointment - 1 PsychologistSchedule
        public int PsychologistScheduleId { get; set; }
        public virtual PsychologistSchedule PsychologistSchedule { get; set; }

        // 1 Specialization - M Appointments
        public int SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }

        // 1 Appointment - M Payments
        public virtual ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();

        // 1 Appointment - 1 MeetingRoom
        public int? MeetingRoomId { get; set; }
        public virtual MeetingRoom? MeetingRoom { get; set; }

        public string SessionId { get; set; }
        public AppointmentStatus Status { get; set; }

        // Notes field
        public string? NotesTitle { get; set; }
        public string? KeyIssues { get; set; }
        public string? Suggestions { get; set; }
        public string? OtherNotes { get; set; }
        public bool? IsNoteShown { get; set; }
    }
}
