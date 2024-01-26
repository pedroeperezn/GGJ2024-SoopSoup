using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddToLeaderboard : MonoBehaviour
{
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject prefab;

    public void AddToLeaderboardTest()
    {
        Debug.Log("Entered initial function");
        Add(1, "123qwe", Time.time.ToString());
    }

    public void Add(int placement, string userName, string time)
    {
        Debug.Log("Entered second function");
        GameObject newPrefabInstance = Instantiate(prefab, transform.position, transform.rotation);
        newPrefabInstance.transform.parent = leaderboard.transform;
        newPrefabInstance.transform.localScale= Vector3.one;

        newPrefabInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = placement.ToString() + ")";
        newPrefabInstance.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = userName;
        newPrefabInstance.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = time;

        leaderboard.GetComponent<DynamicScrollAdjuster>().CalculateHeightOfArea();
    }
}
