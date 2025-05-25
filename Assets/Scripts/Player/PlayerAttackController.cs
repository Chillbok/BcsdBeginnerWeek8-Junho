using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerAttackController : MonoBehaviour
{
    public LayerMask enemyLayer; //공격할 대상 레이어

    //스크립트 참조변수
    [Header("스크립트 참조변수들")]
    [SerializeField] private PlayerController playerController; //PlaerController.cs 참조변수
    [SerializeField] private CursorController cursorController; //CursorController.cs 참조변수

    //히트박스들
    [Header("히트박스들")]
    [SerializeField] private GameObject upHitbox; //위쪽 히트박스
    [SerializeField] private GameObject leftHitbox; //아래쪽 히트박스
    [SerializeField] private GameObject rightHitbox; //왼쪽 히트박스
    [SerializeField] private GameObject downHitbox; //오른쪽 히트박스

    //스크립트 참조변수
    private PlayerData playerData; //PlayerData.cs

    //일반 참조변수
    private Animator animator; //애니메이터 컴포넌트를 담을 변수
    private SpriteRenderer spriteRenderer; //스프라이트렌더러 컴포넌트의 참조 변수

    private float angle; //플레이어 - 마우스 각도
    int playermeleeDamage; //플레이어 공격력(playerData.cs 에서 가져와야함)

    Vector3 mousePosition; //마우스 위치(cursor)
    Vector3 playerPosition; //플레이어 위치

    void Awake()
    {
        ResetReference(); //변수 초기화
        playermeleeDamage = playerData.meleeAtkDamage; //playerData.cs에서 플레이어 공격력 가져오기
    }

    void Start()
    {
        DeactivateAllHitboxes(); //게임 시작 전 모든 히트박스 비활성화
    }

    void ResetReference() //변수 초기화
    {
        playerData = GetComponent<PlayerData>(); //플레이어 데이터 정보 불러오기
        animator = GetComponent<Animator>(); //애니메이터 컴포넌트 초기화
        spriteRenderer = GetComponent<SpriteRenderer>(); //스프라이트렌더러 컴포넌트 초기화
    }

    void Update()
    {
        playerPosition = playerData.playerPosition; //프레임마다 플레이어 위치 동기화
        mousePosition = cursorController.MousePosition; //프레임마다 마우스 위치 동기화
        CalculateMouseAngle(); //플레이어 - 마우스 각도 계산
        //Debug.Log($"플레이어 위치: {playerPosition}, 마우스 위치: {mousePosition}");
        //Debug.Log("캐릭터에서 마우스까지의 각도: " + angle); //마우스 위치 각도 계산 출력용
        AttackWhenClickButton(); //좌클릭 누를 때 공격 실행 시작
    }

    void CalculateMouseAngle()
    {
        //마우스 위치를 기준으로 원점(0,0)으로부터의 각도 계산
        Vector3 directionToMouse = mousePosition - playerPosition; //마우스 위치 벡터 - 플레이어 위치 벡터
        Vector3 referenceDirection = Vector3.right; //오른쪽 향하는 벡터
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
    3.1. 공격 모션 출력하면서 해당 위치의 콜라이더 활성화
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
        float hitboxWay; //공격 방향 = 히트박스 활성화 방향

        //공격 불가능할 때 함수 실행 X
        if (!playerData.playerAbleToAttack)
        {
            return;
        }

        //애니메이션이 이미 실행 중일 때 함수 실행 X
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) //Attack 애니메이션 블렌드 트리 실행 중이라면 메서드 실행 X
        {
            return;
        }

        hitboxWay = AttackAnimationParameter(); //애니메이션 패러미터 설정 및 콜라이더 활성화 방향 설정
        animator.SetTrigger("Attack"); //공격 애니메이션 실행 트리거
        playerData.playerAbleToMove = false; //플레이어 움직임 비활성화
        //AttackStateBehaviour.cs 에서 플레이어 움직임 다시 활성화
        //AttackStateBehaviour.cs 에서 플레이어 움직임 비활성화
    
        ActivateHitbox(hitboxWay); //공격 방향의 히트박스 활성화
    }

    void ActivateHitbox(float hitboxWay) //공격 방향의 히트박스 활성화
    {
        switch (hitboxWay)
        {
            case 0f: //위쪽
                upHitbox.SetActive(true);
                break;
            case 1f: //왼쪽
                leftHitbox.SetActive(true);
                break;
            case 2f: //아래쪽
                downHitbox.SetActive(true);
                break;
            case 3f: //오른쪽
                rightHitbox.SetActive(true);
                break;
        }
    }

    public void DeactivateAllHitboxes() //모든 히트박스 비활성화
    {
        bool upHitboxEnabled = upHitbox.activeSelf;
        bool leftHitboxEnabled = leftHitbox.activeSelf;
        bool rightHitboxEnabled = rightHitbox.activeSelf;
        bool downHitboxEnabled = downHitbox.activeSelf;

        if (upHitboxEnabled) upHitbox.SetActive(false);
        if (leftHitboxEnabled) leftHitbox.SetActive(false);
        if (rightHitboxEnabled) rightHitbox.SetActive(false);
        if (downHitboxEnabled) downHitbox.SetActive(false);
    }

    float AttackAnimationParameter() //공격 애니메이션 출력 방향 패러미터 설정
    {
        float answer;
        if (45f <= angle && angle < 135f) //위쪽
        {
            spriteRenderer.flipX = false;
            animator.SetFloat("AttackWay", 0f);
            answer = 0f;
        }
        else if (135f <= angle && angle < 225f) //왼쪽
        {
            spriteRenderer.flipX = true;
            animator.SetFloat("AttackWay", 1f);
            answer = 1f;
        }
        else if (225f <= angle && angle < 315f) //아래쪽
        {
            spriteRenderer.flipX = false;
            animator.SetFloat("AttackWay", 2f);
            answer = 2f;
        }
        else //오른쪽
        {
            spriteRenderer.flipX = false;
            animator.SetFloat("AttackWay", 3f);
            answer = 3f;
        }
        return answer;
    }
}