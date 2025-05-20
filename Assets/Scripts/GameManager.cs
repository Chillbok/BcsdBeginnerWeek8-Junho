using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 Game Manager
//단 하나만 존재
public class GameManager : MonoBehaviour
{
    public static GameManager instance; //싱글톤을 할당할 전역 변수

    public bool isGameOver = false; //게임 오버 상태인가?

    //게임 시작과 동시에 싱글톤 구성
    void DefineIsThereSingleTon()
    {
        //싱글톤 변수가 비어있는가?
        if (instance == null)
        {
            //instance가 비어있다면 그곳에 자기 자신 할당
            instance = this;
        }
        else //instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우
        {
            Debug.LogWarning("씬에 두 개 이상의 GameManager가 존재한다!");
            Destroy(gameObject);
        }
    }
}