using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GinjaShopEnvironment : MonoBehaviour
{
    [SerializeField] private Animator[] _allAnims;

    [SerializeField] private CharacterPlanes _ginjaPlane;
    [SerializeField] private AudioSource _ambx;
    [SerializeField] private AudioSource _phrasesSource;
    [SerializeField] private AudioClip[] _phrases;

    private PlayerTeam _pTeam;
    private void Start()
    {
        gameObject.SetActive(false);
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
        _ambx.Play();

        AudioClip selectedPhrase = _phrases[Random.Range(0, _phrases.Length)];
        Animator selectedAnimator;

        do
        {
            selectedAnimator = _allAnims[Random.Range(0, _allAnims.Length)];
        } while (!selectedAnimator.gameObject.activeSelf);

        CoroutineHelper.PerformAfterSeconds(1f, () =>
        {
            selectedAnimator.SetBool("talking", true);
            _phrasesSource.PlayOneShot(selectedPhrase);

            CoroutineHelper.PerformAfterSeconds(selectedPhrase.length, () =>
            {
                selectedAnimator.SetBool("talking", false);
            });
            CoroutineHelper.PerformAfterSeconds(selectedPhrase.length + 1f, () =>
            {
                CloseShop();

                JamController.Instance.Team.AddRandomFollower();
                JamController.Instance.Unsupress();
            });
        });
    }
    public void CloseShop()
    {
        gameObject.SetActive(false);
        _ambx.Pause();
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
