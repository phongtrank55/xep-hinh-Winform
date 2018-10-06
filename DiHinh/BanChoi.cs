using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DiHinh
{
    class BanChoi
    {

        public  static int _width, _height, _rowNumber =5, _colNumber=4;
        public BanChoi(int width, int height)
        {
            _width = width; _height = height;
            OHinh._width = _width / _colNumber;
            OHinh._height = _height / _rowNumber;
        }

        //public void DrawCell(Graphics _grp)
        //{
        //    Pen _pen=new Pen(new SolidBrush(Color.Red));
        //    for (int i = 0; i <= _rowNumber; i++)
        //    {
        //        _grp.DrawLine(_pen, 0, i * OHinh._height, _width, i * OHinh._height);
        //    }
        //    for (int i = 0; i <= _colNumber; i++)
        //    {
        //        _grp.DrawLine(_pen, i * OHinh._width, 0, i * OHinh._width, _height);
        //    }
        //}

    }
}
