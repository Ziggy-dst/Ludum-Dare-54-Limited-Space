using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private Camera spaceCam;

    public float heartPickSize;

    public GameObject[] heartIcons;
    // Start is called before the first frame update
    void Start()
    {
        spaceCam = GameObject.Find("Space Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float mousePosX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        float mousePosY = Camera.main.ScreenToViewportPoint(Input.mousePosition).y;
        Vector2 mousePos = new Vector2(mousePosX, mousePosY);

        float heartPosX = spaceCam.WorldToViewportPoint(transform.position).x;
        float heartPosY = spaceCam.WorldToViewportPoint(transform.position).y;
        Vector2 heartPos = new Vector2(heartPosX, heartPosY);
        
        // print((heartPos-mousePos).magnitude);


        if ((heartPos - mousePos).magnitude < heartPickSize)  
        {
            if (Input.GetMouseButtonDown(0))
            {
                print("Picked Up!");

                // instantiate heart icon
                Instantiate(heartIcons[Random.Range(0, heartIcons.Length - 1)], heartPos, Quaternion.identity);
            }
        }
    }
}
