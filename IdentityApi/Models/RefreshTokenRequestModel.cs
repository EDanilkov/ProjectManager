﻿namespace IdentityApi.Models
{
    public class RefreshTokenRequestModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
