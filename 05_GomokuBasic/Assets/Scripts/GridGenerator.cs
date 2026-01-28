using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int gridSize = 11; // 격자의 크기
    public float cellSize = 0.75f; // 각 셀의 크기
    public Material lineMaterial; // LineRenderer에 사용할 Material

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // 카메라의 현재 위치를 기준으로 그리드의 중심을 설정
        Camera mainCamera = Camera.main;
        Vector3 gridCenter = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0);

        float gridLength = gridSize * cellSize;
        float startOffset = -gridLength / 2; // 그리드의 시작 위치를 계산

        for (int i = 0; i <= gridSize; i++)
        {
            // 수평선 그리기
            float yPosition = gridCenter.y + startOffset + i * cellSize;
            CreateLine(new Vector3(gridCenter.x + startOffset, yPosition, 0), new Vector3(gridCenter.x + startOffset + gridLength, yPosition, 0));

            // 수직선 그리기
            float xPosition = gridCenter.x + startOffset + i * cellSize;
            CreateLine(new Vector3(xPosition, gridCenter.y + startOffset, 0), new Vector3(xPosition, gridCenter.y + startOffset + gridLength, 0));
        }
    }

    void CreateLine(Vector3 start, Vector3 end)
    {
        GameObject lineObject = new GameObject("GridLine");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.05f; // 선의 두께
        lineRenderer.endWidth = 0.05f;
        lineRenderer.startColor = Color.black; // 선의 시작 색상
        lineRenderer.endColor = Color.black; // 선의 끝 색상
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
