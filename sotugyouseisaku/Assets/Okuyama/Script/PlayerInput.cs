using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private static PlayerInput _instance = default;
    private static bool _initialized = true;

    /// <summary>���͕���</summary>
    private Vector2 _inputVector = default;
    /// <summary>���݂̃A�N�V�����}�b�v</summary>
    private ActionMapNames _currentMap = ActionMapNames.InGame;
    /// <summary>InputSystem�̐ݒ�N���X</summary>
    private GameInput _gameInputs;

    /// <summary>���͒����ǂ���</summary>
    private bool _isInputting = false;
    /// <summary>���͒���</summary>
    private Dictionary<InputType, Action> _onEnterInputDic = new Dictionary<InputType, Action>();
    /// <summary> ���͒� </summary>
    private Dictionary<InputType, Action> _onStayInputDic = new Dictionary<InputType, Action>();
    /// <summary> ���͉��� </summary>
    private Dictionary<InputType, Action> _onExitInputDic = new Dictionary<InputType, Action>();

    public static PlayerInput Instance
    {
        get
        {
            if (_instance == null)//null�Ȃ�C���X�^���X������
            {
                var obj = new GameObject("PlayerInput");
                var input = obj.AddComponent<PlayerInput>();
                input.Initialization();
                _instance = input;
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    /// <summary> ���͕��� </summary>
    public static Vector2 InputVector
    {
        get
        {
            if (_instance == null || !_initialized)
            {
                return Vector2.zero;
            }
            return _instance._inputVector;
        }
    }
    /// <summary>���͒����ǂ���</summary>
    public bool IsInputting { get => _isInputting; set => _isInputting = value; }

    private void Awake()
    {
        // Input Action�C���X�^���X����
        _gameInputs = new GameInput();

        InputInitialzie();
        ChangeActionMap(ActionMapNames.InGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        ResetInput();
    }

    /// <summary>
    /// �������������s��
    /// </summary>
    private void Initialization()
    {
        for (int i = 0; i < Enum.GetValues(typeof(InputType)).Length; i++)
        {
            _onEnterInputDic.Add((InputType)i, null);
            _onStayInputDic.Add((InputType)i, null);
            _onExitInputDic.Add((InputType)i, null);
        }
    }
    private void ResetInput()
    {
        if (_instance == null || !_initialized) { return; }
        for (int i = 0; i < Enum.GetValues(typeof(InputType)).Length; i++)
        {
            _instance._onEnterInputDic[(InputType)i] = null;
            _instance._onStayInputDic[(InputType)i] = null;
            _instance._onExitInputDic[(InputType)i] = null;
        }
        _initialized = false;
    }

    //�ȉ�InGame��Action

    /// <summary>�ړ�����</summary>
    private void OnMove(InputAction.CallbackContext context)
    {
        _inputVector = context.ReadValue<Vector2>();
        if (_inputVector == Vector2.zero) { _isInputting = false; }
        else { _isInputting = true; }
    }
    /// <summary>�s������ </summary>
    private void OnAction(InputAction.CallbackContext context)
    {
        _onEnterInputDic[InputType.Action1]?.Invoke();
    }
    /// <summary>���͂�����������</summary>
    private void InputInitialzie()
    {
        InGameInputInitialzie();
    }
    /// <summary>�C���Q�[���Ŏg�p������͂�o�^���� </summary>
    private void InGameInputInitialzie()
    {
        //�ړ�
        _gameInputs.InGame.Move.started += OnMove;
        _gameInputs.InGame.Move.performed += OnMove;
        _gameInputs.InGame.Move.canceled += OnMove;

        //�s��
        _gameInputs.InGame.Fire.performed += OnAction;
        _gameInputs.InGame.Fire.canceled += _ => _instance._onExitInputDic[InputType.Action1]?.Invoke();
    }

    /// <summary>���͂�؂�ւ���</summary>
    /// <param name="nextMap">���̓���</param>
    private void ChangeActionMap(ActionMapNames nextMap)
    {
        switch (nextMap)
        {
            case ActionMapNames.InGame:     //�C���Q�[���̓��͂��󂯎��
                _gameInputs.InGame.Enable();
                _gameInputs.UI.Disable();
                break;
            case ActionMapNames.UI:         //UI�̓��͂��󂯎��
                _gameInputs.UI.Enable();
                _gameInputs.InGame.Disable();
                break;
        }

        _currentMap = nextMap;
    }

    /// <summary>
    /// ���͎��̊֐����Z�b�g
    /// </summary>
    /// <param name="type">Input�^�C�v</param>
    /// <param name="action">�Ăт����֐�</param>
    public static void SetEnterInput(InputType type, Action action)
    {
        if (!_initialized) { return; }
        Instance._onEnterInputDic[type] += action;
    }

    public static void LiftEnterInput(InputType type, Action action)
    {
        if (!_initialized) { return; }
        Instance._onEnterInputDic[type] -= action;
    }

    /// <summary>���͒��̊֐����Z�b�g</summary>
    public static void SetStayInput(InputType type, Action action)
    {
        if (!_initialized) { return; }
        Instance._onStayInputDic[type] += action;
    }

    public static void LiftStayInput(InputType type, Action action)
    {
        if (_instance == null || !_initialized) { return; }
        _instance._onStayInputDic[type] -= action;
    }

    /// <summary>���͉������̊֐����Z�b�g</summary>
    public static void SetExitInput(InputType type, Action action)
    {
        if (_instance == null || !_initialized) { return; }
        _instance._onExitInputDic[type] += action;
    }
    public static void LiftExitInput(InputType type, Action action)
    {
        if (_instance == null || !_initialized) { return; }
        _instance._onExitInputDic[type] -= action;
    }
}
/// <summary>�A�N�V�����}�b�v </summary>
public enum ActionMapNames
{
    /// <summary>�C���Q�[���̓���</summary>
    InGame,
    /// <summary>UI�̓��� </summary>
    UI,
}

public enum InputType
{
    /// <summary> ������� </summary>
    Submit,
    /// <summary> �L�����Z������ </summary>
    Cancel,
    /// <summary> �ړ����� </summary>
    Move,
    /// <summary> �A�N�V�������͂P </summary>
    Action1,
    /// <summary> �A�N�V�������͂Q </summary>
    Action2,
    /// <summary> �A�N�V�������͂R </summary>
    Action3,
}
