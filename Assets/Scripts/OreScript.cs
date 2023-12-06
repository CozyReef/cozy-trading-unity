using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;

public class Ore : MonoBehaviour
{
    public bool isCollected;
    public int resourceId;
    Rigidbody2D _rb;
    GameManager gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        isCollected = false;
        _rb = gameObject.GetComponent<Rigidbody2D>();

        gameManagerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log($"clicked on ore {resourceId}");
        if (!isCollected)
        {
        gameManagerScript.Collect(resourceId);
        // TODO: we have to wait and check if this function executed without errors
        //       now the resource is set to collected even if it is not collected
        SetCollected();
        }
        
    }

    public void SetCollected()
    {
        isCollected = true;
        Color oldColor = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, 0.7f);
    }

    // Why collision doesnt work?
    // We need to come near the ORE to be able to collect it 
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

    }


}
