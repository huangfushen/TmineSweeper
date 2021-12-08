using UnityEngine;

namespace Script
{
    public class Brick : MonoBehaviour
    {
        public bool isMine;
        public bool isCover = true;
    
        private GameManager _gameManager;
        private BrickManager _brickManager;
        private Vector2 _positionInGrid;
        // Start is called before the first frame update
        void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            // 判断是否为开始游戏
            if (_gameManager.state != GameManager.State.GameStart) return;
            
            _brickManager = FindObjectOfType<BrickManager>();
            _gameManager = FindObjectOfType<GameManager>();
            var localX = _brickManager.width*50/2;
            var localY = _brickManager.width*50/2;
            Vector2 originPoint = _brickManager.transform.position;
            var position = transform.position;
            int x = (int)(position.x);
            int y = (int)(position.y);
            _positionInGrid = new Vector2(x, y);
            int i = (int)((position.x-originPoint.x+localX)/50);
            int j = (int)((position.y-originPoint.y+localY)/50);
            isMine = Random.value <= _brickManager.isMinePercent; //设置本身是否为地雷
            BrickManager.GridDate[i,j] = this; //将自己添加到网格数据中去
        }

        
        private void OnMouseUp()
        {
            if(isMine)
            {
                _brickManager.ShowAllMine();
                _gameManager.Lost();
            }
            else
            {
                _brickManager.FloodFill((int)_positionInGrid.x, (int)_positionInGrid.y, new bool[_brickManager.width, _brickManager.height]);
                _gameManager.CheckIsWill();
            }
        }
    }
}
