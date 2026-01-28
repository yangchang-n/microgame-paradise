using UnityEngine;

public class SandSimulator : MonoBehaviour
{
    // Grid Settings
    private int gridSize;
    private int cellPixelSize;
    public int clickableRows;

    private int width;
    private int height;

    // Cell Types
    public enum CellType
    {
        Empty,
        SkySand,
        BrownSand,
        Wall
    }

    private CellType[,] grid;
    private Texture2D texture;
    private SpriteRenderer spriteRenderer;

    // Colors
    private Color skySandColor;
    private Color brownSandColor;
    private Color wallColor;
    private Color boardBackgroundColor;
    private Color clickableAreaColor;
    private Color gridLineColor;

    // Constants
    private const int SPAWN_PATTERN_HEIGHT = 3;
    private const int SPAWN_PATTERN_WIDTH = 3;

    void Start()
    {
        InitializeSettings();
        InitializeColors();
        InitializeGrid();
        SetupRenderer();
        SetupCamera();
    }

    void InitializeSettings()
    {
        gridSize = 15;
        cellPixelSize = 20;
        clickableRows = 5;

        width = gridSize * cellPixelSize + 2;  // +2 for walls
        height = gridSize * cellPixelSize + 1; // +1 for bottom wall
    }

    void InitializeColors()
    {
        skySandColor = new Color(0.4f, 0.85f, 0.95f);
        brownSandColor = new Color(0.6f, 0.4f, 0.2f);
        wallColor = new Color(0.3f, 0.3f, 0.3f);
        boardBackgroundColor = new Color(0.85f, 0.75f, 0.6f);
        clickableAreaColor = new Color(0.95f, 0.9f, 0.8f);
        gridLineColor = Color.black;
    }

    void InitializeGrid()
    {
        grid = new CellType[width, height];

        // Bottom wall
        for (int x = 0; x < width; x++)
        {
            grid[x, 0] = CellType.Wall;
        }

        // Side walls
        for (int y = 0; y < height; y++)
        {
            grid[0, y] = CellType.Wall;
            grid[width - 1, y] = CellType.Wall;
        }
    }

    void SetupRenderer()
    {
        texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;

        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, width, height),
            new Vector2(0.5f, 0.5f),
            1f
        );
        spriteRenderer.sprite = sprite;
    }

    void SetupCamera()
    {
        Camera.main.orthographicSize = height / 2f * 1.3f;
        Camera.main.transform.position = new Vector3(0, 15f, -10f);
    }

    public void SimulatePhysics()
    {
        // Scan from bottom to top to avoid processing same particle twice
        for (int y = 1; y < height - 1; y++)
        {
            // Randomize scan direction for natural sand behavior
            bool scanLeft = Random.value > 0.5f;

            for (int x = 1; x < width - 1; x++)
            {
                int currentX = scanLeft ? x : width - 1 - x;

                if (IsSand(grid[currentX, y]))
                {
                    UpdateSand(currentX, y);
                }
            }
        }
    }

    void UpdateSand(int x, int y)
    {
        CellType sandType = grid[x, y];

        // Try to fall down
        if (grid[x, y - 1] == CellType.Empty)
        {
            grid[x, y] = CellType.Empty;
            grid[x, y - 1] = sandType;
            return;
        }

        // Try to fall diagonally
        int direction = Random.value > 0.5f ? 1 : -1;

        if (grid[x + direction, y - 1] == CellType.Empty)
        {
            grid[x, y] = CellType.Empty;
            grid[x + direction, y - 1] = sandType;
        }
        else if (grid[x - direction, y - 1] == CellType.Empty)
        {
            grid[x, y] = CellType.Empty;
            grid[x - direction, y - 1] = sandType;
        }
    }

    public void UpdateTexture()
    {
        DrawBackground();
        DrawGridLines();
        texture.Apply();
    }

    void DrawBackground()
    {
        int minClickableY = GetMinClickableY();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = GetPixelColor(x, y, minClickableY);
                texture.SetPixel(x, y, color);
            }
        }
    }

    Color GetPixelColor(int x, int y, int minClickableY)
    {
        // Check for sand or wall first
        if (grid[x, y] != CellType.Empty)
        {
            return grid[x, y] switch
            {
                CellType.SkySand => skySandColor,
                CellType.BrownSand => brownSandColor,
                CellType.Wall => wallColor,
                _ => boardBackgroundColor
            };
        }

        // Empty cells - check if in clickable area
        bool isInClickableArea = x > 0 && x < width - 1 && y > minClickableY && y > 0;
        return isInClickableArea ? clickableAreaColor : boardBackgroundColor;
    }

    void DrawGridLines()
    {
        // Vertical lines
        for (int i = 0; i <= gridSize; i++)
        {
            int x = 1 + i * cellPixelSize;
            for (int y = 1; y < height; y++)
            {
                texture.SetPixel(x, y, gridLineColor);
            }
        }

        // Horizontal lines
        for (int i = 0; i <= gridSize; i++)
        {
            int y = 1 + i * cellPixelSize;
            if (y >= height) break;

            for (int x = 1; x < width - 1; x++)
            {
                texture.SetPixel(x, y, gridLineColor);
            }
        }
    }

    public bool SpawnSand(int worldX, int worldY, CellType sandType, int amount)
    {
        int spawnedCount = 0;

        // Spawn in a small area around the click position
        for (int dy = 0; dy < SPAWN_PATTERN_HEIGHT && spawnedCount < amount; dy++)
        {
            for (int dx = -1; dx <= 1 && spawnedCount < amount; dx++)
            {
                int posX = worldX + dx;
                int posY = worldY + dy;

                if (IsInBounds(posX, posY) && grid[posX, posY] == CellType.Empty)
                {
                    grid[posX, posY] = sandType;
                    spawnedCount++;
                }
            }
        }

        return spawnedCount > 0;
    }

    // Helper Methods
    int GetMinClickableY()
    {
        return height - 1 - (clickableRows * cellPixelSize);
    }

    bool IsSand(CellType type)
    {
        return type == CellType.SkySand || type == CellType.BrownSand;
    }

    public bool IsInClickableArea(int x, int y)
    {
        if (!IsInBounds(x, y)) return false;
        return y > GetMinClickableY();
    }

    public bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int gridX = Mathf.RoundToInt(worldPos.x + width / 2f);
        int gridY = Mathf.RoundToInt(worldPos.y + height / 2f);
        return new Vector2Int(gridX, gridY);
    }

    // Getters
    public CellType GetCell(int x, int y)
    {
        return IsInBounds(x, y) ? grid[x, y] : CellType.Wall;
    }

    public int GetWidth() => width;
    public int GetHeight() => height;
    public int GetCellPixelSize() => cellPixelSize;
}