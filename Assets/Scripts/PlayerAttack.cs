using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("공격 속도 변수")]
    [SerializeField] private float attackCooldown; //공격 쿨타임

    [Header("근접 공격 변수")]
    [SerializeField] public float meleeAttackRange; //근접 공격 범위

    public LayerMask enemyLayer; //공격할 대상 레이어

    [Header("스크립트들")]
    [SerializeField] private PlayerController playerController; //PlaerController.cs 참조변수
    [SerializeField] private CursorController cursorController; //CursorController.cs 참조변수
    private float lastAttackTime; //마지막 공격 시간

    private float angle; //플레이어 - 마우스 각도
    Vector2 mousePosition; //CursorController.cs에서 불러온 마우스 위치
    Vector2 playerPosition; //PlayerController에서 받아온 플레이어 위치

    private Animator animator; //애니메이터 컴포넌트를 담을 변수

    void Awake()
    {
        //초기화
        playerPosition = playerController.currentPlayerPosition; //플레이어 위치 초기화
        animator = GetComponent<Animator>(); //애니메이터 컴포넌트 초기화
    }

    void Update()
    {
        SetMousePosition(); //변수 업데이트
        SetPlayerPosition(); //플레이어 위치 업데이트
        CalculateMouseAngle(); //플레이어 - 마우스 각도 계산
        //Debug.Log($"플레이어 위치: {playerPosition}, 마우스 위치: {mousePosition}");
        AttackAnimationParameter(); //애니메이션 방향 설정
        AttackWhenClickButton(); //좌클릭 누를 때 공격 실행 시작
    }

    void AttackAnimationParameter() //공격 애니메이션 출력 방향 패러미터 설정
    {
        string animationParamName = "AttackWay"; //애니메이션 방향 패러미터 이름
        //0, 1, 2, 3 = 오른쪽, 왼쪽, 아래, 위

        if (0f <= angle && angle < 90f)
        {
            animator.SetInteger(animationParamName, 0);
        }
        else if (90f <= angle && angle < 180f)
        {
            animator.SetInteger(animationParamName, 1);
        }
        else if (180f <= angle && angle < 270f)
        {
            animator.SetInteger(animationParamName, 2);
        }
        else
        {
            animator.SetInteger(animationParamName, 3);
        }
    }

    void SetPlayerPosition() //플레이어 위치 동기화
    {
        if(playerController != null)
        {
            playerPosition = playerController.currentPlayerPosition;
        }
        else
        {
            Debug.LogWarning("PlayerController 변수 스크립트 지정되지 않음.");
        }
    }

    void SetMousePosition() //CursorController 스크립트에서 저장된 마우스 위치 이 스크립트용 변수에 할당하기
    {
        if(cursorController != null)
        {
            //CursorController 스크립트의 public MousePosition 속성을 가져온다
            mousePosition = cursorController.MousePosition;
        }
    }

    void CalculateMouseAngle()
    {
        //마우스 위치를 기준으로 원점(0,0)으로부터의 각도 계산
        Vector2 directionToMouse = mousePosition - playerPosition; //마우스 위치 벡터 - 플레이어 위치 벡터
        Vector2 referenceDirection = Vector2.right; //오른쪽 향하는 벡터
        float signedAngle = Vector3.SignedAngle(referenceDirection, directionToMouse, Vector3.forward); //벡터 각도 계산

        //0 ~ 360 각도로 변환
        float angle360 = (signedAngle < 0) ? (signedAngle + 360) : signedAngle; //만약 3사분면, 4사분면이면 360 더해서 양수로 만듬
        angle = angle360; //전역변수에 계산한 각도 대입

        Debug.Log("캐릭터에서 마우스까지의 각도: " + angle); //마우스 위치 각도 계산 출력용
    }

    void TryAttack() //공격 가능 여부 확인하는 메서드
    {
        //쿨타임 체크
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PerformAttack();
            lastAttackTime = Time.time; //마지막 공격 시간 업데이트
        }
    }

    void AttackWhenClickButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryAttack(); //공격 실행 가능 여부 확인 후 공격 실행
        }
    }
    void PerformAttack() //공격 실행하는 메서드
    {
    }
}