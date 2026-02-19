using JetBrains.Annotations;

[System.Serializable]
public class CreateUserRequest
{
    public string Username;
    public string Email;
    public string Password;
}

[System.Serializable]
public class AppUserResponse
{
    public int Id;
    public string Username;
    public string Email;
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
    public string LoginMessage;
}

[System.Serializable]
public class MatchRequest
{
    public string UserEmail;
    public string MatchResult;
    public string MapName;
    public int Season;
    public string Rank;
    public int RankDivision;
    public int RankPercentage;
    public string Hero_1;
    [CanBeNull] public string Hero_2;
    [CanBeNull] public string Hero_3;
    public string TeamHeroBan1;
    public string TeamHeroBan2;
    public string EnemyTeamHeroBan1;
    public string EnemyTeamHeroBan2;
    [CanBeNull] public string TeamNotes;
    [CanBeNull] public string EnemyTeamNotes;
}

[System.Serializable]
public class GenericResponse
{
    public bool ok;
    public string ResponseMessage;
}