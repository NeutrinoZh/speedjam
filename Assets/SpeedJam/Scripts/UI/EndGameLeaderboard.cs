using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.Services.Leaderboards;
using UnityEngine;
using Zenject;

namespace SpeedJam
{
    public class EndGameLeaderboard : MonoBehaviour
    {
        private GlobalData _globalData;
        
        private List<TextMeshProUGUI> _usernames = new();
        private List<TextMeshProUGUI> _times = new();

        [Inject]
        public void Construct(GlobalData globalData)
        {
            _globalData = globalData;
        }
        
        private void Awake()
        {
            var entries = transform.Find("Entries");
            foreach (Transform entry in entries)
            {
                _usernames.Add(entry.Find("Username").GetComponent<TextMeshProUGUI>());
                _times.Add(entry.Find("Time").GetComponent<TextMeshProUGUI>());
            }
        }
        
        private async void OnEnable()
        {
            try
            {
                var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(
                    _globalData.LeaderboardId,
                    new GetScoresOptions{ Offset = 0, Limit = 10 }
                );

                var results = scoresResponse.Results;
                for (int i = 0; i < results.Count; ++i)
                {
                    var entry = results[i];
                    _usernames[i].text = entry.PlayerName;
                    _times[i].text = TimeSpan.FromMilliseconds(entry.Score).ToString(@"mm\:ss\.ff");
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}