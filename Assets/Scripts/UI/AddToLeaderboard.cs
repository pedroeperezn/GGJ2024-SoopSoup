using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddToLeaderboard : MonoBehaviour
{
    [SerializeField] private Leaderboard leaderboard;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject leaderboardUIObject;

    public static string UserName = "";
    public static float Score = 0;

    private int _limit = 20;

    async void Start()
    {
        string userName = UserName;
        int limit = _limit;

        // make an instance of the leaderboard and display the contents, limit 20
        LeaderBoardPlace[] leaderboardData = await leaderboard.GetScores(LeaderBoardIds.LeaderBoards[4]);
        UpdateLeaderboardUI(leaderboardData);
    }

    void UpdateLeaderboardUI(LeaderBoardPlace[] data)
    {
        AddToLeaderboard uiReference = FindObjectOfType<AddToLeaderboard>();

        for (int i = 0; i < data.Length; ++i)
        {
            string userName = TruncatePlayerName(data[i].Name, '#');
            Add(data[i].Rank, userName, data[i].Score.ToString());
        }
    }

    // adds a new instance of the UI
    public void Add(int placement, string userName, string time)
    {
        Debug.Log("Entered second function");
        GameObject newPrefabInstance = Instantiate(prefab, transform.position, transform.rotation);
        newPrefabInstance.transform.parent = leaderboardUIObject.transform;
        newPrefabInstance.transform.localScale= Vector3.one;

        newPrefabInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = placement.ToString() + ")";
        newPrefabInstance.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = userName;
        newPrefabInstance.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = time;

        leaderboardUIObject.GetComponent<DynamicScrollAdjuster>().CalculateHeightOfArea();
    }

    //I need to clean this up. It's from the Authorize.cs script BUT i wasn't sure how to call it and use it. I will clean it later.
    private string TruncatePlayerName(string playerName, char delimiter)
    {
        int delimiterIndex = playerName.IndexOf(delimiter);
        string cleanName = delimiterIndex > -1 ? playerName.Substring(0, delimiterIndex) : playerName;

        // Truncate the name to a maximum of 10 characters and append an ellipsis if it was longer
        const int maxLength = 10;
        if (cleanName.Length > maxLength)
        {
            cleanName = cleanName.Substring(0, maxLength) + "...";
        }

        return cleanName;
    }
}
