namespace API.Models
{
    public class PasswordResponseModel
    {

    public string StrengthFeedback { get; set; } = string.Empty;

    public bool IsCompromised { get; set; }
    public string CompromisedFeedback { get; set; } = string.Empty;


    }


    
}