using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{

    public GameObject MainUI;
    public GameObject ResourcesUI;
    public List<GameObject> InventorySlots;
    private Dictionary<int, Sprite> resourceTypeIdToSprite;

    void Start()
    {
        resourceTypeIdToSprite = new Dictionary<int, Sprite>() {
            {1, Resources.Load<Sprite>("Sprites/ore_copper")},
            {2, Resources.Load<Sprite>("Sprites/ore_tin")},
            {3, Resources.Load<Sprite>("Sprites/ore_iron")}
        };
    }

    public void ToggleResources()
    {
        ResourcesUI.SetActive(!ResourcesUI.activeSelf);
    }

    void Update() {
        int[] playerResources = GameManager.manager.playerResources;
        int totalResources = 0;
        for (int i = 0; i < playerResources.Length; i++)
        {
            for (int j = 0; j < playerResources[i]; j++)
            {
                InventorySlots[totalResources].SetActive(true);
                InventorySlots[totalResources].GetComponent<Image>().sprite = resourceTypeIdToSprite[i + 1];
                totalResources++;
            }
        }
        for (int i = totalResources; i < InventorySlots.Count; i++)
        {
            InventorySlots[i].SetActive(false);
        }
    }
}
