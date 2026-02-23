namespace Tha7.Utility
{
    public static class StringWrapper
    {
        public static Maps FromMapName(string mapName)
        {
            switch (mapName)
            {
                case "King's Row":
                    return Maps.Kings_Row;
                case "Watchpoint: Gibraltar":
                    return Maps.Watchpoint_Gibraltar;
                case "Numbani":
                    return Maps.Numbani;
                case "Dorado":
                    return Maps.Dorado;
                case "Hollywood":
                    return Maps.Hollywood;
                case "Lijiang Tower":
                    return Maps.Lijiang_Tower;
                case "Ilios":
                    return Maps.Ilios;
                case "Nepal":
                    return Maps.Nepal;
                case "Route 66":
                    return Maps.Route66;
                case "Eichenwalde":
                    return Maps.Eichenwalde;
                case "Oasis":
                    return Maps.Oasis;
                case "Junkertown":
                    return Maps.Junkertown;
                case "Blizzard World":
                    return Maps.Blizzard_World;
                case "Rialto":
                    return Maps.Rialto;
                case "Busan":
                    return Maps.Busan;
                case "Havana":
                    return Maps.Havana;
                case "New Queen Street":
                    return Maps.New_Queen_Street;
                case "Circuit Royal":
                    return Maps.Circuit_Royal;
                case "Colosseo":
                    return Maps.Colosseo;
                case "Midtown":
                    return Maps.Midtown;
                case "Paraíso":
                    return Maps.Paraiso;
                case "Esperança":
                    return Maps.Esperanca;
                case "Shambali Monastery":
                    return Maps.Shambali_Monastery;
                case "Antarctic Peninsula":
                    return Maps.Antarctic_Peninsula;
                case "New Junk City":
                    return Maps.New_Junk_City;
                case "Suravasa":
                    return Maps.Suravasa;
                case "Samoa":
                    return Maps.Samoa;
                case "Runasapi":
                    return Maps.Runasapi;
                case "Aatlis":
                    return Maps.Aatlis;
                case "None":
                default:
                    return Maps.None;
                    
            }
        }

        public static string ToMapName(Maps map)
        {
            switch (map)
            {
                case Maps.Kings_Row:
                    return "King's Row";
                case Maps.Watchpoint_Gibraltar:
                    return "Watchpoint: Gibraltar";
                case Maps.Numbani:
                    return "Numbani";
                case Maps.Dorado:
                    return "Dorado";
                case Maps.Hollywood:
                    return "Hollywood";
                case Maps.Lijiang_Tower:
                    return "Lijiang Tower";
                case Maps.Ilios:
                    return "Ilios";
                case Maps.Nepal:
                    return "Nepal";
                case Maps.Route66:
                    return "Route 66";
                case Maps.Eichenwalde:
                    return "Eichenwalde";
                case Maps.Oasis:
                    return "Oasis";
                case Maps.Junkertown:
                    return "Junkertown";
                case Maps.Blizzard_World:
                    return "Blizzard World";
                case Maps.Rialto:
                    return "Rialto";
                case Maps.Busan:
                    return "Busan";
                case Maps.Havana:
                    return "Havana";
                case Maps.New_Queen_Street:
                    return "New Queen Street";
                case Maps.Circuit_Royal:
                    return "Circuit Royal";
                case Maps.Colosseo:
                    return "Colosseo";
                case Maps.Midtown:
                    return "Midtown";
                case Maps.Paraiso:
                    return "Paraíso";
                case Maps.Esperanca:
                    return "Esperança";
                case Maps.Shambali_Monastery:
                    return "Shambali Monastery";
                case Maps.Antarctic_Peninsula:
                    return "Antarctic Peninsula";
                case Maps.New_Junk_City:
                    return "New Junk City";
                case Maps.Suravasa:
                    return "Suravasa";
                case Maps.Samoa:
                    return "Samoa";
                case Maps.Runasapi:
                    return "Runasapi";
                case Maps.Aatlis:
                    return "Aatlis";
                default:
                    return "MAP NOT FOUND";
            }
        }

        public static Heroes FromHeroName(string heroName)
        {
            switch (heroName)
            {
                case "Tracer":
                    return Heroes.Tracer;
                case "Reaper":
                    return Heroes.Reaper;
                case "Widowmaker":
                    return Heroes.Widowmaker;
                case "Pharah":
                    return Heroes.Pharah;
                case "Reinhardt":
                    return Heroes.Reinhardt;
                case "Mercy":
                    return Heroes.Mercy;
                case "Torbjörn":
                    return Heroes.Torbjorn;
                case "Hanzo":
                    return Heroes.Hanzo;
                case "Winston":
                    return Heroes.Winston;
                case "Zenyatta":
                    return Heroes.Zenyatta;
                case "Bastion":
                    return Heroes.Bastion;
                case "Symmetra":
                    return Heroes.Symmetra;
                case "Zarya":
                    return Heroes.Zarya;
                case "Cassidy":
                    return Heroes.Cassidy;
                case "Soldier: 76":
                    return Heroes.Soldier76;
                case "Lúcio":
                    return Heroes.Lucio;
                case "Roadhog":
                    return Heroes.Roadhog;
                case "Junkrat":
                    return Heroes.Junkrat;
                case "D.Va":
                    return Heroes.DVa;
                case "Mei":
                    return Heroes.Mei;
                case "Genji":
                    return Heroes.Genji;
                case "Ana":
                    return Heroes.Ana;
                case "Sombra":
                    return Heroes.Sombra;
                case "Orisa":
                    return Heroes.Orisa;
                case "Doomfist":
                    return Heroes.Doomfist;
                case "Moira":
                    return Heroes.Moira;
                case "Brigitte":
                    return Heroes.Brigitte;
                case "Wrecking Ball":
                    return Heroes.Wrecking_Ball;
                case "Ashe":
                    return Heroes.Ashe;
                case "Baptiste":
                    return Heroes.Baptiste;
                case "Sigma":
                    return Heroes.Sigma;
                case "Echo":
                    return Heroes.Echo;
                case "Sojourn":
                    return Heroes.Sojourn;
                case "Junker Queen":
                    return Heroes.Junker_Queen;
                case "Kiriko":
                    return Heroes.Kiriko;
                case "Ramattra":
                    return Heroes.Ramattra;
                case "Lifeweaver":
                    return Heroes.Lifeweaver;
                case "Illari":
                    return Heroes.Illari;
                case "Mauga":
                    return Heroes.Mauga;
                case "Venture":
                    return Heroes.Venture;
                case "Juno":
                    return Heroes.Juno;
                case "Hazard":
                    return Heroes.Hazard;
                case "Freja":
                    return Heroes.Freja;
                case "Wuyang":
                    return Heroes.Wuyang;
                case "Vendetta":
                    return Heroes.Vendetta;
                case "Domina":
                    return Heroes.Domina;
                case "Emre":
                    return Heroes.Emre;
                case "Mizuki":
                    return Heroes.Mizuki;
                case "Anran":
                    return Heroes.Anran;
                case "Jetpack Cat":
                    return Heroes.Jetpack_Cat;
                default:
                case "":
                    return Heroes.None;
            }
        }

        public static string ToHeroName(Heroes hero)
        {
            switch (hero)
            {
                case Heroes.None:
                    return "";
                case Heroes.Tracer:
                    return "Tracer";
                case Heroes.Reaper:
                    return "Reaper";
                case Heroes.Widowmaker:
                    return "Widowmaker";
                case Heroes.Pharah:
                    return "Pharah";
                case Heroes.Reinhardt:
                    return "Reinhardt";
                case Heroes.Mercy:
                    return "Mercy";
                case Heroes.Torbjorn:
                    return "Torbjörn";
                case Heroes.Hanzo:
                    return "Hanzo";
                case Heroes.Winston:
                    return "Winston";
                case Heroes.Zenyatta:
                    return "Zenyatta";
                case Heroes.Bastion:
                    return "Bastion";
                case Heroes.Symmetra:
                    return "Symmetra";
                case Heroes.Zarya:
                    return "Zarya";
                case Heroes.Cassidy:
                    return "Cassidy";
                case Heroes.Soldier76:
                    return "Soldier: 76";
                case Heroes.Lucio:
                    return "Lúcio";
                case Heroes.Roadhog:
                    return "Roadhog";
                case Heroes.Junkrat:
                    return "Junkrat";
                case Heroes.DVa:
                    return "D.Va";
                case Heroes.Mei:
                    return "Mei";
                case Heroes.Genji:
                    return "Genji";
                case Heroes.Ana:
                    return "Ana";
                case Heroes.Sombra:
                    return "Sombra";
                case Heroes.Orisa:
                    return "Orisa";
                case Heroes.Doomfist:
                    return "Doomfist";
                case Heroes.Moira:
                    return "Moira";
                case Heroes.Brigitte:
                    return "Brigitte";
                case Heroes.Wrecking_Ball:
                    return "Wrecking Ball";
                case Heroes.Ashe:
                    return "Ashe";
                case Heroes.Baptiste:
                    return "Baptiste";
                case Heroes.Sigma:
                    return "Sigma";
                case Heroes.Echo:
                    return "Echo";
                case Heroes.Sojourn:
                    return "Sojourn";
                case Heroes.Junker_Queen:
                    return "Junker Queen";
                case Heroes.Kiriko:
                    return "Kiriko";
                case Heroes.Ramattra:
                    return "Ramattra";
                case Heroes.Lifeweaver:
                    return "Lifeweaver";
                case Heroes.Illari:
                    return "Illari";
                case Heroes.Mauga:
                    return "Mauga";
                case Heroes.Venture:
                    return "Venture";
                case Heroes.Juno:
                    return "Juno";
                case Heroes.Hazard:
                    return "Hazard";
                case Heroes.Freja:
                    return "Freja";
                case Heroes.Wuyang:
                    return "Wuyang";
                case Heroes.Vendetta:
                    return "Vendetta";
                case Heroes.Domina:
                    return "Domina";
                case Heroes.Emre:
                    return "Emre";
                case Heroes.Mizuki:
                    return "Mizuki";
                case Heroes.Anran:
                    return "Anran";
                case Heroes.Jetpack_Cat:
                    return "Jetpack Cat";
                default:
                    return "HERO NOT FOUND";
            }
        }
    }
}