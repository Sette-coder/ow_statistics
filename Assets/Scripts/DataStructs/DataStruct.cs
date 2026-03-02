using JetBrains.Annotations;

public enum MapMode
{
    Default = 0,
    Escort = 1,
    Hybrid = 2,
    Control = 3,
    Push = 4,
    Flashpoint = 5
}

public class Map
{
    public int Id;
    public string Name;
    public MapMode Mode;
    public int ModeId;
}

public enum HeroRoles
{
    None = 0,
    Support = 1,
    Damage = 2,
    Tank = 3,
}

public class Hero
{
    public int Id;
    public string Name;
    public HeroRoles Role;
}

public enum MatchResult
{
    Win,
    Lose,
    Draw
}

public enum Ranks
{
    Bronze = 0,
    Silver = 1,
    Gold = 2,
    Platinum = 3,
    Diamond = 4,
    Master = 5,
    Grandmaster = 6,
    Champion = 7
}


public enum PopUpType
{
    Error,
    Info,
    Success,
    Warning,
    Confirm
}

[System.Serializable]
public class FromDatabaseMaps
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Mode { get; set; }
    public int ModeId { get; set; }
}

[System.Serializable]
public class FromDatabaseHeroes
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
}