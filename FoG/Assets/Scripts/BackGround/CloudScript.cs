using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    private float speed = 6f;
    private float endPosY = -60f;
    public void StartFloating(float speed, float endPos)
    {
        this.speed = speed;
        endPosY = endPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);

        if (transform.position.y <= endPosY)
        {
            Destroy(gameObject);
        }
    }
}
