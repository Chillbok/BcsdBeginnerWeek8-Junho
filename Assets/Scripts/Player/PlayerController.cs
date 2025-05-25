/*
용도: 플레이어 이동 관리
*/
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //스크립트 참조변수
    private PlayerData playerData;
    private Rigidbody2D varRigidBody; //플레이어의 rigidbody2d 컴포넌트 참조
    private Animator animator; //애니메이터 컴포넌트 참조를 위한 변수
    private SpriteRenderer spriteRenderer; //SpriteRender 컴포넌트 참조 변수

    //스크립트 내에서 사용할 변수
    private Vector2 movementInput; //플레이어의 입력 값을 저장할 변수
    float leftRightInput; //좌우 이동값
    float upDownInput; //위아래 이동값
    

    void Awake()
    {
        ResetReference(); //참조변수 초기화
    }

    void ResetReference()
    {
        playerData = GetComponent<PlayerData>();
        //게임 시작 시 Rigidbody2d 컴포넌트 가져오기
        varRigidBody = GetComponent<Rigidbody2D>();
        //게임 시작 시 Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //프레임마다 사용자 입력 받기
        GetPlayerMoveInput();
        //플레이어 애니메이션과 관련된 부분은 "보이는 영역"이므로, 프레임 업데이트마다 설정해도 충분하다고 결론을 내림
        //따라서 플레이어 애니메이션과 관련된 부분은 Update() 메서드에 선언.
        PlayerMoveAnimationControll();

    }

    void FixedUpdate()
    {
        //물리 관련 계산 및 이동 처리
        PlayerMove();
    }

    void GetPlayerMoveInput() //사용자 입력 받는 메서드
    {
        bool ableToMove = playerData.playerAbleToMove;
        if (ableToMove) //플레이어가 움직일 수 있는 상태라면
        {
            leftRightInput = Input.GetAxisRaw("Horizontal");
            upDownInput = Input.GetAxisRaw("Vertical");

            //이동 방향 벡터 저장
            //.normalized를 사용해 대각선 이동 시 속도가 빨라지는 것 방지
            movementInput = new Vector2(leftRightInput, upDownInput).normalized;
            Debug.Log("움직임 가능");
        }
        else
        {
            //움직임이 불가능할 때 입력을 0으로 처리해 캐릭터 멈추기
            Debug.Log("움직임 불가능");
            leftRightInput = 0;
            upDownInput = 0;
            movementInput = Vector2.zero;
        }
    }
    void PlayerMove() //플레이어 이동을 구현한 메서드
    {
        float moveSpeed = playerData.playerBasicMoveSpeed;
        //이동 방향 * 이동 속도
        Vector2 targetVelocity = movementInput * moveSpeed;
        varRigidBody.linearVelocity = targetVelocity;
    }

    void PlayerMoveAnimationControll() //플레이어 이동 애니메이션 컨트롤 메서드
    {
        if (upDownInput != 0) //위아래로 움직임
        {
            animator.SetBool("isMove", true);
        }
        else if (leftRightInput < 0) //왼쪽으로 움직임
        {
            animator.SetBool("isMove", true);
            //varSpriteRenderer.flipX = true;
            spriteRenderer.flipX = true;
        }
        else if (leftRightInput > 0) //오른쪽으로 움직임
        {
            animator.SetBool("isMove", true);
            //spriteRenderer.flipX = false;
            spriteRenderer.flipX = false;
        }
        else //캐릭터가 움직이지 않는 경우
        {
            animator.SetBool("isMove", false);
        }
        //
        //animationFlip = varSpriteRenderer.flipX;
    }
}