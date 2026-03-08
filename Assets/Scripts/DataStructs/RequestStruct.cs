using System;
using JetBrains.Annotations;

[Serializable]
public class CreateUserRequest
{
    public string Username;
    public string Email;
    public string Password;
    public string Role;
}

[Serializable]
public class CreateMapRequest
{
    public string Name;
    public string Mode;
    public int ModeId;
}

[Serializable]
public class MapResponse
{
    public string Name;
    public string Mode;
    public int ModeId;
}

[Serializable]
public class CreateHeroRequest
{
    public string Name;
    public string Role;
}

[Serializable]
public class HeroResponse
{
    public string Name;
    public string Role;
}

[Serializable]
public class LoginRequest
{
    public string UsernameOrEmail;
    public string Password;
}

[Serializable]
public class LoginResponse
{
    public bool Authorized { get; set; } = false;
    public int UserId { get; set; } = -1;
    public string Username { get; set; } = "";
    public string UserEmail { get; set; } = "";
    public string Role { get; set; } = "";
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
    public string LoginMessage { get; set; } = "";
}

[Serializable]
public class GenericResponse
{
    public bool Ok;
    public string ResponseMessage;
}

[Serializable]
public class MatchDataSubmitRequest
{
    public int UserId;
    public int MapId;
    public string Season;
    public string Rank;
    public int RankDivision;
    public int RankPercentage;
    public int Hero1Id;
    public string MatchResult;
    public int TeamBan1Id;
    public int TeamBan2Id;
    public int EnemyTeamBan1Id;
    public int EnemyTeamBan2Id;
    [CanBeNull] public string TeamNotes;
    [CanBeNull] public string EnemyTeamNotes;
    public int? Hero2Id;
    public int? Hero3Id;
}

[Serializable]
public class RefreshRequest
{
    public string RefreshToken { get; set; } = "";
}

[Serializable]
public class RefreshResponse
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
}

[Serializable]
public class UserProfileResponse
{
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public string UserEmail { get; set; } = "";
    public string Role { get; set; } = "";
}

[Serializable]
public class UpdateUserRequest
{
    [CanBeNull] public string Username { get; set; } // null = no change
    [CanBeNull] public string Email { get; set; } // null = no change
    [CanBeNull] public string CurrentPassword { get; set; } // required only when changing password as non-admin
    [CanBeNull] public string NewPassword { get; set; } // null = no change
    [CanBeNull] public string Role { get; set; } // null = no change, Admin only
}

[Serializable]
public class UpdateUserResponse
{
    public bool Ok { get; set; } = false;
    public string ResponseMessage { get; set; } = "";
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public string UserEmail { get; set; } = "";
    public string Role { get; set; } = "";
    public bool SessionsRevoked { get; set; } = false; // tells Unity to force re-login
}

[Serializable]
public class UserIdRequest
{
    public int UserId;
}

public class CreateMatchRequest
{
    public int UserId { get; set; } = -1;
    public int MapId { get; set; } = -1;
    public string Season { get; set; } = "";
    public string Rank { get; set; } = "";
    public int RankDivision { get; set; } = 0;
    public int RankPercentage { get; set; } = 0;
    public int Hero1Id { get; set; } = -1;
    public int? Hero2Id { get; set; }
    public int? Hero3Id { get; set; }
    public string MatchResult { get; set; } = "";
    public int TeamBan1Id { get; set; } = -1;
    public int TeamBan2Id { get; set; } = -1;
    public int EnemyTeamBan1Id { get; set; } = -1;
    public int EnemyTeamBan2Id { get; set; } = -1;
    [CanBeNull] public string TeamNotes { get; set; }
    [CanBeNull] public string EnemyTeamNotes { get; set; }
}

public class UpdateMatchRequest
{
    // All fields optional — only provided fields will be updated
    public int? MapId { get; set; }
    [CanBeNull] public string Season { get; set; }
    [CanBeNull] public string Rank { get; set; }
    public int? RankDivision { get; set; }
    public int? RankPercentage { get; set; }
    public int? Hero1Id { get; set; }
    public int? Hero2Id { get; set; } // send -1 to clear
    public int? Hero3Id { get; set; } // send -1 to clear
    [CanBeNull] public string MatchResult { get; set; }
    public int? TeamBan1Id { get; set; }
    public int? TeamBan2Id { get; set; }
    public int? EnemyTeamBan1Id { get; set; }
    public int? EnemyTeamBan2Id { get; set; }
    [CanBeNull] public string TeamNotes { get; set; }
    [CanBeNull] public string EnemyTeamNotes { get; set; }
}


public class MatchDto
{
    public int Id { get; set; } = -1;
    public int UserId { get; set; } = -1;
    public DateTime SubmitTime { get; set; } = DateTime.UtcNow;
    public Map Map { get; set; } = new();
    public string Season { get; set; } = "";
    public string Rank { get; set; } = "";
    public int RankDivision { get; set; } = -1;
    public int RankPercentage { get; set; } = -1;
    public Hero Hero1 { get; set; } = new();
    [CanBeNull] public Hero Hero2 { get; set; }
    [CanBeNull] public Hero Hero3 { get; set; }
    public string MatchResult { get; set; } = "";
    public Hero TeamBan1 { get; set; } = new();
    public Hero TeamBan2 { get; set; } = new();
    public Hero EnemyTeamBan1 { get; set; } = new();
    public Hero EnemyTeamBan2 { get; set; } = new();
    [CanBeNull] public string TeamNotes { get; set; }
    [CanBeNull] public string EnemyTeamNotes { get; set; }
}

[Serializable]
// Shared response for both map and hero aggregations
public class AggregatedStatsResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = ""; // map name or hero name
    public string SubGroup { get; set; } = ""; // hero role (empty for maps)
    public int TotalMatches { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public double WinRate { get; set; }
}