using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCount : MonoBehaviour 
{
    private void Start()
    {
        print(transform.childCount);
    }
}
