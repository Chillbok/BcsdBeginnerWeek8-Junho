using UnityEngine;
using UnityEngine.Rendering;

public class CursorController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; //메인카메라
    private Vector2 mousePos; //마우스 위치
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

    void CursorLocation() //마우스 커서 위치 저장
    {
        //마우스 위치를 월드 좌표로 변환
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePos);
    }
}