using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{

    public GameObject MainUI;
    public GameObject ResourcesUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleResources()
    {
        Debug.Log("togggle");
        ResourcesUI.SetActive(!ResourcesUI.activeSelf);
    }
}
