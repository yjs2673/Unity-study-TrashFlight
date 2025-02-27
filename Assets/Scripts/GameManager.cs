using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text; // 텍스트 패널 업데이트를 위한 변수, TMPro 헤더라인에 쓰기

    [SerializeField]
    private GameObject gameOverPanel;

    private int coin = 0;

    [HideInInspector] // inspector에서 숨겨준다
    public bool isGameOver = false;

    void Awake() { // start보다 빨리 호출되는 매소드
        if(instance == null) instance = this;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
	        Application.Quit(); //게임 앱 종료
        }
    }

    public void IncreaseCoin() {
        coin++;
        text.SetText(coin.ToString());

        if(coin % 20 == 0) { // 20개마다 무기 강화
           Player player = FindObjectOfType<Player>();
           if(player != null) player.Upgrade(); 
        }
    }

    public void SetGameOver()
    {
        isGameOver = true;

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if(enemySpawner != null) enemySpawner.StopEnemyRoutine();

        Invoke("ShowGmaeOverPanel", 1f); // 특정 시간이 지나고 메소드를 실행시킴
    }

    void ShowGmaeOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
