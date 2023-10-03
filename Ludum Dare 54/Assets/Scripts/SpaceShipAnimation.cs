using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShipAnimation : MonoBehaviour
{
    
    void Start()
    {
        Sequence pickUp = DOTween.Sequence();
        
        pickUp
            .Append(transform.DOMove(PlayerController.instance.transform.position, 2f)).SetEase(Ease.OutQuad)
            .AppendInterval(0.2f)
            .Append(PlayerController.instance.GetComponentInChildren<SpriteRenderer>().DOFade(0, 0.5f))
            .Append(transform.DOMove((PlayerController.instance.transform.position + Vector3.right * 14), 2f))
            .SetEase(Ease.InQuad)
            .OnComplete((
                () =>
                {
                    SceneManager.LoadScene("GameOver");
                }));
    }
    
    void Update()
    {
        
    }
}
