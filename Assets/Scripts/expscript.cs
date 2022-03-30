using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expscript : MonoBehaviour
{
    public GameObject target;

    public void Start()
    {
        Destroy(target, 1.5f);
    }
}
