﻿namespace ju.model
{
    public class AuthResponse
    {
        public int UserId { get; set; }

        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? role { get; set; }
        public string Token { get; set; }
    }
}