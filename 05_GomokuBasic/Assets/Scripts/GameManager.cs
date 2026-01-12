// 2026-01-12 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject blackStonePrefab; // 검은돌 프리팹
    public GameObject whiteStonePrefab; // 흰돌 프리팹
    public int gridSize = 11; // 격자 크기
    public float cellSize = 0.75f; // 셀 크기

    private GameObject[,] placedStones; // 이미 놓인 돌을 저장하는 배열
    private bool isBlackTurn = true; // 현재 차례가 검은돌인지 여부
    private Vector3 gridCenter; // 격자의 중심 좌표
    private Vector3 gridStart; // 격자의 시작 좌표

    void Start()
    {
        // 격자 크기에 맞는 배열 초기화 (12x12로 변경)
        placedStones = new GameObject[gridSize + 1, gridSize + 1];

        // 격자의 중심 좌표를 카메라의 위치로 설정
        Camera mainCamera = Camera.main;
        gridCenter = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0);

        // 격자의 시작 좌표 계산 (왼쪽과 아래 방향으로 반 칸만 이동)
        float gridLength = gridSize * cellSize; // 격자 크기 계산
        gridStart = gridCenter - new Vector3(gridLength / 2, gridLength / 2, 0);
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) // 새로운 Input System에서 마우스 클릭 감지
        {
            PlaceStone();
        }
    }

    void PlaceStone()
    {
        // 마우스 클릭 위치를 월드 좌표로 변환
        Vector2 mousePosition = Mouse.current.position.ReadValue();
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
}