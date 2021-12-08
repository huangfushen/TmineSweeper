using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class BrickManager : MonoBehaviour
    {
    
        public int width;
        public int height;
        public static Brick[,] GridDate;
        // 雷的概率
        public float isMinePercent = 0.15f;
        public GameObject brick;
        private GameManager _gameManager;
        public Sprite[] Sprite;
        public Sprite mineImage;
        void Update()
        {
            _gameManager = FindObjectOfType<GameManager>();
            GameObject gamePage = GameObject.Find("GamePage");
            // 判断是否为开始游戏
            if (_gameManager.state == GameManager.State.GameLoad)
            {
                var localX = 50 * width/2;
                var localY = 50 * height/2;
                GridDate = new Brick[width, height]; 
                Vector2 originPoint = transform.position;
                for (int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        GameObject item = GameObject.Instantiate(brick, new Vector2(originPoint.x + x*50 - localX, originPoint.y + y*50 - localY), Quaternion.identity);
                        item.transform.SetParent(gamePage.transform);
                    }
            
                }
                _gameManager.state = GameManager.State.GameStart;
            }
        }
        // 显示所有地雷
        public void ShowAllMine()
        {
            foreach(Brick brick in GridDate)
            {
                if (brick.isMine)
                {
                    brick.gameObject.GetComponent<Image>().sprite = mineImage;
                    brick.isCover = false;
                }
            }
        }
        
        //校验是否为地雷
        private bool BrickIsMine(int x, int y)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                return GridDate[x, y].isMine;
            }
            return false;
        }
        
        
        // 砖块附件的地雷数
        public int NearbyMineCount(int x, int y)
        {
            int count = 0;
            if(BrickIsMine(x, y + 1)) // 上方
            {
                count++;
            }
            if(BrickIsMine(x - 1, y + 1)) // 左上
            {
                count++;
            }
            if (BrickIsMine(x + 1, y + 1))  // 右上
            {
                count++;
            }
            if(BrickIsMine(x - 1, y))  // 左
            {
                count++;
            }
            if (BrickIsMine(x + 1, y))  //  右
            {
                count++;
            }
            if(BrickIsMine(x - 1, y - 1))  // 左下
            {
                count++;
            }
            if(BrickIsMine(x, y - 1))  // 下
            {
                count++;
            }
            if(BrickIsMine(x + 1, y - 1))  // 右下
            {
                count++;
            }

            return count;
        }
        // 通过地雷数修改sprite
        public void ChangeSpriteByNearbyMineCount(int x, int y, int count)
        {
            GridDate[x, y].GetComponent<SpriteRenderer>().sprite = Sprite[count];
            GridDate[x, y].isCover = false;
        }
        
        public void FloodFill(int x, int y, bool[,] visited)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                if (visited[x, y])
                {
                    return;
                }
                visited[x, y] = true;

                int count = NearbyMineCount(x, y);
                ChangeSpriteByNearbyMineCount(x, y, count);
                if (count > 0)
                {
                    return;
                }

                FloodFill(x, y + 1, visited);  //  上
                FloodFill(x - 1, y, visited);  //  左
                FloodFill(x, y - 1, visited);  //  下
                FloodFill(x + 1, y, visited);  //  右
            }

        }
        
        // 校验是否胜利
        public bool IsWill()
        {
            foreach(Brick brick in GridDate)
            {
                if(brick.isCover && !brick.isMine)      
                {
                    return false;
                }
            }
            return true;
        }
    }
}
