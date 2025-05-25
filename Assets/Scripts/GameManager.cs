using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 Game Manager
//단 하나만 존재
public class GameManager : MonoBehaviour
{
    public static GameManager instance; //싱글톤을 할당할 전역 변수
    GoblinData goblinData;
    PlayerData playerData;

    public bool isGameOver = false; //게임 오버 상태인가?
    bool isTimeStopped = false; //게임 시간이 멈췄는가?
    bool isPaused = false; //게임 일시중지 상태인가?

    void Awake()
    {
        DefineIsThereSingleTon(); //싱글톤 판단용 메서드 실행
        ReferenceReset(); //스크립트 참조변수 초기화
    }

    void ReferenceReset() //스크립트 참조변수 초기화
    {
        goblinData = GetComponent<GoblinData>();
        playerData = GetComponent<PlayerData>();
    }

    //게임 시작과 동시에 싱글톤 구성
    void DefineIsThereSingleTon()
    {
        //싱글톤 변수가 비어있는가?
        if (instance == null) instance = this; //instance가 비어있다면 그곳에 자기 자신 할당
        else //instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우
        {
            Debug.LogWarning("씬에 두 개 이상의 GameManager가 존재한다!");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        SeePlayerHP(); //플레이어 HP 점검하고 게임오버 판정
    }

    public void StopTime() //시간 멈추는 메서드
    {
        if (Time.timeScale != 0f) Time.timeScale = 0f; //시간 멈춤
        if (!isTimeStopped) isTimeStopped = true; //시간이 멈췄습니다
        Debug.Log("시간이 멈춤");
    }

    public void ResumeTime() //시간 다시 흐르게 하는 함수
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f; //시간이 다시 정상으로
        if (isTimeStopped) isTimeStopped = false; //시간이 다시 흐릅니다
    }

    void GameOver() //게임오버 판정 받으면
    {
        if (isGameOver)
        {
            StopTime(); //시간 멈춤
        }

        // ### 게임오버 후 씬 넘어가는 코드 여기 적기
    }

    void SeePlayerHP() //플레이어 HP 점검
    {
        int playerHP = playerData.currentPlayerHP; //플레이어 현재 HP
        if (playerHP > 0) return; //현재 플레이어 HP가 0보다 크면 건너뜀

        //플레이어 HP가 0보다 작거나 같으면
        //게임오버
        if (!isGameOver) isGameOver = true;
        if (isGameOver)
        {
            GameOver();
        }
    }
}