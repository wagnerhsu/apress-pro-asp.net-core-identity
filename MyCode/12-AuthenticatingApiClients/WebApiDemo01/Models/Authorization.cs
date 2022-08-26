// Copyright (c) xxx, 2022. All rights reserved.

namespace WebApiDemo01.Models
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            Moderator,
            User
        }


        public const string default_username = "user";
        public const string default_email = "user@secureapi.com";
        public const string default_password = "Pa$$w0rd.";
        public const Roles default_role = Roles.User;
    }
}
