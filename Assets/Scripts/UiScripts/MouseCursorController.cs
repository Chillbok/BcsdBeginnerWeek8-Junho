using UnityEngine;

public class MouseCursorController : MonoBehaviour
{
    [SerializeField] Transform tf_cursor; //마우스 커서 현재 위치

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CursorMoving();
    }

    void CursorMoving()
    {
        //마우스 커서 위치 게임 속 마우스 위치로 이동시키기
        float x = Input.mousePosition.x - (Screen.width / 2);
        float y = Input.mousePosition.y - (Screen.height / 2);
        tf_cursor.localPosition = new Vector2(x, y); //현재 마우스의 위치

        //마우스 가두기(범위 지정)
        float tmp_cursorPosX = tf_cursor.localPosition.x;
        float tmp_cursorPosY = tf_cursor.localPosition.y;

        float minScreenWidth = -Screen.width / 2;
        float maxScreenWidth = Screen.width / 2;
        float minScreenHeight = -Screen.height / 2;
        float maxScreenHeight = Screen.height / 2;
        int padding = 20;

        //clamp 함수로 커서의 최대, 최소 범위 지정
        tmp_cursorPosX = Mathf.Clamp(tmp_cursorPosX, minScreenWidth + padding, maxScreenWidth - padding);
        tmp_cursorPosY = Mathf.Clamp(tmp_cursorPosY, minScreenHeight + padding, maxScreenHeight - padding);

        //범위를 지정해 tf_cursor의 값을 다시 새로운 벡터로 바꿔주기
        tf_cursor.localPosition = new Vector2(tmp_cursorPosX, tmp_cursorPosY);
    }
}
