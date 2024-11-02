namespace Iks.WebApi.Transversal.Modelos;

public class AppSettings
{
    public string[] OriginsCors { get; set; }
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }

}
