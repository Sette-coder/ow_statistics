using System;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MatchLoader : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _mapDropdown;
    [SerializeField] private TMP_InputField _seasonInputField;
    [SerializeField] private TMP_Dropdown _rankDropDown;
    [SerializeField] private TMP_Dropdown _rankDivisionDropDown;
    [SerializeField] private TMP_InputField _rankPercentageInputField;
    [SerializeField] private TMP_Dropdown _matchResultDropdown;
    [SerializeField] private TMP_Dropdown _hero1DropDown;
    [SerializeField] private TMP_Dropdown _hero2DropDown;
    [SerializeField] private TMP_Dropdown _hero3DropDown;
    [SerializeField] private TMP_Dropdown _teamBan1DropDown;
    [SerializeField] private TMP_Dropdown _teamBan2DropDown;
    [SerializeField] private TMP_Dropdown _enemyTeamBan1DropDown;
    [SerializeField] private TMP_Dropdown _enemyTeamBan2DropDown;
    [SerializeField] private TMP_InputField _teamNotes;
    [SerializeField] private TMP_InputField _enemyTeamNotes;
    [SerializeField] private Button _submitDataButton;

    private void Awake()
    {
        SetupDropDown();
        _submitDataButton.onClick.AddListener(SubmitData);
    }

    void SetupDropDown()
    {
        List<TMP_Dropdown.OptionData> mapOptions = new List<TMP_Dropdown.OptionData>();
        foreach (Maps map in Enum.GetValues(typeof(Maps)))
        {
            string mapName = map.ToString();
            // if (mapName.Contains('_'))
            //     mapName = mapName.Replace('_', ' ');
            mapOptions.Add(new TMP_Dropdown.OptionData(mapName));
        }

        List<TMP_Dropdown.OptionData> heroOptions = new List<TMP_Dropdown.OptionData>();
        foreach (Heroes hero in Enum.GetValues(typeof(Heroes)))
        {
            string heroName = hero.ToString();
            // if (heroName.Contains('_'))
            //     heroName = heroName.Replace('_', ' ');

            heroOptions.Add(new TMP_Dropdown.OptionData(heroName));
        }

        List<TMP_Dropdown.OptionData> rankOptions = new List<TMP_Dropdown.OptionData>();
        foreach (Ranks rank in Enum.GetValues(typeof(Ranks)))
        {
            rankOptions.Add(new TMP_Dropdown.OptionData(rank.ToString()));
        }

        List<TMP_Dropdown.OptionData> matchResultOptions = new List<TMP_Dropdown.OptionData>();
        foreach (MatchResult matchResult in Enum.GetValues(typeof(MatchResult)))
        {
            matchResultOptions.Add(new TMP_Dropdown.OptionData(matchResult.ToString()));
        }

        _mapDropdown.ClearOptions();
        _mapDropdown.AddOptions(mapOptions);

        _rankDropDown.ClearOptions();
        _rankDropDown.AddOptions(rankOptions);

        _matchResultDropdown.ClearOptions();
        _matchResultDropdown.AddOptions(matchResultOptions);

        _hero1DropDown.ClearOptions();
        _hero1DropDown.AddOptions(heroOptions);

        _hero2DropDown.ClearOptions();
        _hero2DropDown.AddOptions(heroOptions);

        _hero3DropDown.ClearOptions();
        _hero3DropDown.AddOptions(heroOptions);

        _teamBan1DropDown.ClearOptions();
        _teamBan1DropDown.AddOptions(heroOptions);

        _teamBan2DropDown.ClearOptions();
        _teamBan2DropDown.AddOptions(heroOptions);

        _enemyTeamBan1DropDown.ClearOptions();
        _enemyTeamBan1DropDown.AddOptions(heroOptions);

        _enemyTeamBan2DropDown.ClearOptions();
        _enemyTeamBan2DropDown.AddOptions(heroOptions);
    }

    private void OnEnable()
    {
        ResetDefault();
    }

    void ResetDefault()
    {
        _mapDropdown.value = 0;
        _seasonInputField.text = "";
        _rankDropDown.value = 0;
        _rankDivisionDropDown.value = 0;
        _rankPercentageInputField.text = "";
        _matchResultDropdown.value = 0;
        _hero1DropDown.value = 0;
        _hero2DropDown.value = 0;
        _hero3DropDown.value = 0;
        _teamBan1DropDown.value = 0;
        _teamBan2DropDown.value = 0;
        _enemyTeamBan1DropDown.value = 0;
        _enemyTeamBan2DropDown.value = 0;
        _teamNotes.text = "";
        _enemyTeamNotes.text = "";
    }

    async void SubmitData()
    {
        if (string.IsNullOrWhiteSpace(_seasonInputField.text))
        {
            UiManager.Instance.OpenPopUp(PopUpType.Error, "Season Missing", "Season Field cannot be empty");
            return;
        }
        
        if (string.IsNullOrWhiteSpace(_rankPercentageInputField.text))
        {
            UiManager.Instance.OpenPopUp(PopUpType.Error, "Rank Percentage", "Rank Percentage cannot be empty");
            return;
        }

        if (DoubleHeroSettedCheck())
        {
            UiManager.Instance.OpenPopUp(PopUpType.Error, "Hero", "Cannot input the same hero twice");
            return;
        }
        
        if (_hero1DropDown.value == 0)
        {
            UiManager.Instance.OpenPopUp(PopUpType.Error, "Hero 1", "Hero 1 Cannot be None");
            return;
        }

        if (HeroBanCheck(0))
        {
            UiManager.Instance.OpenPopUp(PopUpType.Error, "Ban Missing", "A hero ban is missing");
            return;
        }

        if (HeroBanCheck(_hero1DropDown.value))
        {
            UiManager.Instance.OpenPopUp(PopUpType.Error, "Hero 1", "You cannot be playing a banned hero");
            return;
        }

        if (HeroBanCheck(_hero2DropDown.value))
        {
            UiManager.Instance.OpenPopUp(PopUpType.Error, "Hero 2", "You cannot be playing a banned hero");
            return;
        }

        if (HeroBanCheck(_hero3DropDown.value))
        {
            UiManager.Instance.OpenPopUp(PopUpType.Error, "Hero 3", "You cannot be playing a banned hero");
            return;
        }

        if (DoubleBanCheck())
        {
            UiManager.Instance.OpenPopUp(PopUpType.Error, "Hero Ban", "You cannot ban the same hero twice");
            return;
        }


        MatchDataSubmitRequest request = new MatchDataSubmitRequest
        {
            UserEmail = UserDataManager.Instance.GetUserEmail(),
            MapName = _mapDropdown.options[_mapDropdown.value].text,
            Season = _seasonInputField.text,
            Rank = _rankDropDown.options[_rankDropDown.value].text,
            RankDivision = Int32.Parse(_rankDivisionDropDown.options[_rankDivisionDropDown.value].text),
            RankPercentage = Int32.Parse(_rankPercentageInputField.text),
            Hero_1 = _hero1DropDown.options[_hero1DropDown.value].text,
            Hero_2 = _hero2DropDown.options[_hero2DropDown.value].text == "None"
                ? ""
                : _hero2DropDown.options[_hero2DropDown.value].text,
            Hero_3 = _hero3DropDown.options[_hero3DropDown.value].text == "None"
                ? ""
                : _hero3DropDown.options[_hero3DropDown.value].text,
            MatchResult = _matchResultDropdown.options[_matchResultDropdown.value].text,
            TeamBan_1 = _teamBan1DropDown.options[_teamBan1DropDown.value].text,
            TeamBan_2 = _teamBan2DropDown.options[_teamBan2DropDown.value].text,
            EnemyTeamBan_1 = _enemyTeamBan1DropDown.options[_enemyTeamBan1DropDown.value].text,
            EnemyTeamBan_2 = _enemyTeamBan2DropDown.options[_enemyTeamBan2DropDown.value].text,
            TeamNotes = _teamNotes.text,
            EnemyTeamNotes = _enemyTeamNotes.text
        };

        var response = await ApiClient.Instance.SendMatchData(request);

        if (!response.ok)
        {
            UiManager.Instance.OpenPopUp(PopUpType.Error, "Error", response.ResponseMessage);
        }
        else
        {
            UiManager.Instance.OpenPopUp(PopUpType.Success, "Success", response.ResponseMessage);
        }
    }

    bool DoubleHeroSettedCheck()
    {
        return _hero1DropDown.value == _hero2DropDown.value ||
               _hero1DropDown.value == _hero3DropDown.value ||
               _hero2DropDown.value == _hero3DropDown.value;
    }
    
    bool HeroBanCheck(int heroValue)
    {
        return heroValue == _teamBan1DropDown.value ||
               heroValue == _teamBan2DropDown.value ||
               heroValue == _enemyTeamBan1DropDown.value ||
               heroValue == _enemyTeamBan2DropDown.value;
    }

    bool DoubleBanCheck()
    {
        return
            (_teamBan1DropDown.value == _teamBan2DropDown.value ||
             _teamBan1DropDown.value == _enemyTeamBan1DropDown.value ||
             _teamBan1DropDown.value == _enemyTeamBan2DropDown.value) ||
            (_teamBan2DropDown.value == _teamBan1DropDown.value ||
             _teamBan2DropDown.value == _enemyTeamBan1DropDown.value ||
             _teamBan2DropDown.value == _enemyTeamBan2DropDown.value) ||
            (_enemyTeamBan1DropDown.value == _teamBan1DropDown.value ||
             _enemyTeamBan1DropDown.value == _teamBan2DropDown.value ||
             _enemyTeamBan1DropDown.value == _enemyTeamBan2DropDown.value) ||
            (_enemyTeamBan2DropDown.value == _teamBan1DropDown.value ||
             _enemyTeamBan2DropDown.value == _teamBan2DropDown.value ||
             _enemyTeamBan2DropDown.value == _enemyTeamBan1DropDown.value);
    }
}