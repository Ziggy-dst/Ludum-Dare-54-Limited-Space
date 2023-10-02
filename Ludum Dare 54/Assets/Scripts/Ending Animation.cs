using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingAnimation : MonoBehaviour
{
    public Transform stars;
    public Transform player;
    public Transform particles;
    public Transform cannon;
    public Transform title;
    public TextMeshPro result;
    public TextMeshPro fInstruction;
    public TextMeshPro instruction;
    
    void Start()
    {
        Vector2 playerInitialPos = player.position;
        
        Sequence endingAnimation = DOTween.Sequence();
        endingAnimation
            .Append(player.DOMove(cannon.position, 1f))
            .AppendInterval(0.5f)
            .OnComplete((() =>
            {
                particles.gameObject.SetActive(true);
                Sequence endingAnimation2 = DOTween.Sequence();
                endingAnimation2
                    .Append(cannon.DOShakePosition(1f,0.25F,2))
                    .Insert(0, player.DOMove(playerInitialPos, 1f))
                    .AppendInterval(4)
                    .Append(stars.DOMove(Vector3.down * 15, 10f).SetEase(Ease.Linear))
                    .Insert(5,result.DOText("That was " + GameManager.Instance.currentState + ".",2f));
            }));
        // .Append(title.)

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
