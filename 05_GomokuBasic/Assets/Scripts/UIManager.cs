using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject winPanel; // 승리 패널
    public Text blackWinText; // 흑돌 승리 텍스트
    public Text whiteWinText; // 백돌 승리 텍스트
    public Button resetButton; // 리셋 버튼

    private GameManager gameManager; // GameManager 참조

    void Start()
    {
        // GameManager 찾기
        gameManager = FindObjectOfType<GameManager>();

        // 리셋 버튼에 이벤트 연결
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(OnResetButtonClicked);
        }

        // 게임 시작 시 UI 초기화
        HidePanel();
    }

    // 승리 패널 표시
    public void ShowWinPanel(bool isBlackWinner)
    {
        // 패널 활성화
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        // 승자에 따라 텍스트 활성화
        if (isBlackWinner)
        {
            if (blackWinText != null)
                blackWinText.gameObject.SetActive(true);

            if (whiteWinText != null)
                whiteWinText.gameObject.SetActive(false);
        }
        else
        {
            if (blackWinText != null)
                blackWinText.gameObject.SetActive(false);

            if (whiteWinText != null)
                whiteWinText.gameObject.SetActive(true);
        }
    }

    // 승리 패널 숨기기
    public void HidePanel()
    {
        // 패널 비활성화
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        // 모든 텍스트 비활성화
        if (blackWinText != null)
            blackWinText.gameObject.SetActive(false);

        if (whiteWinText != null)
            whiteWinText.gameObject.SetActive(false);
    }

    // 리셋 버튼 클릭 이벤트
    void OnResetButtonClicked()
    {
        if (gameManager != null)
        {
            gameManager.ResetGame();
        }
    }
}