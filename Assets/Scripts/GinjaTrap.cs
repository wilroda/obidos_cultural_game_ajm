using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GinjaTrap : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _ginjaSurprise;
    [SerializeField] private GinjaProfile[] _possibleGinjas;


    [SerializeField] private CharacterPlanes _planes;

    [Space]
    [SerializeField] private AudioSource _doorSource;
    [SerializeField] private AudioClip _doorOpenSound;

    private GinjaShopEnvironment _ginjaShop;

    private JamController _controller;
    private Transform _lookAtTarget;

    private bool _visited;

    private void Start()
    {
        _ginjaShop = FindObjectOfType<GinjaShopEnvironment>(true);
    }
    public void PlayerEnteredCollider()
    {
        if (_visited)
        {
            return;
        }
        GinjaProfile chosenP = _possibleGinjas[Random.Range(0, _possibleGinjas.Length)];
        Texture2D chosenTex = chosenP.Texture;
        _planes.ApplyTextureToMeshes(chosenTex);

        _anim.SetBool("opened", true);
        _anim.SetTrigger("ginjaSurprise");
        _controller = FindObjectOfType<JamController>();
        _lookAtTarget = _controller.transform;

        CoroutineHelper.PerformAfterSeconds(0.2f, () =>
        {
            _controller.Suppress();
        });
        CoroutineHelper.PerformAfterSeconds(1.5f, () =>
        {
            _ginjaShop.OpenShop(chosenTex);
        });
        
        // CoroutineHelper.PerformAfterSeconds(3.5f, () =>
        // {
        //     _ginjaShop.CloseShop();
        //     _controller.Team.AddRandomFollower();
        //     _controller.Unsupress();
        // });

        _visited = true;

        _doorSource.PlayOneShot(_doorOpenSound);

        CoroutineHelper.PerformAfterSeconds(0.35f, () => _doorSource.PlayOneShot(chosenP.Sound));
    }
    public void PlayerExitedCollider()
    {
        _anim.SetBool("opened", false);
        //_anim.SetTrigger("ginjaGoAway");
    }
    private void LateUpdate()
    {
        if (_lookAtTarget != null)
        {
            _ginjaSurprise.LookAt(_lookAtTarget.position + Vector3.up);
        }
    }
}
