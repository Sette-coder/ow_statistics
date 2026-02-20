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

public struct MapData
{
    public string MapName;
    public MapMode Mode;

    public MapData(string mapName, MapMode mode)
    {
        MapName = mapName;
        Mode = mode;
    }
}

public enum HeroRoles
{
    None = 0,
    Support = 1,
    Damage = 2,
    Tank = 3,
}

public struct Hero
{
    public string HeroName;
    public HeroRoles Role;

    public Hero(string heroName, HeroRoles role)
    {
        HeroName = heroName;
        Role = role;
    }
}

public struct MatchData
{
    public string UserEmail;
    public MatchResult MatchResult;
    public Maps Map;
    public int Season;
    public Ranks Rank;
    public int RankDivision;
    public int RankPercentage;
    public Heroes Hero_1;
    public Heroes Hero_2;
    public Heroes Hero_3;
    public Heroes TeamHeroBan1;
    public Heroes TeamHeroBan2;
    public Heroes EnemyTeamHeroBan1;
    public Heroes EnemyTeamHeroBan2;
    [CanBeNull] public string TeamNotes;
    [CanBeNull] public string EnemyTeamNotes;
}

public enum MatchResult
{
    Win,
    Lose,
    Draw
}

public enum Ranks
{
    Bronze,
    Silver,
    Gold,
    Platinum,
    Diamond,
    Master,
    Grandmaster,
    Champion
}

public enum Maps
{
    KingsRow,
    Numbani,
    Dorado,
    Hollywood,
    LijiangTower,
    Ilios,
    Nepal,
    Route66,
    Eichenwalde,
    Oasis,
    Junkertown,
    BlizzardWorld,
    Rialto,
    Busan,
    Havana,
    NewQueenStreet,
    CircuitRoyal,
    Colosseo,
    Midtown,
    Paraiso,
    Esperanca,
    ShambaliMonastery,
    AntarcticPeninsula,
    NewJunkCity,
    Suravasa,
    Samoa,
    Runasapi,
    Aatlis
}

public enum Heroes
{
    None = 0,
    Tracer,
    Reaper,
    Widowmaker,
    Phara,
    ReinHardt,
    Mercy,
    Torbjorn,
    Hanzo,
    Winston,
    Zenyatta,
    Bastion,
    Symmetra,
    Zarya,
    Cassidy,
    Soldier76,
    Lucio,
    Roadhog,
    Junkrat,
    DVa,
    Mei,
    Genji,
    Ana,
    Sombra,
    Orisa,
    Doomfist,
    Moira,
    Brigitte,
    WreckingBall,
    Ashe,
    Baptiste,
    Sigma,
    Echo,
    Sojourn,
    JunkerQueen,
    Kiriko,
    Ramattra,
    Lifeweaver,
    Illari,
    Mauga,
    Venture,
    Juno,
    Hazard,
    Freja,
    Wuyang,
    Vendetta,
    Domina,
    Emre,
    Mizuki,
    Anran,
    JetpackCat
}

public enum PopUpType
{
    Error,
    Info,
    Success,
    Warning,
}