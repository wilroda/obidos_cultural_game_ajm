using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GinjaShopEnvironment : MonoBehaviour
{
    [SerializeField] private Animator[] _allAnims;
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
}
