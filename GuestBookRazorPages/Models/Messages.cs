﻿namespace GuestBookRazorPages.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int Id_User { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
