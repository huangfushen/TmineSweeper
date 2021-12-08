using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public bool isMine;
    public bool isCover = true;
    
    private GameManager _gameManager;
    private BrickManager _brickManager;
    private Vector2 positionInGrid;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        // 判断是否为开始游戏
        if (_gameManager.state == GameManager.State.GameStart)
        {
            _brickManager = FindObjectOfType<BrickManager>();
            _gameManager = FindObjectOfType<GameManager>();
            Vector2 originPoint = _brickManager.transform.position;
            int x = (int)(transform.position.x - originPoint.x);
            int y = (int)(transform.position.y - originPoint.y);
            positionInGrid = new Vector2(x, y);

            isMine = Random.value <= _brickManager.isMinePercent; //设置本身是否为地雷
            BrickManager.GridDate[x, y] = this; //将自己添加到网格数据中去
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
