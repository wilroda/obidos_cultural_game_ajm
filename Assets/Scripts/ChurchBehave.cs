using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChurchBehave : MonoBehaviour
{
    [SerializeField] private TMP_Text _txt;
    [SerializeField] private AudioSource _aSrc;
    [SerializeField] private AudioClip _winClip;
    public void FeedPlayerCount()
    {
        int maxPeople = Enum.GetNames(typeof(IRLPeople)).Length;
        _txt.SetText(JamController.Instance.Team.Followers.Count + 1 + "/" + maxPeople);

        if (maxPeople == JamController.Instance.Team.Followers.Count + 1)
        {
            JamController.Instance.Suppress();
            FindObjectOfType<GinjaShopEnvironment>(true).OpenShop(null, true);
            _aSrc.PlayOneShot(_winClip);
        }
    }
}
