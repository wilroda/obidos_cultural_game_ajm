using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GinjaShopEnvironment : MonoBehaviour
{
    [SerializeField] private Animator[] _allAnims;

    private PlayerTeam _pTeam;
    private void Start()
    {
        gameObject.SetActive(false);
        _allAnims[Random.Range(0, _allAnims.Length)].SetBool("talking", true);
    }

    public void OpenShop()
    {
        gameObject.SetActive(true);
    }
    public void CloseShop()
    {
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        if (_pTeam == default)
        {
            _pTeam = FindObjectOfType<PlayerTeam>();
        }

        _allAnims[0].GetComponent<CharacterPlanes>()?.Setup(_pTeam.Lead);

        for (int i = 1; i < _allAnims.Length; i++)
        {
            Animator a = _allAnims[i];
            if (_pTeam.Followers.Count <= i - 1)
            {
                a.gameObject.SetActive(false);
                continue;
            }

            a.gameObject.SetActive(true);
            a.GetComponent<CharacterPlanes>()?.Setup(_pTeam.Followers[i - 1]);
        }
    }
}
