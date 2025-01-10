using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ChestItemSO : ScriptableObject
{
    public Transform itemTransform;
    public Sprite sprite;
    public string itemName;
}
