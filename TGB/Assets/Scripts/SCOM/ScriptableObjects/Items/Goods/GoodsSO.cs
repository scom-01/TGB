using SCOM;
using UnityEngine;

[CreateAssetMenu(fileName = "newGoodsSO", menuName = "Data/GoodsData")]
public class GoodsSO : ScriptableObject
{
    /// <summary>
    /// 재화 Sprite
    /// </summary>
    [SerializeField] public Sprite GoodsSprite;
    /// <summary>
    /// 재화 습득 사운드클립
    /// </summary>
    [SerializeField] public AudioPrefab EquipSoundClip;
    /// <summary>
    /// 재화 습득 이펙트
    /// </summary>
    [SerializeField] public GameObject EquipEffect;
    /// <summary>
    /// 재화의 Circle Radius
    /// </summary>
    [SerializeField] public float CircleSize;
}
