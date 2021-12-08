using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BrickManager _brickManager;
    public enum State {
        GameBefore, 
        GameLoad,
        GameStart,
        GameOver
    }
    // 状态
    public State state;
    // Start is called before the first frame update
    void Start()
    {
        state = State.GameBefore;
    }

    // Update is called once per frame
    void Update()
    {
        // 根据状态判断执行操作
        // switch (state)
        // {
        //     case State.GameBefore:
        //     
        //         break;
        //     case State.GameStart:
        //
        //         break;
        //     case State.GameOver:
        //
        //         break;
        // }
    }

    public void CheckDifficulty()
    {
        _brickManager = FindObjectOfType<BrickManager>();
        var buttonSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        switch (buttonSelf.name)
        {
            case "easy":
                _brickManager.width = 4;
                _brickManager.height = 4;
                break;
            case "normal":
                _brickManager.width = 8;
                _brickManager.height = 8;
                break;
            case "difficulty":
                _brickManager.width = 12;
                _brickManager.height = 12;
                break;
            case "hell":
                _brickManager.width = 16;
                _brickManager.height = 16;
                break;
        }

        state = State.GameLoad;
        GameObject beforePage = GameObject.Find("BeforePage");
        beforePage.SetActive(false);
        
    }
}
