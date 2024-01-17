// Interfaces/IMemberService.cs

using System.Collections.Generic;

public interface IMemberService
{
    List<Member> GetAllMembers();
    Member GetMemberById(int id);
    void AddMember(Member newMember);
    void UpdateMember(Member updatedMember);
    void DeleteMember(int id);
}
