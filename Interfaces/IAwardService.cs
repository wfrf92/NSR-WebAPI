// IAwardService.cs
using System.Collections.Generic;

public interface IAwardService
{
    List<Award> GetAllAwards();
    Award GetAwardById(int id);
    void AddAward(Award award);
    void UpdateAward(int id, Award updatedAward);
    void DeleteAward(int id);
}