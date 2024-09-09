using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Configuration;
#nullable disable
public class JwtConfiguration : IJwtConfiguration
{
    public const string Section = "JwtSettings";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int Expires { get; set; }
}
