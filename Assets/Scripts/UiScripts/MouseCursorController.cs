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
        float x = Input.mousePosition.x - (Screen.width / 2);
        float y = Input.mousePosition.y - (Screen.height / 2);
        tf_cursor.localPosition = new Vector2(x,y); //현재 마우스의 위ㅈ
    }
}
