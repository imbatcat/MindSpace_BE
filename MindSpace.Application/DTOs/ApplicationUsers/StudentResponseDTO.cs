﻿namespace MindSpace.Application.DTOs.ApplicationUsers
{
    public class StudentResponseDTO
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public SchoolDTO? School { get; set; }
    }
}