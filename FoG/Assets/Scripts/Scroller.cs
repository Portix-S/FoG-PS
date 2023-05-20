using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0f, speed * Time.deltaTime, 0f);

        if (transform.position.y <= -70f)
        {
            transform.position += new Vector3(0f, 2 * transform.localScale.y, 0f);
        }

    }
}
