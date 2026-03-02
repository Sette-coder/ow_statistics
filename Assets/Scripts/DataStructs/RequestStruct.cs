using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Serialization;

[System.Serializable]
public class CreateUserRequest
{
    public string Username;
    public string Email;
    public string Password;
    public string Role;
}

[System.Serializable]
public class CreateMapRequest
{
    public string Name;
    public string Mode;
    public int ModeId;
}

[System.Serializable]
public class MapResponse
{
    public string Name;
    public string Mode;
    public int ModeId;
}

[System.Serializable]
public class CreateHeroRequest
{
    public string Name;
    public string Role;
}

[System.Serializable]
public class HeroResponse
{
    public string Name;
    public string Role;
}

[System.Serializable]
public class LoginRequest
{
    public string UsernameOrEmail;
    public string Password;
}

[System.Serializable]
public class LoginResponse
{
    public bool Authorized;
    public int UserId;
    public string Username;
    public string UserEmail;
    public string Role;
    public string LoginMessage;
}

[System.Serializable]
public class GenericResponse
{
    public bool ok;
    public string ResponseMessage;
}

[System.Serializable]
public class MatchDataSubmitRequest
{
    public int UserId;
    public int MapId;
    public string Season;
    public string Rank;
    public int RankDivision;
    public int RankPercentage;
    public int Hero1Id;
    public int? Hero2Id;
    public int? Hero3Id;
    public string MatchResult;
    public int TeamBan1Id;
    public int TeamBan2Id;
    public int EnemyTeamBan1Id;
    public int EnemyTeamBan2Id;
    [CanBeNull] public string TeamNotes;
    [CanBeNull] public string EnemyTeamNotes;
}

[System.Serializable]
public class UserIdRequest
{
    public int UserId;
}

[System.Serializable]
public class MatchData
{
    public int Id;
    public int UserId;
    public string SubmitTime;
    public Map Map;
    public string Season;
    public string Rank;
    public int RankDivision;
    public int RankPercentage;
    public Hero Hero1;
    [CanBeNull] public Hero Hero2;
    [CanBeNull] public Hero Hero3;
    public string MatchResult;
    public Hero TeamBan1;
    public Hero TeamBan2;
    public Hero EnemyTeamBan1;
    public Hero EnemyTeamBan2;
    [CanBeNull] public string TeamNotes;
    [CanBeNull] public string EnemyTeamNotes;
}
