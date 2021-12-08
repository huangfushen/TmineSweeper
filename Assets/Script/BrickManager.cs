using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    
    public int width;
    public int height;
    public static Brick[,] GridDate;
    // 雷的概率
    public float isMinePercent = 0.15f;
    public GameObject brick;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        _gameManager = FindObjectOfType<GameManager>();
        GameObject gamePage = GameObject.Find("GamePage");
        // 判断是否为开始游戏
        if (_gameManager.state == GameManager.State.GameLoad)
        {
            GridDate = new Brick[width, height]; 
            Vector2 originPoint = transform.position;
            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    GameObject item = Instantiate(brick, new Vector2(originPoint.x + x, originPoint.y + y), Quaternion.identity);
                    item.transform.parent = gamePage.transform;
                }
            
            }
            _gameManager.state = GameManager.State.GameStart;
        }
    }
    
    
}
