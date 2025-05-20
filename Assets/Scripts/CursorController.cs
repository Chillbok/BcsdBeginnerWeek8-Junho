using UnityEngine;

public class CursorController : MonoBehaviour
{
    void Start()
    {
        //게임 시작 시 마우스 커서 보이기
        CursorVisible();
    }

    void CursorVisible() //마우스 커서 보이게 하는 메서드
    {
        //마우스 커서 보이기
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}