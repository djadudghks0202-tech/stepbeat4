using UnityEngine;
using UnityEngine.EventSystems; // �� ���ӽ����̽��� ���콺 �̺�Ʈ�� ó���ϴ� �� �ʿ��մϴ�!

// IPointerClickHandler �������̽��� �����մϴ�.
public class RightClickButton : MonoBehaviour, IPointerClickHandler
{
    // ��ư�� Ŭ������ ��(���콺 ��ư�� ������ ������ ��) ȣ��Ǵ� �Լ��Դϴ�.
    public void OnPointerClick(PointerEventData eventData)
    {
        // eventData.button�� ����Ͽ� � ���콺 ��ư�� ���ȴ��� Ȯ���մϴ�.
        // 0: ��Ŭ�� (Left)
        // 1: ��Ŭ�� (Right)
        // 2: �� Ŭ�� (Middle)

        // ���콺 ��Ŭ��(1��)���� Ȯ���մϴ�.
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("��Ŭ���� �����Ǿ� ��ư�� ���Ƚ��ϴ�!");

            //  ���⿡ **������ �����ϰ� ���� ���**�� �ۼ��ϸ� �˴ϴ�.
            // ���� ���:
            // GetComponent<Button>().onClick.Invoke(); // �Ϲ� Button�� OnClick �̺�Ʈ�� ���� ����
            // GameManager.Instance.OpenContextMenu(); // Ư�� ���� ����� ����
        }

        // �� ��ũ��Ʈ�� ��Ŭ��(0��)�� ���͵� �ƹ��͵� ���� �ʽ��ϴ�.
    }

    // Start()�� Update()�� �ʿ� �����Ƿ� ������ ������, ���ܵμŵ� ��������ϴ�.
}