using TMPro;
using UnityEngine;

public class MatchListElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _matchResultText;
    [SerializeField] private TextMeshProUGUI _mapNameText;
    [SerializeField] private TextMeshProUGUI _seasonText;
    [SerializeField] private TextMeshProUGUI _rankText;
    [SerializeField] private TextMeshProUGUI _hero1Text;
    [SerializeField] private TextMeshProUGUI _hero2Text;
    [SerializeField] private TextMeshProUGUI _hero3Text;
    [SerializeField] private TextMeshProUGUI _teamBan1Text;
    [SerializeField] private TextMeshProUGUI _teamBan2Text;
    [SerializeField] private TextMeshProUGUI _enemyTeamBan1Text;
    [SerializeField] private TextMeshProUGUI _enemyTeamBan2Text;
    [SerializeField] private TextMeshProUGUI _teamNotesText;
    [SerializeField] private TextMeshProUGUI _enemyTeamNotesText;

    public void InitializeElements(MatchData matchResponse)
    {
        _matchResultText.text = matchResponse.MatchResult;
        _mapNameText.text = Tha7.Utility.StringWrapper.ToMapName(matchResponse.MapName);
        _seasonText.text = matchResponse.Season;
        _rankText.text = $"{matchResponse.Rank} {matchResponse.RankDivision} - {matchResponse.RankPercentage}%";
        _hero1Text.text = Tha7.Utility.StringWrapper.ToHeroName(matchResponse.Hero_1);
        _hero2Text.text = Tha7.Utility.StringWrapper.ToHeroName(matchResponse.Hero_2);
        _hero3Text.text = Tha7.Utility.StringWrapper.ToHeroName(matchResponse.Hero_3);
        _teamBan1Text.text = Tha7.Utility.StringWrapper.ToHeroName(matchResponse.TeamBan_1);
        _teamBan2Text.text = Tha7.Utility.StringWrapper.ToHeroName(matchResponse.TeamBan_2);
        _enemyTeamBan1Text.text = Tha7.Utility.StringWrapper.ToHeroName(matchResponse.EnemyTeamBan_1);
        _enemyTeamBan2Text.text = Tha7.Utility.StringWrapper.ToHeroName(matchResponse.EnemyTeamBan_2);
        _teamNotesText.text = matchResponse.TeamNotes;
        _enemyTeamNotesText.text = matchResponse.EnemyTeamNotes;
    }
}