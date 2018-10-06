using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DiHinh
{
    class OHinh
    {
        public static int _width, _height;
        private int _x, _y;
        private int _indexImage;
        private Color _colorBorder;
        private int _row, _col;
        private Image _img;
        public int IndexImage
        {
            get { return _indexImage; }
            //set { _indexImage = value; }
        }
        
        public int Col
        {
            get { return _col; }
            set { _col = value; }
        }

        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }

        

        public OHinh(int row, int col, Image img, int indexImage)
        {
            _row = row; _col = col;
            _x = _col * _width; _y = _row * _height;
            _img = img; _colorBorder = Color.White;
            _indexImage = indexImage;
        }
        ~OHinh()
        {
            _img.Dispose();
        }
        public void SetWhiteCell()
        {
            _img.Dispose();
            _img = new Bitmap(_width, _height);
            _colorBorder = Color.BlueViolet;
        }
        
        public void DrawCellImage(Graphics _grp)
        {
            _grp.DrawImage(_img, _x, _y);
            _grp.DrawRectangle(new Pen(new SolidBrush(_colorBorder)), _x, _y, _width, _height);
        }
        public void Swap(OHinh _Ohinh)
        {
            Image tempImage = _img; _img = _Ohinh._img; _Ohinh._img = tempImage;
            Color tempColor = _colorBorder; _colorBorder = _Ohinh._colorBorder; _Ohinh._colorBorder = tempColor;
            int tempIndexImage = _indexImage; _indexImage = _Ohinh._indexImage; _Ohinh._indexImage = tempIndexImage;
        }
    }
}
