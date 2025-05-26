// 버튼 클릭하면 특정 오브젝트 활성화시키는 스크립트
using UnityEngine;
using UnityEngine.SceneManagement; //씬 전환을 위해 필요함

public class ButtonActions : MonoBehaviour
{
    public GameObject targetObject; //활성화할 오브젝트
    public string sceneName; //이동할 Scene

    public void ActivateObject()
    {
        if (targetObject != null) targetObject.SetActive(true); //오브젝트 활성화
    }

    public void DeactivateObject()
    {
        if (targetObject != null) targetObject.SetActive(false); //오브젝트 비활성화
    }

    public void ChangeScene() //버튼 클릭 시 씬 바꾸기
    {
        if (!string.IsNullOrEmpty(sceneName)) SceneManager.LoadScene(sceneName);
        else Debug.LogWarning("씬 이름이 비어있습니다.");
    }

    public void QuitGame()
    {
        Application.Quit(); //빌드된 게임에서는 애플리케이션 종료
#if UNITY_EDITOR //만약 Unity 에디터에서 실행했다면
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
