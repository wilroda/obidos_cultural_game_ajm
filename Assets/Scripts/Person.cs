using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Person", menuName = "Obidos/Person", order = 0)]
public class Person : ScriptableObject
{
    [SerializeField] private IRLPeople _person;
    [SerializeField] private Texture2D _faceTexture;

    public void ApplyTextureToMeshes(params Renderer[] renderers)
    {
        foreach (Renderer mr in renderers)
        {
            mr.material.SetTexture("_MainTex", _faceTexture);
            mr.material.mainTexture = _faceTexture;
        }
    }
}

public enum IRLPeople
{
    Wilson,
    Filipe,
    Tomas,
    Joana,

    Nelio,
    Micaela,
    Bruno,
    Conceicao,
    Phil,
    Suzane,
}