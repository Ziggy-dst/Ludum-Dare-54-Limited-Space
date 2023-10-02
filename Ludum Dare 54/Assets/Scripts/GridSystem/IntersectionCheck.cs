using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionCheck : MonoBehaviour
{
    public int maxGrids;
    public Collider2D[] singleGridColliders;

    [HideInInspector]
    public int currentActiveColliders = 0;

    private void Start()
    {
        singleGridColliders = new Collider2D[maxGrids];
    }
}
