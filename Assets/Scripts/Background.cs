using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float moveSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime; // 배경 아래로 이동
        if(transform.position.y < -10) transform.position += new Vector3(0, 20f, 0);
    }
}
