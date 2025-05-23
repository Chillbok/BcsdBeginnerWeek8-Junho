using System.Collections;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("공격 속도 변수")]
    [SerializeField] private float attackCooldown; //공격 쿨타임

    [Header("근접 공격 변수")]
    [SerializeField] public float meleeAttackRange; //근접 공격 범위

    public LayerMask enemyLayer; //공격할 대상 레이어

    [Header("스크립트들")]
    [SerializeField] private PlayerController playerController; //PlaerController.cs 참조변수
    [SerializeField] private CursorController cursorController; //CursorController.cs 참조변수

    private float angle; //플레이어 - 마우스 각도
    Vector2 mousePosition; //CursorController.cs에서 불러온 마우스 위치
    Vector2 playerPosition; //PlayerController에서 받아온 플레이어 위치

    private Animator animator; //애니메이터 컴포넌트를 담을 변수
    private SpriteRenderer spriteRenderer; //스프라이트렌더러 컴포넌트의 참조 변수
    private bool AbleToAttack = true; //공격 가능한지 확인하기 위한 변수. true면 공격 가능

    void Awake()
    {
        //초기화
        playerPosition = playerController.currentPlayerPosition; //플레이어 위치 초기화
        animator = GetComponent<Animator>(); //애니메이터 컴포넌트 초기화
        spriteRenderer = GetComponent<SpriteRenderer>(); //스프라이트렌더러 컴포넌트 초기화
    }

    void Update()
    {
        SetMousePosition(); //변수 업데이트
        SetPlayerPosition(); //플레이어 위치 업데이트
        CalculateMouseAngle(); //플레이어 - 마우스 각도 계산
        //Debug.Log($"플레이어 위치: {playerPosition}, 마우스 위치: {mousePosition}");
        Debug.Log("캐릭터에서 마우스까지의 각도: " + angle); //마우스 위치 각도 계산 출력용
        AttackWhenClickButton(); //좌클릭 누를 때 공격 실행 시작
    }

    void SetPlayerPosition() //플레이어 위치 동기화
    {
        if (playerController != null)
        {
            playerPosition = playerController.currentPlayerPosition;
            playerPosition.y += 1f; //캐릭터 위치 맞추기 위함, y로 1f만큼 올리기
        }
        else
        {
            Debug.LogWarning("PlayerController 변수 스크립트 지정되지 않음.");
        }
    }

    void SetMousePosition() //CursorController 스크립트에서 저장된 마우스 위치 이 스크립트용 변수에 할당하기
    {
        if (cursorController != null)
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

    }

    /*
    공격 절차
    1. 플레이어 입력 확인
    2. 플레이어 마우스 위치 확인
    3. 플레이어 마우스 위치에 따라 다른 공격 모션 출력
    */
    void AttackWhenClickButton() //좌클릭 눌렀는지 확인
    {
        if (Input.GetMouseButtonDown(0)) //마우스 클릭 여부 확인
        {
            TryAttack(); //공격 가능 여부 확인
        }
    }

    void TryAttack() //공격 가능 여부 확인하는 메서드
    {
        //공격 불가능할 때 함수 실행 X
        if (!AbleToAttack)
        {
            return;
        }

        //애니메이션이 이미 실행 중일 때 함수 실행 X
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return;
        }

        AttackAnimationParameter(); //애니메이션 패러미터 설정
        animator.SetTrigger("Attack");
        //Debug.Log("공격 애니메이션 실행");
    }

    void AttackAnimationParameter() //공격 애니메이션 출력 방향 패러미터 설정
    {
        if (45f <= angle && angle < 135f) //위쪽
        {
            spriteRenderer.flipX = false;
            animator.SetFloat("AttackWay", 0f);
        }
        else if (135f <= angle && angle < 225f) //왼쪽
        {
            spriteRenderer.flipX = true;
            animator.SetFloat("AttackWay", 1f);
        }
        else if (225f <= angle && angle < 315f) //아래쪽
        {
            spriteRenderer.flipX = false;
            animator.SetFloat("AttackWay", 2f);
        }
        else //오른쪽
        {
            spriteRenderer.flipX = false;
            animator.SetFloat("AttackWay", 3f);
        }
    }
}