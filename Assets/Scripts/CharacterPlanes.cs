using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlanes : MonoBehaviour
{
    [SerializeField] private Renderer[] _rends;

    public void Setup(Person p)
    {
        p.ApplyTextureToMeshes(_rends);
    }

    public void ApplyTextureToMeshes(Texture2D texture)
    {
        foreach (Renderer mr in _rends)
        {
            mr.material.SetTexture("_MainTex", texture);
            mr.material.mainTexture = texture;
        }
    }
}
