using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private static PlayerInput _instance = default;
    private static bool _initialized = true;

    /// <summary>入力方向</summary>
    private Vector2 _inputVector = default;
    /// <summary>現在のアクションマップ</summary>
    private ActionMapNames _currentMap = ActionMapNames.InGame;
    /// <summary>InputSystemの設定クラス</summary>
    private GameInput _gameInputs;

    /// <summary>入力中かどうか</summary>
    private bool _isInputting = false;
    /// <summary>入力直後</summary>
    private Dictionary<InputType, Action> _onEnterInputDic = new Dictionary<InputType, Action>();
    /// <summary> 入力中 </summary>
    private Dictionary<InputType, Action> _onStayInputDic = new Dictionary<InputType, Action>();
    /// <summary> 入力解除 </summary>
    private Dictionary<InputType, Action> _onExitInputDic = new Dictionary<InputType, Action>();

    public static PlayerInput Instance
    {
        get
        {
            if (_instance == null)//nullならインスタンス化する
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

    /// <summary> 入力方向 </summary>
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
    /// <summary>入力中かどうか</summary>
    public bool IsInputting { get => _isInputting; set => _isInputting = value; }

    private void Awake()
    {
        // Input Actionインスタンス生成
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
    /// 初期化処理を行う
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

    //以下InGameのAction

    /// <summary>移動処理</summary>
    private void OnMove(InputAction.CallbackContext context)
    {
        _inputVector = context.ReadValue<Vector2>();
        if (_inputVector == Vector2.zero) { _isInputting = false; }
        else { _isInputting = true; }
    }
    /// <summary>行動処理 </summary>
    private void OnAction(InputAction.CallbackContext context)
    {
        _onEnterInputDic[InputType.Action1]?.Invoke();
    }
    /// <summary>入力を初期化する</summary>
    private void InputInitialzie()
    {
        InGameInputInitialzie();
    }
    /// <summary>インゲームで使用する入力を登録する </summary>
    private void InGameInputInitialzie()
    {
        //移動
        _gameInputs.InGame.Move.started += OnMove;
        _gameInputs.InGame.Move.performed += OnMove;
        _gameInputs.InGame.Move.canceled += OnMove;

        //行動
        _gameInputs.InGame.Fire.performed += OnAction;
        _gameInputs.InGame.Fire.canceled += _ => _instance._onExitInputDic[InputType.Action1]?.Invoke();
    }

    /// <summary>入力を切り替える</summary>
    /// <param name="nextMap">次の入力</param>
    private void ChangeActionMap(ActionMapNames nextMap)
    {
        switch (nextMap)
        {
            case ActionMapNames.InGame:     //インゲームの入力を受け取る
                _gameInputs.InGame.Enable();
                _gameInputs.UI.Disable();
                break;
            case ActionMapNames.UI:         //UIの入力を受け取る
                _gameInputs.UI.Enable();
                _gameInputs.InGame.Disable();
                break;
        }

        _currentMap = nextMap;
    }

    /// <summary>
    /// 入力時の関数をセット
    /// </summary>
    /// <param name="type">Inputタイプ</param>
    /// <param name="action">呼びたい関数</param>
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

    /// <summary>入力中の関数をセット</summary>
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

    /// <summary>入力解除時の関数をセット</summary>
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
/// <summary>アクションマップ </summary>
public enum ActionMapNames
{
    /// <summary>インゲームの入力</summary>
    InGame,
    /// <summary>UIの入力 </summary>
    UI,
}

public enum InputType
{
    /// <summary> 決定入力 </summary>
    Submit,
    /// <summary> キャンセル入力 </summary>
    Cancel,
    /// <summary> 移動入力 </summary>
    Move,
    /// <summary> アクション入力１ </summary>
    Action1,
    /// <summary> アクション入力２ </summary>
    Action2,
    /// <summary> アクション入力３ </summary>
    Action3,
}
