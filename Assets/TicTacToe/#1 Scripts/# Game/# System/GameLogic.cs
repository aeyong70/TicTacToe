using static Constants;


public class GameLogic
{
    // 화면에 Block을 제어하기 위한 변수
    public BlockController blockController;

    // 보드의 상태
    private PlayerType[,] board;

    // 플레이어 상태 변수
    public BaseState playerStateA;
    public BaseState playerStateB;

    // 현재 상태 변수
    private BaseState currentState;

    // 게임의 결과 
    public enum GameResult { None, Win, Lose, Draw }

    // 보드 정보
    public Constants.PlayerType[,] Board { get { return board; } }

    public GameLogic(GameType gameType, BlockController blockController)
    {
        // BlockController 할당
        this.blockController = blockController;

        // 보드 정보 초기화
        board = new PlayerType[BOARD_SIZE, BOARD_SIZE];

        // GameType에 따른 초기화 작업 수행
        switch (gameType)
        {
            case GameType.SinglePlay:
                // 싱글 플레이 모드 초기화 작업
                playerStateA = new PlayerState(true);
                playerStateB = new AIState(false);

                SetState(playerStateA);
                break;
            case GameType.MultiPlay:
                // 멀티 플레이 모드 초기화 작업
                playerStateA = new PlayerState(true);
                playerStateB = new PlayerState(false);

                // 초기 상태 설정 (예 : 플레이어 A부터 시작
                SetState(playerStateA);
                break;
        }
    }

    // 턴 바뀔 때 호출하는 메서드
    public void SetState(BaseState newState)
    {
        currentState?.OnExit(this);
        currentState = newState;
        currentState?.OnEnter(this);
    }

    // 마커 표시를 위한 메서드
    public bool PlaceMarker(int index, PlayerType playerType)
    {
        var row = index / BOARD_SIZE;
        var col = index % BOARD_SIZE;

        // 해당 위치에 이미 마커가 있는지 확인, 뭔가 있으면 false 반환
        if (board[row, col] != Constants.PlayerType.None) return false;

        blockController.PlaceMarker(index, playerType);
        board[row, col] = playerType;

        return true;
    }

    // 턴 변경
    public void ChangeGameState()
    {
        if(currentState == playerStateA)
        {
            SetState(playerStateB);
        }
        else
        {
            SetState(playerStateA);
        }
    }

    // 게임 결과 확인
    public GameResult CheckGameResult()
    {
        // 승리 조건 확인 로직 구현 (생략)
        if(CheckGameWin(PlayerType.PlayerA, board))
        {
            return GameResult.Win;
        }
        else if(CheckGameWin(PlayerType.PlayerB, board))
        {
            return GameResult.Lose;
        }
        else if (CheckGameDraw(board))
        {
            return GameResult.Draw;
        }

        return GameResult.None;
    }

    public bool CheckGameWin(Constants.PlayerType playerType, Constants.PlayerType[,] board)
    {
        for (var row = 0; row < board.GetLength(0); row++)
        {
            if (board[row, 0] == playerType &&
                board[row, 1] == playerType &&
                board[row, 2] == playerType)
            {
                return true;
            }
        }

        for (var col = 0; col < board.GetLength(1); col++)
        {
            if (board[0, col] == playerType &&
                board[1, col] == playerType &&
                board[2, col] == playerType)
            {
                return true;
            }
        }

        if (board[0, 0] == playerType &&
            board[1, 1] == playerType &&
            board[2, 2] == playerType)
        {
            return true;
        }

        if (board[0, 2] == playerType &&
            board[1, 1] == playerType &&
            board[2, 0] == playerType)
        {
            return true;
        }

        return false;
    }

    public bool CheckGameDraw(Constants.PlayerType[,] board)
    {
        for(var row = 0; row < board.GetLength(0); row++)
        {
            for(var col = 0; col < board.GetLength(1); col++)
            {
                if (board[row, col] == Constants.PlayerType.None) return false;
            }
        }
        return true;
    }

    public void EndGame(GameResult gameResult)
    {
        // 게임 오버가 되면 "게임오버" 팝업 띄우고 팝업에서 확인 버튼을 누르면 Main 씬으로 전환

        string resultStr = string.Empty;
        switch (gameResult)
        {
            case GameResult.Win:
                resultStr = "Player1 승리!";
                break;
            case GameResult.Lose:
                resultStr = "Player2 승리!";
                break;
            case GameResult.Draw:
                resultStr = "무승부!";
                break;
        }

        GameManager.Instance.OpenConfirmPanel(resultStr, () =>
        {
            GameManager.Instance.ChangeToMainScene();
        });
    }
}

