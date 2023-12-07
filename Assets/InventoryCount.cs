using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCount : MonoBehaviour
{

    public Text Count;
    public GameManager _GameManager;

    // Update is called once per frame
    void Update()
    {
            Count.text = (_GameManager.totalPlayerResources).ToString();
    }
}
