using UnityEngine;


public class PlayerState : BaseState
{
    private Constants.PlayerType playerType;
    public PlayerState(bool isFirstPlayer)
    {
        playerType = isFirstPlayer ? Constants.PlayerType.PlayerA : Constants.PlayerType.PlayerB;
    }

    public override void HandleMove(GameLogic gameLogic, int index)
    {
        ProcessMove(gameLogic, index, playerType);
    }

    public override void HandleNextTurn(GameLogic gameLogic)
    {
        gameLogic.ChangeGameState();

    }

    public override void OnEnter(GameLogic gameLogic)
    {
        // 상태 진입 시 로직 구현
        gameLogic.blockController.onBlockClicked = (blockIndex) =>
        {
            // 블록이 클릭되었을 때 처리할 로직
            HandleMove(gameLogic, blockIndex);
        };

        // O, X UI 업데이트
        GameManager.Instance.SetGameTurn(playerType);
    }

    public override void OnExit(GameLogic gameLogic)
    {

    }
}

