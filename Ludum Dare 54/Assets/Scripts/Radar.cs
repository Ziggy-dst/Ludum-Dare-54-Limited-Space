using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public GameObject directionIndicator;
    
    public GameObject bodyIndicator;

    public GameObject dangerIndicator;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void Update()
    {
        UpdateDir();
        UpdateBody();
        UpdateDanger();
    }

    void UpdateDir()
    {
        if(PlayerController.instance.moveDirection != Vector2.zero)
        {
            directionIndicator.transform.rotation =
                Quaternion.LookRotation(Vector3.forward, PlayerController.instance.rb2D.velocity);
        }
    }
    
    void UpdateBody()
    {
        if (PlayerController.instance.nearestBody != null)
            bodyIndicator.transform.rotation = Quaternion.LookRotation(Vector3.forward,
                PlayerController.instance.nearestBody.transform.position -
                PlayerController.instance.transform.position) * Quaternion.Euler(new Vector3(0, 0, -45));

    }
    
    void UpdateDanger()
    {
        if (PlayerController.instance.nearestDanger != null && PlayerController.instance.nearestDangerDistance < 20f) 
        {
            dangerIndicator.transform.rotation = Quaternion.LookRotation(Vector3.forward,
                PlayerController.instance.nearestDanger.transform.position -
                PlayerController.instance.transform.position) * Quaternion.Euler(new Vector3(0, 0, 135));
        }
    }
}
