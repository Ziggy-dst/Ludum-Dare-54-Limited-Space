using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpaceShipAnimation : MonoBehaviour
{
    
    void Start()
    {
        Sequence pickUp = DOTween.Sequence();
        
        pickUp
            .AppendInterval(1f)
            .Append(transform.DOMove(PlayerController.instance.transform.position, 2f))
            .AppendInterval(0.5f)
            .Append(PlayerController.instance.GetComponentInChildren<SpriteRenderer>().DOFade(0, 0.5f))
            .Append(transform.DOMove((PlayerController.instance.transform.position + Vector3.right * 20), 2f));
    }
    
    void Update()
    {
        
    }
}
