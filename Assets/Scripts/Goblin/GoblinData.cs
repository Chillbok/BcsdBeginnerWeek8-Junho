using Unity.VisualScripting;
using UnityEngine;

public class GoblinData : MonoBehaviour
{
    [SerializeField] public float damage; //데미지
    [SerializeField] public float hp; //체력
    [SerializeField] public float moveSpeed; //움직이는 속도
    [SerializeField] public float attackRange; //공격 거리
    [SerializeField] public float seeRange; //플레이어 인지 거리
    [SerializeField] GameObject defaultTarget; //기본 공격 목표
    GameObject Player; //플레이어 게임 오브젝트

    PlayerController playerController;
    Vector2 moveDirection;
    Vector3 goblinPosition;

    void Update()
    {
        goblinPosition = transform.position;
    }
}
