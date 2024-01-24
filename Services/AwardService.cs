// AwardService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class AwardService : IAwardService
{
    private static List<Award> awards = new List<Award>();

    public AwardService()
    {
        awards = GetAwardFromJsonFile();
    }

    public List<Award> GetAwardFromJsonFile()
    {
        // Add this before the file check
Console.WriteLine($"Current Working Directory: {AppContext.BaseDirectory}");


        var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Json/awards.json");
Console.WriteLine($"Composed File Path: {jsonFilePath}");
        if (!File.Exists(jsonFilePath))
        {
            // Handle the case when the file does not exist
            return new List<Award>();
        }

        var json = File.ReadAllText(jsonFilePath);
        var products = JsonConvert.DeserializeObject<List<Award>>(json);

        return products ?? new List<Award>();
    }

    public List<Award> GetAllAwards()
    {
        return awards;
    }

    public Award GetAwardById(int id)
    {
        return awards.FirstOrDefault(a => a.Id == id);
    }

    public void AddAward(Award award)
    {
        if (award == null)
        {
            throw new ArgumentNullException(nameof(award));
        }

        award.Id = awards.Count + 1;
        award.Active = true;
        awards.Add(award);
         // Serialize the updated products list to JSON
        string json = JsonConvert.SerializeObject(awards);

        // Specify the path to your JSON file
        string jfilePath = "Json/awards.json";

        // Write the JSON data to the file
         File.WriteAllTextAsync(jfilePath, json);
    }

    public void UpdateAward(int id, Award updatedAward)
    {
        var existingAward = awards.FirstOrDefault(a => a.Id == id);

        if (existingAward != null)
        {
            existingAward.Icon = updatedAward.Icon;
            existingAward.Date = updatedAward.Date;
            existingAward.Name = updatedAward.Name;
            existingAward.Description = updatedAward.Description;
            existingAward.Active = updatedAward.Active;
        }
    }

    public void DeleteAward(int id)
    {
        var award = awards.FirstOrDefault(a => a.Id == id);

        if (award != null)
        {
            awards.Remove(award);
        }
    }
}
