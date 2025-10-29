using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool isTurn = false;
    private Vector3 startPosition;
    private Vector3 oldPosition;

    private int moveCnt = 0;
    private int TurnCnt = 0;
    private int SpawnCnt = 0;
    private bool isDie = false;

    private AudioSource sound;

    [Header("Ÿ�̸� ����")]
    public TimerBarController timerBarController; // Inspector ���� �ʿ�
    private float lastClickTime;
    private bool isFirstClick = true;
    private const float TIMEOUT_DURATION = 1.0f; // 1�� Ÿ�Ӿƿ�

    [Header("�ܻ� ȿ��")]
    public AfterimageManager afterimageManager; // Inspector���� ����

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        startPosition = transform.position;
        Init();
    }

    void Update()
    {
        // �� PlayerNamePanel�� ���� �ִٸ� ���� ����
        GameObject playerNamePanel = GameObject.Find("PlayerNamePanel");
        if (playerNamePanel != null && playerNamePanel.activeSelf)
        {
            return; // ���� �̸� �Է� ���̰ų� ���� ��ư �� ����
        }

        // �� InputField�� ��Ŀ���� �� ������ ���� ����
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
        {
            var selected = EventSystem.current.currentSelectedGameObject;
            if (selected.GetComponent<TMPro.TMP_InputField>() != null)
            {
                return;
            }
        }

        // �� ��/�� �Է� ó�� (A, D, ����Ű)
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            isTurn = true; // ����
            spriteRenderer.flipX = isTurn;
            CharMove();

            if (timerBarController != null)
                timerBarController.ResetTimer();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            isTurn = false; // ������
            spriteRenderer.flipX = isTurn;
            CharMove();

            if (timerBarController != null)
                timerBarController.ResetTimer();
        }

        // Ÿ�Ӿƿ� üũ
        CheckTimeout();
    }


    // ===============================================
    // [Ÿ�Ӿƿ� ���� ����]
    // ===============================================
    private void UpdateClickTime()
    {
        if (isDie) return;
        isFirstClick = false;
        lastClickTime = Time.time;
    }

    private void CheckTimeout()
    {
        if (isDie || isFirstClick)
            return;

        if (Time.time > lastClickTime + TIMEOUT_DURATION)
        {
            Debug.Log("Ÿ�Ӿƿ�! 1�� ���� �Է��� ���� ��� ó���˴ϴ�.");
            CharDie();
        }
    }

    // ===============================================
    // [�ʱ�ȭ ����]
    // ===============================================
    private void Init()
    {
        anim.SetBool("Die", false);
        transform.position = startPosition;
        oldPosition = startPosition;
        moveCnt = 0;
        TurnCnt = 0;
        SpawnCnt = 0;
        isTurn = false;
        spriteRenderer.flipX = isTurn;
        isDie = false;
        isFirstClick = true;
        lastClickTime = 0f;

        if (timerBarController != null)
            timerBarController.ResetTimer();

        // ĳ���� ��Ȱ �� �ܻ� ����
        if (afterimageManager != null)
            afterimageManager.StopAfterimage();
    }

    // ===============================================
    // [ĳ���� ����]
    // ===============================================
    public void CharMove()
    {
        if (isDie) return;

        //  ������ �ٽ� �κ�: �Է��� ���� �� �ð��� �����մϴ�. 
        UpdateClickTime();

        sound.Play();
        moveCnt++;
        MoveDirection();

        // �̵� �� �ܻ� ����
        if (afterimageManager != null)
            afterimageManager.StartAfterimage();

        if (isFailTurn())
        {
            CharDie();
            return;
        }

        if (moveCnt > 5)
            RespawnStair();

        GameManager.Instance.AddScore();
    }

    private void MoveDirection()
    {
        if (isTurn)
            oldPosition += new Vector3(-0.75f, 0.5f, 0);
        else
            oldPosition += new Vector3(0.75f, 0.5f, 0);

        transform.position = oldPosition;
        anim.SetTrigger("Move");
    }

    private bool isFailTurn()
    {
        bool result = false;

        if (GameManager.Instance.isTurn[TurnCnt] != isTurn)
            result = true;

        TurnCnt++;

        if (TurnCnt > GameManager.Instance.Stairs.Length - 1)
            TurnCnt = 0;

        return result;
    }

    private void RespawnStair()
    {
        GameManager.Instance.SpawnStair(SpawnCnt);
        SpawnCnt++;

        if (SpawnCnt > GameManager.Instance.Stairs.Length - 1)
            SpawnCnt = 0;
    }

    // ===============================================
    // [��� ó��]
    // ===============================================
    private void CharDie()
    {
        GameManager.Instance.GameOver();
        anim.SetBool("Die", true);
        isDie = true;

        // ��� �� �ܻ� ����
        if (afterimageManager != null)
            afterimageManager.StopAfterimage();
    }

    public void ButtonRestart()
    {
        Init();
        GameManager.Instance.Init();
        GameManager.Instance.InitStairs();
    }
}
