using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningXp : MonoBehaviour
{
    public Text Xp;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.manager.skillXp.Length > 0) {
            Xp.text = (GameManager.manager.skillXp[0]).ToString();
        }
    }
}
