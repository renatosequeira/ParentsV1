﻿namespace Parents.Models.Sistema
{
    public class ChangePasswordRequest
    {
        public string Email { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

    }
}
