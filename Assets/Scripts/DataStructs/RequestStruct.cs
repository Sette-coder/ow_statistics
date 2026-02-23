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
    public string Username;
    public string MapName;
    public string Season;
    public string Rank;
    public int RankDivision;
    public int RankPercentage;
    public string Hero_1;
    public string Hero_2;
    public string Hero_3;
    public string MatchResult;
    public string TeamBan_1;
    public string TeamBan_2;
    public string EnemyTeamBan_1;
    public string EnemyTeamBan_2;
    public string TeamNotes;
    public string EnemyTeamNotes;
}

[System.Serializable]
public class UsernameRequest
{
    public string Username;
}

public class MatchData
{
    public int Id;
    public string Username;
    public string UploadTime;
    public Maps MapName;
    public string Season;
    public string Rank;
    public int RankDivision;
    public int RankPercentage;
    public Heroes Hero_1;
    public Heroes Hero_2;
    public Heroes Hero_3;
    public string MatchResult;
    public Heroes TeamBan_1;
    public Heroes TeamBan_2;
    public Heroes EnemyTeamBan_1;
    public Heroes EnemyTeamBan_2;
    public string TeamNotes;
    public string EnemyTeamNotes;
}
[System.Serializable]
public class MatchResponse
{
    public int Id;
    public string Username;
    public string UploadTime;
    public string MapName;
    public string Season;
    public string Rank;
    public int RankDivision;
    public int RankPercentage;
    public string Hero_1;
    public string Hero_2;
    public string Hero_3;
    public string MatchResult;
    public string TeamBan_1;
    public string TeamBan_2;
    public string EnemyTeamBan_1;
    public string EnemyTeamBan_2;
    public string TeamNotes;
    public string EnemyTeamNotes;
}

[System.Serializable]
public class MatchListResponse
{
    public List<MatchResponse> Matches;
}