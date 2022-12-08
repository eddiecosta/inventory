using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerGFXTransform : MonoBehaviour
{
    private Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Transform>();    
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0.0f, parent.rotation.y, 0.0f);
    }
}
