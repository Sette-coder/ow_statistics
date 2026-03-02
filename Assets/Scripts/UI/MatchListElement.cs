using TMPro;
using UnityEngine;
using static Tha7.Utility.FromDatabaseWrapper;

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
    
    private MatchData _matchData;
    
    public void InitializeElements(MatchData matchResponse)
    {
        _matchData = matchResponse;
        
        _matchResultText.text = matchResponse.MatchResult;
        _mapNameText.text = matchResponse.Map.Name;
        _seasonText.text = matchResponse.Season;
        _rankText.text = $"{matchResponse.Rank} {matchResponse.RankDivision} - {matchResponse.RankPercentage}%";
        _hero1Text.text = matchResponse.Hero1.Name;
        _hero2Text.text = "";
        if (matchResponse.Hero2 != null)
        {
            _hero2Text.text = matchResponse.Hero2.Name;
        }
        _hero3Text.text = "";
        if (matchResponse.Hero3 != null)
        {
            _hero3Text.text = matchResponse.Hero3.Name;
        }
        _teamBan1Text.text = matchResponse.TeamBan1.Name;
        _teamBan2Text.text = matchResponse.TeamBan2.Name;
        _enemyTeamBan1Text.text = matchResponse.EnemyTeamBan1.Name;
        _enemyTeamBan2Text.text = matchResponse.EnemyTeamBan2.Name;
        _teamNotesText.text = matchResponse.TeamNotes;
        _enemyTeamNotesText.text = matchResponse.EnemyTeamNotes;
    }
}