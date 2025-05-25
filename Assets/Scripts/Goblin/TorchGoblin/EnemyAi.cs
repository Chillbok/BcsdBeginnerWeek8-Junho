using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI; //적이 플레이어에게 움직이게 하기 위해 사용

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float moveSpeed;

    //스크립트 참조변수
    GoblinData goblinData;

    void Awake()
    {
        ReferenceReset();
        moveSpeed = goblinData.moveSpeed;
    }

    void ReferenceReset()
    {
        goblinData = GetComponent<GoblinData>();
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void measureTargetLocation(Transform target)
    {
        if (target == null)
        {
            return;
        }
        Vector2 direction = (target.position - transform.position).normalized;

        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }
}
