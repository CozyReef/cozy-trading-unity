using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningXp : MonoBehaviour
{

    public Text Xp;
    public GameManager _GameManager;

    // Update is called once per frame
    void Update()
    {
        Xp.text = (_GameManager.skillXp[0]).ToString();
        Debug.Log("XP:" + _GameManager.skillXp[0]);
    }
}
