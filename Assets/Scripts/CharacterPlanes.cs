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
}
