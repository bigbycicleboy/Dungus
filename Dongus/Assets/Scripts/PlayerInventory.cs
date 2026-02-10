using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public int gold = 0;
    public Item[] items;
    public TextMeshProUGUI goldText;
    public GameObject inventoryUI;

    public void AddGold(int amount)
    {
        gold += amount;
        goldText.text = "Gold: " + gold.ToString();
    }

    public void ToggleInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }
    }
}