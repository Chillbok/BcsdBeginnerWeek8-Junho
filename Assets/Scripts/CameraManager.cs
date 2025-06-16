using Unity.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    void Start()
    {
    }

    void Update()
    {
        FollowObject(player);
    }

    void FollowObject(GameObject objectToFollow)
    {
        float posX = objectToFollow.transform.position.x;
        float posY = objectToFollow.transform.position.y;
        gameObject.transform.position = new Vector3(posX, posY, -10);
    }
}
