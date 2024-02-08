using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Ginja Profile", menuName = "Obidos/GinjaProfile", order = 0)]
public class GinjaProfile : ScriptableObject
{
    [field:SerializeField][field:ShowAssetPreview] public Texture2D Texture;
    [field:SerializeField] public AudioClip Sound;
}
