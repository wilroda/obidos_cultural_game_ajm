using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GinjaShopEnvironment : MonoBehaviour
{
    [SerializeField] private Animator[] _allAnims;

    [SerializeField] private CharacterPlanes _ginjaPlane;
    [SerializeField] private AudioSource _ambx;
    [SerializeField] private AudioSource _phrasesSource;
    [SerializeField] private AudioClip[] _phrases;
    [SerializeField] private BarInfo[] _barInfos;
    [SerializeField] private BarInfo _churchInf;
    [Space]
    [SerializeField] private Renderer _bg;
    [SerializeField] private Renderer _fg;

    private PlayerTeam _pTeam;
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenShop(Texture2D ginjatex = default, bool endless = false)
    {
        BarInfo chosenInfo = _barInfos[Random.Range(0, _barInfos.Length)];

        _ginjaPlane.gameObject.SetActive(false);
        if (ginjatex != null)
        {
            _ginjaPlane.ApplyTextureToMeshes(ginjatex);
            _ginjaPlane.gameObject.SetActive(true);
        }

        _bg.material.mainTexture = chosenInfo.BG;
        _fg.material.mainTexture = chosenInfo.FG;

        _fg.gameObject.SetActive(true);
        if (chosenInfo.FG == null)
        {
            _fg.gameObject.SetActive(false);
        }

        if (endless)
        {
            _bg.material.mainTexture = _churchInf.BG;
            _fg.material.mainTexture = _churchInf.FG;
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
            if (endless)
            {
                CoroutineHelper.PerformAfterSeconds(selectedPhrase.length + 8f, () =>
                {
                    string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
                    UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
                });
            }
            else
            {
                CoroutineHelper.PerformAfterSeconds(selectedPhrase.length + 1f, () =>
                {
                    CloseShop();

                    JamController.Instance.Team.AddRandomFollower();
                    JamController.Instance.Unsupress();
                });
            }
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
            if (_pTeam.Followers == null)
            {
                _pTeam.Followers = new List<Person>();
            }
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
