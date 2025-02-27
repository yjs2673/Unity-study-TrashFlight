using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] // 유니티에서 직접 플레이어의 스피드를 정할 수 있음
    private float moveSpeed; // 움직이는 속도

    [SerializeField]
    private GameObject[] weapons; // 미사일
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform; // 플레이어 머리 위

    [SerializeField]
    private float shootInterval = 0.05f; // 미사일 간격
    private float lastShotTime = 0f;

    // Update is called once per frame
    void Update() // 계속 호출되는 함수
    {
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");
        // Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0f);
        // transform.position += moveTo * moveSpeed * Time.deltaTime; // 플레이어 이동

        Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        if(Input.GetKey(KeyCode.LeftArrow)) transform.position -= moveTo; 
        else if(Input.GetKey(KeyCode.RightArrow)) transform.position += moveTo;

        // 마우스로 이동
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f); // 범위 정하기
        // transform.position = new Vector3(toX, transform.position.y, transform.position.z);

        if(GameManager.instance.isGameOver == false) Shoot();
    }

    void Shoot()
    {
        if(Time.time - lastShotTime > shootInterval) {
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") {
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Coin") {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void Upgrade() {
        weaponIndex++;
        if(weaponIndex >= weapons.Length) weaponIndex = weapons.Length - 1; 
    }
}
