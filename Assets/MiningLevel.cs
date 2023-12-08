using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningLevel : MonoBehaviour
{

    public Text Level;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.manager.skillLevel.Length > 0) {
            Level.text = (GameManager.manager.skillLevel[0]).ToString();
        }
    }
}
