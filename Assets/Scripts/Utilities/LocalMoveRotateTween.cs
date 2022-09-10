using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LocalMoveRotateTween : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 2, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(Ease.Linear);
    }
   
}
