using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningLevel : MonoBehaviour
{

    public Text Level;
    public GameManager _GameManager;

    // Update is called once per frame
    void Update()
    {
        Level.text = (_GameManager.skillLevel[0]).ToString();
    }
}
