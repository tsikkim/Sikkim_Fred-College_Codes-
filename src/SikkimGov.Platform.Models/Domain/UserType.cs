using System;

namespace SikkimGov.Platform.Models.Domain
{
    public enum UserType
    {
        SuperAdmin = 1,
        Admin = 2,
        DDOUser = 4,
        RCOUser = 8
    }
}
