using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroAnimation : MonoBehaviour
{
    public Transform player;
    public Transform spaceShip;
    public Transform title;
    public RectTransform instruction;
    
    void Start()
    {
        Sequence introAnimation = DOTween.Sequence();
        introAnimation
            .Append(title.GetComponent<SpriteRenderer>().DOFade(0,0.5f))
            .AppendInterval(0.2f)
            .Append(player.DOMove(spaceShip.position, 1.5f)).SetEase(Ease.Linear)
            .Append(player.GetComponent<SpriteRenderer>().DOFade(0, 1f)).SetEase(Ease.InOutQuad)
            .Append(spaceShip.DOMove(Vector3.up * 10f, 2f)).SetEase(Ease.OutQuad)
            .OnComplete((() =>
            {
                SceneManager.LoadScene("Main");
                GameManager.Instance.ChangeState(GameManager.GameState.GamePlay);
            }));
    }


    void Update()
    {
        instruction.DOKill();
        instruction.gameObject.SetActive(false);
    }
}
