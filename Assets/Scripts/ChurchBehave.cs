using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChurchBehave : MonoBehaviour
{
    [SerializeField] private TMP_Text _txt;
    public void FeedPlayerCount()
    {
        int maxPeople = Enum.GetNames(typeof(IRLPeople)).Length;
        _txt.SetText(JamController.Instance.Team.Followers.Count + 1 + "/" + maxPeople);
    }
}
