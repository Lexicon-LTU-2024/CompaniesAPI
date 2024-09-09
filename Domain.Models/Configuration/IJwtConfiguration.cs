namespace Domain.Models.Configuration;

public interface IJwtConfiguration
{
    string Audience { get; set; }
    int Expires { get; set; }
    string Issuer { get; set; }
}