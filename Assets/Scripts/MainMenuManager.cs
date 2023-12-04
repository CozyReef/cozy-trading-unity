using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ThirdwebManager.Instance.Initialize("avalanche");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
