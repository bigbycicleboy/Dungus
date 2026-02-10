using UnityEngine;

[CreateAssetMenu(menuName = "Dongus/Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Weapon
    }

    public ItemType itemType;
    public string itemName;
    public GameObject itemPrefab;
}