using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "BarInfo", menuName = "Obidos/BarInfo", order = 0)]
public class BarInfo : ScriptableObject
{
    [field: SerializeField][field: ShowAssetPreview] public Texture2D BG { get; set; }
    [field: SerializeField][field: ShowAssetPreview] public Texture2D FG { get; set; }
}