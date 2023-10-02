using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    private DraggableUI draggableUI;

    public List<AudioClip> cutSounds;

    public GameObject[] hearts;

    private bool isCutting = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        draggableUI = GetComponent<DraggableUI>();
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
                if (Input.GetKeyDown(knifeKey) && !draggableUI.isDragging && !isCutting)
                {
                    isCutting = true;
                    
                    GameObject body = PlayerController.instance.nearestBody;
                    
                    spriteRenderer.sprite = clicked;
                    Invoke("ResetCrosshair", 3f);
                
                    //a function that generate a heart into the bag

                    Sequence lineSequence = DOTween.Sequence();

                    lineSequence
                        .Append(PlayerController.instance.playerLine.DOText(
                            PlayerController.instance.lines[Random.Range(0, PlayerController.instance.lines.Count)],
                            1f))
                        .AppendInterval(2)
                        .Append(PlayerController.instance.playerLine.DOText("", 0f));
                    
                   
                    AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                    audioSource.clip = cutSounds[Random.Range(0, cutSounds.Count)];
                    audioSource.spatialBlend = 0;
                    audioSource.Play();
                    StartCoroutine(SelfDestroy(audioSource));
                    
                    DropHeart();
                    
                    body.transform.parent.GetComponent<SpriteRenderer>().DOFade(0, 2);
                    body.transform.parent.parent.GetComponent<SpriteRenderer>().DOFade(0, 2).OnComplete(() =>
                    {
                        body.transform.parent.parent.DOKill();
                        body.transform.parent.DOKill();
                        Destroy(body.transform.parent.parent.gameObject);
                        isCutting = false;
                    });
                }
            }
        }
    }

    IEnumerator SelfDestroy(AudioSource audioSource)
    {
        yield return new WaitForSeconds(7f);
        Destroy(audioSource);
    }

    void DropHeart()
    {
        Instantiate(hearts[Random.Range(0, hearts.Length-1)], PlayerController.instance.nearestBody.transform.position,
            Quaternion.Euler(0, 0, Random.Range(0, 360f)));
    }

    void ResetCrosshair()
    {
        spriteRenderer.sprite = idle;
    }
}
