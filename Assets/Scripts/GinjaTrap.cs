using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GinjaTrap : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _ginjaSurprise;
    [SerializeField] private Texture2D[] _possibleGinjasTex;

    [SerializeField] private CharacterPlanes _planes;

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
        Texture2D chosenTex = _possibleGinjasTex[Random.Range(0, _possibleGinjasTex.Length)];
        _planes.ApplyTextureToMeshes(chosenTex);

        _anim.SetBool("opened", true);
        _anim.SetTrigger("ginjaSurprise");
        _controller = FindObjectOfType<JamController>();
        _lookAtTarget = _controller.transform;

        CoroutineHelper.PerformAfterSeconds(0.5f, () =>
        {
            _ginjaShop.OpenShop(chosenTex);
            _controller.Team.AddRandomFollower();
            _controller.Suppress();
        });
        CoroutineHelper.PerformAfterSeconds(3f, () =>
        {
            _ginjaShop.CloseShop();
            _controller.Unsupress();
        });

        _visited = true;
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
