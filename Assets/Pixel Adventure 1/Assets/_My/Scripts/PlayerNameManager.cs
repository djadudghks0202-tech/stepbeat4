using UnityEngine;
using TMPro;

public class PlayerNameManager : MonoBehaviour
{
    [Header("UI ����")]
    public GameObject playerNamePanel;     // �̸� �Է� �г�
    public GameObject blurBackground;      // �� ���
    public TMP_InputField nameInputField;  // �Է� �ʵ�

    private const string playerNameKey = "PlayerName";

    void Start()
    {
        // �׻� �̸� �Է�â ǥ�� (���� �̸��� ������ �ҷ�����)
        blurBackground.SetActive(true);
        playerNamePanel.SetActive(true);
        Time.timeScale = 0f; // ���� �Ͻ�����

        string oldName = PlayerPrefs.GetString(playerNameKey, "");
        if (!string.IsNullOrEmpty(oldName))
        {
            nameInputField.text = oldName;
        }
    }

    // ��ư Ŭ�� �� ����
    public void OnConfirmName()
    {
        string playerName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("[PlayerNameManager] �̸��� ��� ����");
            return;
        }

        // �̸� ����
        PlayerPrefs.SetString(playerNameKey, playerName);
        PlayerPrefs.Save();

        // �Է�â �� �� ��� ����
        if (blurBackground != null) blurBackground.SetActive(false);
        if (playerNamePanel != null) playerNamePanel.SetActive(false);

        // ���� �簳
        Time.timeScale = 1f;

        Debug.Log($"[PlayerNameManager] �̸� ���� �Ϸ�: {playerName}, ���� ���۵�");
    }

    public static string GetPlayerName()
    {
        return PlayerPrefs.GetString(playerNameKey, "Guest");
    }
}
