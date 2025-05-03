using System;

namespace API.Services;

public interface IPasswordService
{
   Task<bool> CheckCompromised(string password);
 
    bool CheckStrength(string password);
    string GetStrengthFeedback(string password);

}
