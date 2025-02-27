using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private float moveSpeed = 10f;

    private float minY = -7f;

    [SerializeField]
    private float hp = 1f;

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed; // 앞은 클래스 내의 변수, 뒤는 함수 인자
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if(transform.position.y < minY) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) // 충돌 감지만, 물리 작용 없음
    {
        if(other.gameObject.tag == "Weapon") { // 미사일과 적이 충돌한 경우
                Weapon weapon = other.gameObject.GetComponent<Weapon>();
                hp -= weapon.damage;
                if(hp <= 0) {
                        if(gameObject.tag == "Boss") GameManager.instance.SetGameOver();
                        Destroy(gameObject);
                        Instantiate(coin, transform.position, Quaternion.identity);
                }
                Destroy(other.gameObject);
        }
    }
}
