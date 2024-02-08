using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GinjaShopEnvironment : MonoBehaviour
{
    [SerializeField] private Animator[] _allAnims;

    [SerializeField] private CharacterPlanes _ginjaPlane;

    private PlayerTeam _pTeam;
    private void Start()
    {
        gameObject.SetActive(false);
        _allAnims[Random.Range(0, _allAnims.Length)].SetBool("talking", true);
    }

    public void OpenShop(Texture2D ginjatex = default)
    {
        _ginjaPlane.gameObject.SetActive(false);
        if (ginjatex != null)
        {
            _ginjaPlane.ApplyTextureToMeshes(ginjatex);
            _ginjaPlane.gameObject.SetActive(true);
        }

        gameObject.SetActive(true);
    }
    public void CloseShop()
    {
        gameObject.SetActive(false);
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
