using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Sprite buttonIdle;
    public Sprite buttonHover;
    public Sprite buttonPressed;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject.name.Contains("Button")) 
        {
            spriteRenderer.sprite = buttonHover;
            if (Input.GetMouseButton(0))
            {
                spriteRenderer.sprite = buttonPressed;
            }

            if (Input.GetMouseButtonUp(0))
            {
                GameManager.Instance.OnGameWins();
            }
        }
        else
        {
            spriteRenderer.sprite = buttonIdle;
        }
    }
}
