using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public RectTransform maskTransform;

    public RectTransform renderTextureTransform;

    public Camera knifeCam;

    public Canvas canvas;

    public float heartHitSize;

    public KeyCode knifeKey;

    private SpriteRenderer spriteRenderer;

    public Sprite idle;
    
    public Sprite clicked;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        maskTransform.anchoredPosition = CanvasPositioningExtensions.WorldToCanvasPosition(canvas, transform.position, Camera.main);
        renderTextureTransform.anchoredPosition = -maskTransform.anchoredPosition;
        
        if (PlayerController.instance.nearestBody != null)
        {
            if ((knifeCam.WorldToViewportPoint(PlayerController.instance.nearestBody.transform.position) -
                 Camera.main.WorldToViewportPoint(transform.position)).magnitude <= heartHitSize)
            {
                if (Input.GetKeyDown(knifeKey))
                {
                    spriteRenderer.sprite = clicked;
                    Invoke("ResetCrosshair", 3f);
                
                    //a function that generate a heart into the bag
                    
                    Destroy(PlayerController.instance.nearestBody.transform.parent.parent.gameObject);
                    
                }
            }
        }
    }

    void ResetCrosshair()
    {
        spriteRenderer.sprite = idle;
    }
}
