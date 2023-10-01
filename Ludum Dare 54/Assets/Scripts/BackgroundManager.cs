using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public RawImage star1;
    public RawImage star2;
    public RawImage star3;

    public float star1Speed;
    public float star2Speed;
    public float star3Speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        star1.uvRect = new Rect(Camera.main.transform.position.x * star1Speed,
            Camera.main.transform.position.y * star1Speed, 1f, 1f);
        
        star2.uvRect = new Rect(Camera.main.transform.position.x * star2Speed,
            Camera.main.transform.position.y * star2Speed, 2f, 2f);
        
        star3.uvRect = new Rect(Camera.main.transform.position.x * star3Speed,
            Camera.main.transform.position.y * star3Speed, 3f, 3f);
    }
}
