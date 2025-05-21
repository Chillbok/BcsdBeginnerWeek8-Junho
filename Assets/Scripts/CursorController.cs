using UnityEngine;
using UnityEngine.Rendering;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance; //static 변수로 인스턴스 선언

    [SerializeField] private Camera mainCamera; //메인카메라
    
    //외부에 공개할 마우스 위치 프로퍼티(읽기 전용)
    public Vector2 MousePosition {get; private set;} //마우스 위치 프로퍼티
    public PlayerController playerController; //Inspector에서 연결
    Vector2 playerPosition; //PlayerController에서 받아온 플레이어 위치

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //씬이 바뀌어도 파괴되지 않게 하려면 주석 해제
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //이미 Instance가 존재하면 중복된 이 오브젝트 파괴
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //게임 시작 시 마우스 커서 보이기
        CursorVisible();
    }

    void Update()
    {
        CursorLocation();
    }

    void CursorVisible() //마우스 커서 보이게 하는 메서드
    {
        //마우스 커서 보이기
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void CursorLocation() //마우스 커서 위치 저장 및 업데이트
    {
        if (mainCamera != null)
        {
            //마우스 위치를 월드 좌표로 변환
            MousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(MousePosition); //마우스 위치 제대로 출력되는지 확인용
        }
        else
        {
            Debug.LogError("메인 카메라 설정되지 않음");
        }
    }
}