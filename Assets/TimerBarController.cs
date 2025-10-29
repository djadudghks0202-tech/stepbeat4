using UnityEngine;
using UnityEngine.UI; // Image ������Ʈ�� ����ϱ� ���� �ʿ��մϴ�!

public class TimerBarController : MonoBehaviour
{
    // Inspector â���� FillBar Image ������Ʈ�� ������ ����
    public Image fillImage;

    // Player ��ũ��Ʈ�� ������ Ÿ�Ӿƿ� �ð� (1.0f)
    private const float TIMEOUT_DURATION = 1.0f;

    // ���� ��� �ð� (0.0f���� 1.0f���� ����)
    private float currentTime = 0f;

    // ������ ���۵� �� ù Ŭ�� ������ Ȯ�� (Player ��ũ��Ʈ�� isFirstClick�� ����)
    private bool isStarted = false;

    void Start()
    {
        // ���� �� �������� ������ ä���Ӵϴ�.
        fillImage.fillAmount = 1f;
    }

    void Update()
    {
        // ������ ���۵� �� ù Ŭ���� �߻��ؾ� Ÿ�̸Ӱ� �۵��մϴ�.
        if (!isStarted) return;

        // ���� �ð��� 1��(TIMEOUT_DURATION)�� ���� ������ŵ�ϴ�.
        currentTime += Time.deltaTime;

        // ��� �ð��� Ÿ�Ӿƿ� �ð��� �ʰ��ϸ� �� �̻� ������Ű�� �ʽ��ϴ�.
        if (currentTime > TIMEOUT_DURATION)
        {
            currentTime = TIMEOUT_DURATION;
        }

        // ������ ���� fillAmount�� ����մϴ�.
        // ���� �ð� / ��ü �ð�(1.0��)���� 0.0 ~ 1.0 ������ ������ ���ϰ�,
        // 1���� �� ������ ���� �ð��� �������� �پ��� ����ϴ�.
        float fillRatio = 1f - (currentTime / TIMEOUT_DURATION);

        // �̹����� fillAmount�� �����մϴ�.
        fillImage.fillAmount = fillRatio;
    }

    // �ܺ�(Player ��ũ��Ʈ)���� ȣ���Ͽ� Ÿ�̸Ӹ� �ʱ�ȭ�ϴ� �Լ�
    public void ResetTimer()
    {
        // �ð��� �����ִ��� �ٽ� 0���� �����ϵ��� �ʱ�ȭ�մϴ�.
        currentTime = 0f;

        // Ÿ�̸Ӱ� �۵� ������ ǥ���մϴ�.
        isStarted = true;
    }
}