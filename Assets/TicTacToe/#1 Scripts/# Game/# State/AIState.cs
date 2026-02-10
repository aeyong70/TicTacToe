using System.Threading.Tasks;
using UnityEngine;
using static Constants;


public class AIState : BaseState
{
    public Constants.PlayerType playerType;

    public AIState(bool isFirstPlayer)
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

    public override async void OnEnter(GameLogic gameLogic)
    {
        // OX UI 업데이트
        GameManager.Instance.SetGameTurn(playerType);

        await Task.Delay(1000);

        var board = gameLogic.Board;
        var result = TicTacToeAI.GetBestMove(board);

        if (result.HasValue)
        {
            int row = result.Value.row;
            int col = result.Value.col;
            int index = row * Constants.BOARD_SIZE + col;

            HandleMove(gameLogic, index);
        }
    }

    public override void OnExit(GameLogic gameLogic)
    {
    }
}

