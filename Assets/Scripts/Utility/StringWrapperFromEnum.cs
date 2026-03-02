using System.Collections.Generic;

namespace Tha7.Utility
{

    public static class FromDatabaseWrapper
    {
        public static int GetMapIdFromName(string mapName)
        {
            var maps = UserDataManager.Instance.GetMapList();
            int mapId = maps.Find(map => map.Name == mapName).Id;
            
            return mapId;
        }
        
        public static string GetMapNameFromId(int mapId)
        {
            var maps = UserDataManager.Instance.GetMapList();
            string mapName = maps.Find(map => map.Id == mapId).Name;
            
            return mapName;
        }
        
        public static int GetHeroIdFromName(string heroName)
        {
            var heroes = UserDataManager.Instance.GetHeroesList();
            
            int heroId = heroes.Find(hero => hero.Name == heroName).Id;
            
            return heroId;
        }
        
        public static string GetHeroNameFromId(int? heroId)
        {
            if (heroId == null ||  heroId == -1)
            {
                return "None";
            }
            var heroes = UserDataManager.Instance.GetHeroesList();
            
            string heroName = heroes.Find(hero => hero.Id == heroId).Name;
            
            return heroName;
        }
    }
}