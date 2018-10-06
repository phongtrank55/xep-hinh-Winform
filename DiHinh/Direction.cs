using System;
using System.Collections.Generic;
using System.Text;

namespace DiHinh
{
    public enum MOVE { UP, DOWN, LEFT, RIGHT, NULL}
    class Direction
    {
        private string[] _direction;
        private int _numDirection;
        public Direction(bool UP, bool DOWN, bool LEFT, bool RIGHT)
        {
            _direction = new string[4];
           _numDirection = 0;
           if (UP) _direction[_numDirection++] = "UP";
           if (DOWN) _direction[_numDirection++] = "DOWN";
           if (LEFT) _direction[_numDirection++] = "LEFT";
           if (RIGHT) _direction[_numDirection++] = "RIGHT";
        }
        public void Del(MOVE _dir)
        {
            string huongdi;
            switch (_dir)
            {
                case MOVE.UP: huongdi = "UP"; break;
                case MOVE.DOWN: huongdi = "DOWN"; break;
                case MOVE.LEFT: huongdi = "LEFT"; break;
                default: huongdi = "RIGHT"; break;
            }
            int i = 0;
            while (i < _numDirection && _direction[i] != huongdi)
                i++;
            if (i == _numDirection) return;
            _numDirection--;
            while (i < _numDirection)
            {
                _direction[i] = _direction[i+1];
                i++;
            }
        }
        public MOVE RandomChooseDirection()
        {
            int ranDirection = new Random().Next(1000) % _numDirection;
            switch(_direction[ranDirection])
            {
                case "UP": return MOVE.UP;
                case "DOWN": return MOVE.DOWN;
                case "LEFT": return MOVE.LEFT;
                default: return MOVE.RIGHT;
            }
        }
    }
}
