using System;

namespace LegacyApp;

public class UserValidator
{
    public bool ValidateUser(string firstName, string lastName,string email,DateTime dateOfBirth)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            return false;
        }

        if (!email.Contains("@") && !email.Contains("."))
        {
            return false;
        }

        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

        if (age < 21)
        {
            return false;
        }
        return true;
    }
}