using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    
    public void DestroyLaser()
    {
        Destroy(GetComponentInParent<RectTransform>().gameObject);
    }
}
