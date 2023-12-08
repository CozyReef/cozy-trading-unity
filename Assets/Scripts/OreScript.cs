using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Thirdweb;

public class Ore : MonoBehaviour
{
    public enum OreType
    {
        Copper,
        Tin,
        Iron
    }
    private Dictionary<OreType, Sprite> oreTypeToSprite;

    public GameObject requirementsNotMet;
    
    public OreType oreType;
    public int resourceId;
    public bool isCollected = false;

    public Image oreImage;
    public GameObject canvas;
    public Sprite collectedOreSprite;
    public Sprite oreSprite;

    void Start()
    {
        oreTypeToSprite = new Dictionary<OreType, Sprite>() {
            {OreType.Copper, Resources.Load<Sprite>("Sprites/ore_copper")},
            {OreType.Tin, Resources.Load<Sprite>("Sprites/ore_tin")},
            {OreType.Iron, Resources.Load<Sprite>("Sprites/ore_iron")}
        };
        oreImage.sprite = oreTypeToSprite[oreType];
        if (oreType == OreType.Iron) {
            requirementsNotMet.SetActive(true);
        }
    }

    public async void OnClickCollect()
    {
        Debug.Log($"clicked on ore {resourceId}");
        if (!isCollected)
        {
            GameManager.manager.Collect(resourceId);
            canvas.SetActive(false);
        }
        
    }

    public void SetCollected(bool _isCollected)
    {
        isCollected = _isCollected;
        Color oldColor = gameObject.GetComponent<SpriteRenderer>().color;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (isCollected)
        {
        spriteRenderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, 0.7f);
        spriteRenderer.sprite = collectedOreSprite;
        } else
        {
            spriteRenderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, 1f);
            spriteRenderer.sprite = oreSprite;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isCollected)
        {
            canvas.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvas.SetActive(false);
        }
    }
}
