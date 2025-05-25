using UnityEngine;

public class AttackStateBehaviour : StateMachineBehaviour
{
    PlayerData playerData;
    PlayerAttackController playerAtkCtrlr;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Attack 상태 진입 시 플레이어 컨트롤러를 찾아서 canMove를 false로 설정
        playerData = animator.GetComponent<PlayerData>();
        if (playerData != null)
        {
            playerData.playerAbleToMove = false; //플레이어 움직임 비활성화
        }
        else
        {
            Debug.LogWarning("AttackStateBehaviour에 PlayerData.cs 할당 안됨");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerData = animator.GetComponent<PlayerData>();
        playerAtkCtrlr = animator.GetComponent<PlayerAttackController>();

        if (playerData != null)
        {
            playerData.playerAbleToMove = true; //플레이어 움직임 활성화
        }

        if (playerAtkCtrlr != null)
        {
            playerAtkCtrlr.DeactivateAllHitboxes(); //모든 히트박스 비활성화
        }
    }
}
