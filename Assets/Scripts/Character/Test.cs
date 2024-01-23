using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * 100, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
