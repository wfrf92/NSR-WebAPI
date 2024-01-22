// Services/MemberService.cs

using System.Collections.Generic;
using Newtonsoft.Json;

public class MemberService : IMemberService
{
    private List<Member> _members;

    public MemberService()
    {
        _members = GetMemberFromJsonFile();
    }

    public List<Member> GetMemberFromJsonFile()
    {
        var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Json/members.json");

        if (!File.Exists(jsonFilePath))
        {
            // Handle the case when the file does not exist
            return new List<Member>();
        }

        var json = File.ReadAllText(jsonFilePath);
        var products = JsonConvert.DeserializeObject<List<Member>>(json);

        return products ?? new List<Member>();
    }

    public List<Member> GetAllMembers()
    {
        return _members;
    }

    public Member GetMemberById(int id)
    {
        return _members.Find(member => member.Id == id);
    }

    public void AddMember(Member newMember)
    {
        _members.Add(newMember);
    }

    public void UpdateMember(Member updatedMember)
    {
        var index = _members.FindIndex(member => member.Id == updatedMember.Id);
        if (index != -1)
        {
            _members[index] = updatedMember;
        }
    }

    public void DeleteMember(int id)
    {
        _members.RemoveAll(member => member.Id == id);
    }
}
