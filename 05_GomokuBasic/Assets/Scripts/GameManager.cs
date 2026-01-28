using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject blackStonePrefab; // 검은돌 프리팹
    public GameObject whiteStonePrefab; // 흰돌 프리팹
    public int gridSize = 11; // 격자 크기
    public float cellSize = 0.75f; // 셀 크기

    private UIManager uiManager; // UI 관리자 참조
    private GameObject[,] placedStones; // 이미 놓인 돌을 저장하는 배열
    private int[,] boardState; // 오목판 상태 (0: 빈칸, 1: 흑돌, 2: 백돌)
    private bool isBlackTurn = true; // 현재 차례가 검은돌인지 여부
    private Vector3 gridCenter; // 격자의 중심 좌표
    private Vector3 gridStart; // 격자의 시작 좌표

    // 승리 조건 관련 변수
    private bool isGameOver = false; // 게임이 끝났는지 여부
    private bool isBlackWinner = false; // 검은돌이 이겼는지 여부
    private bool isWhiteWinner = false; // 흰돌이 이겼는지 여부

    void Start()
    {
        // UI Manager 찾기
        uiManager = FindObjectOfType<UIManager>();

        // 격자 크기에 맞춰 배열 초기화 (12x12로 변경)
        placedStones = new GameObject[gridSize + 1, gridSize + 1];
        boardState = new int[gridSize + 1, gridSize + 1]; // 오목판 상태 배열 초기화 (0으로 자동 초기화)

        // 격자의 중심 좌표를 카메라의 위치로 설정
        Camera mainCamera = Camera.main;
        gridCenter = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0);

        // 격자의 시작 좌표 계산 (왼쪽과 아래 방향으로 반 칸만 이동)
        float gridLength = gridSize * cellSize; // 격자 크기 계산
        gridStart = gridCenter - new Vector3(gridLength / 2, gridLength / 2, 0);
    }

    void Update()
    {
        // 게임이 끝났으면 더 이상 돌을 놓을 수 없음
        if (isGameOver)
            return;

        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 감지
        {
            PlaceStone();
        }
    }

    void PlaceStone()
    {
        // 마우스 클릭 위치를 월드 좌표로 변환
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        worldPosition.z = 0; // Z축은 0으로 고정

        // 가장 가까운 격자 교차점 계산
        int xIndex = Mathf.RoundToInt((worldPosition.x - gridStart.x) / cellSize);
        int yIndex = Mathf.RoundToInt((worldPosition.y - gridStart.y) / cellSize);

        // 격자 범위 내에 있는지 확인 (12x12로 범위 확장)
        if (xIndex >= 0 && xIndex <= gridSize && yIndex >= 0 && yIndex <= gridSize)
        {
            // 이미 돌이 놓여져 있는지 확인
            if (placedStones[xIndex, yIndex] == null)
            {
                // 현재 차례에 따라 돌 배치
                GameObject stonePrefab = isBlackTurn ? blackStonePrefab : whiteStonePrefab;
                Vector3 stonePosition = gridStart + new Vector3(xIndex * cellSize, yIndex * cellSize, 0);
                GameObject newStone = Instantiate(stonePrefab, stonePosition, Quaternion.identity);

                // 배열에 돌 저장
                placedStones[xIndex, yIndex] = newStone;

                // 오목판 상태 배열에 기록 (1: 흑돌, 2: 백돌)
                boardState[xIndex, yIndex] = isBlackTurn ? 1 : 2;

                // 승리 조건 체크
                if (CheckWinCondition(xIndex, yIndex, isBlackTurn))
                {
                    isGameOver = true;
                    if (isBlackTurn)
                    {
                        isBlackWinner = true;
                        Debug.Log("검은돌이 승리했습니다!");
                    }
                    else
                    {
                        isWhiteWinner = true;
                        Debug.Log("흰돌이 승리했습니다!");
                    }

                    // UI 패널 표시
                    if (uiManager != null)
                    {
                        uiManager.ShowWinPanel(isBlackWinner);
                    }
                }

                // 차례 변경
                isBlackTurn = !isBlackTurn;
            }
            else
            {
                Debug.Log("이미 돌이 놓여진 위치입니다!");
            }
        }
        else
        {
            Debug.Log("격자 범위를 벗어났습니다!");
        }
    }

    // 승리 조건 체크 함수
    bool CheckWinCondition(int x, int y, bool isBlack)
    {
        // 4가지 방향 체크: 가로, 세로, 대각선(\), 대각선(/)
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 0),   // 가로 (좌우)
            new Vector2Int(0, 1),   // 세로 (상하)
            new Vector2Int(1, 1),   // 대각선 (\)
            new Vector2Int(1, -1)   // 대각선 (/)
        };

        foreach (Vector2Int direction in directions)
        {
            int count = 1; // 현재 놓은 돌 포함

            // 양방향으로 체크
            count += CountStonesInDirection(x, y, direction.x, direction.y, isBlack);
            count += CountStonesInDirection(x, y, -direction.x, -direction.y, isBlack);

            // 연속된 돌이 5개 이상이면 승리
            if (count >= 5)
            {
                return true;
            }
        }

        return false;
    }

    // 특정 방향으로 연속된 같은 색 돌의 개수를 세는 함수
    int CountStonesInDirection(int x, int y, int dx, int dy, bool isBlack)
    {
        int count = 0;
        int currentX = x + dx;
        int currentY = y + dy;
        int targetStoneType = isBlack ? 1 : 2; // 1: 흑돌, 2: 백돌

        // 격자 범위 내에서 같은 색의 돌이 연속되는지 체크
        while (currentX >= 0 && currentX <= gridSize &&
               currentY >= 0 && currentY <= gridSize)
        {
            // boardState 배열에서 돌의 상태 확인
            int stoneType = boardState[currentX, currentY];

            // 돌이 없거나(0) 다른 색이면 중단
            if (stoneType != targetStoneType)
                break;

            count++;
            currentX += dx;
            currentY += dy;
        }

        return count;
    }

    // 게임 리셋 함수 (필요시 사용)
    public void ResetGame()
    {
        // 모든 돌 제거
        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                if (placedStones[x, y] != null)
                {
                    Destroy(placedStones[x, y]);
                    placedStones[x, y] = null;
                }

                // 오목판 상태 배열 초기화
                boardState[x, y] = 0;
            }
        }

        // 게임 상태 초기화
        isBlackTurn = true;
        isGameOver = false;
        isBlackWinner = false;
        isWhiteWinner = false;

        // UI 패널 숨기기
        if (uiManager != null)
        {
            uiManager.HidePanel();
        }

        Debug.Log("게임이 초기화되었습니다!");
    }
}