﻿namespace People_Power.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
