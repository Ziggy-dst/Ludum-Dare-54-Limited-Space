using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StarSystem : MonoBehaviour
{
    public Transform stream;
    public float streamSpeed;

    public Transform circleInner;
    public float circleInnerSpeed;

    public Transform circleOuter;
    public float circleOuterSpeed;

    public Transform trackInner;
    public float trackInnerSpeed;

    public Transform trackOuter;
    public float trackOuterSpeed;

    public Transform satelliteInner;
    public float satelliteInnerSpeed;

    public Transform satelliteMiddle;
    public float satelliteMiddleSpeed;

    public Transform satelliteOuter;
    public float satelliteOuterSpeed;
    
    void Start()
    {
        stream.DORotate(new Vector3(0, 0, 360), 360 / streamSpeed, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        circleInner.DORotate(new Vector3(0, 0, 360), 360 / circleInnerSpeed, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        circleOuter.DORotate(new Vector3(0, 0, 360), 360 / circleOuterSpeed, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        trackInner.DORotate(new Vector3(0, 0, 360), 360 / trackInnerSpeed, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        trackOuter.DORotate(new Vector3(0, 0, 360), 360 / trackOuterSpeed, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        
        satelliteInner.DORotate(new Vector3(0, 0, 360), 360 / satelliteInnerSpeed, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        satelliteMiddle.DORotate(new Vector3(0, 0, 360), 360 / satelliteMiddleSpeed, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        satelliteOuter.DORotate(new Vector3(0, 0, 360), 360 / satelliteOuterSpeed, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
