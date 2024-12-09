using System;
using UnityEngine;

public class OnDestroyObject : MonoBehaviour
{    
    public void OnDestroy()
    {
        Destroy(gameObject);
    }   
}
