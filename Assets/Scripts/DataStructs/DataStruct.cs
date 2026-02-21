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
    Kings_Row,
    Watchpoint_Gibraltar,
    Numbani,
    Dorado,
    Hollywood,
    Lijiang_Tower,
    Ilios,
    Nepal,
    Route66,
    Eichenwalde,
    Oasis,
    Junkertown,
    Blizzard_World,
    Rialto,
    Busan,
    Havana,
    New_Queen_Street,
    Circuit_Royal,
    Colosseo,
    Midtown,
    Paraiso,
    Esperanca,
    Shambali_Monastery,
    Antarctic_Peninsula,
    New_Junk_City,
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
    Reinhardt,
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
    Wrecking_Ball,
    Ashe,
    Baptiste,
    Sigma,
    Echo,
    Sojourn,
    Junker_Queen,
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
    Jetpack_Cat
}

public enum PopUpType
{
    Error,
    Info,
    Success,
    Warning,
    Confirm
}