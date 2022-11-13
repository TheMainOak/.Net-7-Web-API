namespace Athlete_Manager_API.Models;

public class Athlete
{
    public long ID {get;set;}
    public string? FirstName {get;set;}
    public string? Surname {get;set;}
    public string? Sport {get;set;}
    public int Age {get;set;}
    public bool IsActive {get;set;}

    public string? Secret {get;set;}
}