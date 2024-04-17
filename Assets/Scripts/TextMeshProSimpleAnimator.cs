// ===================================
//
// Copyright(c) 2020 Copocopo All rights reserved.
// https://github.com/coposuke/TextMeshProAnimator
//
// ===================================


using UnityEngine;


/// <summary>
/// ��������A�j���[�V����
/// </summary>
public class TextMeshProSimpleAnimator : MonoBehaviour
{
    /// <summary>
    /// �A�j���[�V���������ǂ���
    /// </summary>
    public bool isAnimating { get; private set; } = false;

    /// <summary>
    /// 1����������̕\�����x
    /// </summary>
    public float speedPerCharacter = 0.1f;

    /// <summary>
    /// �����Đ�
    /// </summary>
    [SerializeField]
    private bool playOnEnable = false;

    /// <summary>
    /// ���[�v���邩�ǂ���
    /// </summary>
    public bool isLoop = false;

    /// <summary>
    /// TextMeshPro
    /// </summary>
    private TMPro.TMP_Text text = default;

    /// <summary>
    /// �A�j���[�V��������
    /// </summary>
    private float time = 0.0f;


    /// <summary>
    /// Override Unity Function
    /// </summary>
    private void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }

    /// <summary>
    /// Override Unity Function
    /// </summary>
    private void Start()
    {
        if (this.playOnEnable) { Play(); }
    }

    /// <summary>
    /// Override Unity Function
    /// </summary>
    private void OnEnable()
    {
        if (this.playOnEnable) { Play(); }
    }

    /// <summary>
    /// Override Unity Function
    /// </summary>
    private void Update()
    {
        if (this.isAnimating)
            UpdateAnimation(Time.deltaTime);
    }

    /// <summary>
    /// �A�j���[�V�����Đ��J�n
    /// </summary>
    public void Play()
    {
        if (this.isAnimating)
            return;

        this.time = 0.0f;
        this.isAnimating = true;
        this.text.ForceMeshUpdate(true);
        UpdateAnimation(0.0f);
    }

    /// <summary>
    /// �A�j���[�V���������I��
    /// </summary>
    public void Finish()
    {
        if (!this.isAnimating)
            return;

        this.isAnimating = false;
        this.text.maxVisibleCharacters = this.text.textInfo.characterCount;
        this.time = 0.0f;
    }

    /// <summary>
    /// �A�j���[�V�����X�V
    /// </summary>
    private void UpdateAnimation(float deltaTime)
    {

        int maxVisibleCharacters = this.text.textInfo.characterCount;
        float maxTime = (maxVisibleCharacters + 1) * speedPerCharacter;

        this.time += deltaTime;

        int visibleCharacters = Mathf.Clamp(Mathf.FloorToInt(time / speedPerCharacter), 0, maxVisibleCharacters);
        if (text.maxVisibleCharacters != visibleCharacters)
            text.maxVisibleCharacters = visibleCharacters;

        if (this.time > maxTime)
        {
            if (this.isLoop)
            {
                time = time % maxTime;
            }
            else
            {
                Finish();
            }
        }
    }
}
