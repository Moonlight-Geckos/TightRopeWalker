using UnityEngine;

[CreateAssetMenu(menuName ="Skin Item")]
public class SkinItem : ScriptableObject
{
    public Sprite skinSprite;
    public int price;
    public GameObject skinObject;
    public int skinNumber;
}
