﻿using SikkimGov.Platform.Models.ApiModels;

namespace SikkimGov.Platform.Business.Services.Contracts
{
    public interface IUserService
    {
        bool IsUserExists(string userName);
    }
}