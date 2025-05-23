using UnityEngine;

public class AttackStateBehaviour : StateMachineBehaviour
{
    PlayerController playerController;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Attack 상태 진입 시 플레이어 컨트롤러를 찾아서 canMove를 false로 설정
        playerController = animator.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.canMove = false; //플레이어 움직임 비활성화
        }
        else
        {
            Debug.LogWarning("AttackStateBehaviour에 PlayerController.cs 할당 안됨");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController = animator.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.canMove = true; //플레이어 움직임 활성화
        }
    }
}
