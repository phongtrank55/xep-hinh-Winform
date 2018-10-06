using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace DiHinh
{
    class DiHinh
    {
        private MOVE _oldDirection;
        private Direction _direction;
        private BanChoi _banchoi;
        private OHinh[,] _arrayCellImage;
        private Image _img;
        private int _rowCellWhite, _colCellWhite;
        private string[] huongdi;
        public Image Img
        {
            get { return _img; }
            set { _img = value; }
        }
        public struct HuongDi
        {
            public string[] huong;
            public int len;
        }
        public DiHinh(System.Windows.Forms.Panel pnl)
        {
            _banchoi = new BanChoi(pnl.Width, pnl.Height);
            _arrayCellImage = new OHinh[BanChoi._rowNumber, BanChoi._colNumber];
        }
       
        private Image TachHinh(Image img, int width, int height, int xsrc, int ysrc)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage((Image)bmp);
            g.DrawImage(img, 0, 0, new Rectangle(xsrc, ysrc, width, height), GraphicsUnit.Pixel);
            g.Dispose();
            return (Image)bmp;
        }

        public void SplitCellImage()
        {
            int indexImage = 0; // Sắp thứ tự  chỉ số các hình
            for (int i = 0; i < BanChoi._rowNumber; i++)
                for (int j = 0; j < BanChoi._colNumber; j++)
                {
                    _arrayCellImage[i, j] = new OHinh(i, j, TachHinh(_img, OHinh._width, OHinh._height, j * OHinh._width, i * OHinh._height), indexImage++);
                }
        }
        public void VeBanChoi(Graphics grp)
        {
            
            //grp.DrawImage(_bmp, 0, 0);
            for (int i = 0; i < BanChoi._rowNumber; i++)
                for (int j = 0; j < BanChoi._colNumber; j++)
                    _arrayCellImage[i, j].DrawCellImage(grp);
                    //grp.DrawRectangle(new Pen(new SolidBrush(Color.Red)), MangOHinh[3, 0].Col * OHinh._width, MangOHinh[3, 0].Row * OHinh._height, OHinh._width, OHinh._height);
            //_banchoi.DrawCell(grp);
            //_cellWhite.DrawCellImage(grp);
        }
        #region Sắp xếp và Chơi
        private void SwapWhiteCell(int rowCellImage, int colCellImage)
        {
            _arrayCellImage[rowCellImage, colCellImage].Swap(_arrayCellImage[_rowCellWhite, _colCellWhite]);
            _rowCellWhite = rowCellImage; _colCellWhite = colCellImage;
        }
        public bool Move(int Xmouse, int Ymouse)
        {
            int rowCellImage = Ymouse / OHinh._height;
            int colCellImage = Xmouse / OHinh._width;
            if (rowCellImage == _rowCellWhite && colCellImage == _colCellWhite) return false;
            //Biện luận ô trắng có ở quanh hay k và đổi chỗ
            if ((_rowCellWhite == rowCellImage && (colCellImage == _colCellWhite-1 ||colCellImage == _colCellWhite+1 )) || (_colCellWhite == colCellImage && (rowCellImage == _rowCellWhite-1 ||rowCellImage == _rowCellWhite+1 )))
            {
                SwapWhiteCell(rowCellImage, colCellImage);
                return true;
            }
            return false;
        }
        public void ChooseCellWhite()
        {
            _rowCellWhite = new Random().Next(0, BanChoi._rowNumber);
            _colCellWhite = new Random().Next(0, BanChoi._colNumber);
            _arrayCellImage[_rowCellWhite, _colCellWhite].SetWhiteCell();
        }
       public bool IsWin()
        {
            int indexImage=0;
            //Duyệt xem chỉ số hình ảnh đã đúng thứ tự chưa
            for (int i = 0; i < BanChoi._rowNumber; i++)
                for (int j = 0; j < BanChoi._colNumber; j++)
                    if (_arrayCellImage[i, j].IndexImage != indexImage++)
                        return false;
            return true;
        }
        private void MoveUP()
       {
           SwapWhiteCell(_rowCellWhite - 1, _colCellWhite);
        }
        private void MoveDown()
        {
            SwapWhiteCell(_rowCellWhite + 1, _colCellWhite);
        }
        private void MoveLeft()
        {
            SwapWhiteCell(_rowCellWhite, _colCellWhite - 1);
        }
        private void MoveRight()
        {
            SwapWhiteCell(_rowCellWhite, _colCellWhite + 1);
        }
        private void TimHuongDi()
        {
            if (_rowCellWhite == 0 && _colCellWhite == 0)
            {
                //Goc Trên  trái
                _direction = new Direction(false, true, false, true);
                    
            }
            else if (_rowCellWhite == 0 && _colCellWhite == BanChoi._colNumber - 1)
            {
                //Góc trên phải
                _direction = new Direction(false, true, true, false);
            }
            else if (_colCellWhite == 0 && _rowCellWhite == BanChoi._rowNumber - 1)
            {
                //Góc dưới trái
                _direction = new Direction(true, false, false, true); 
            }
            else if (_colCellWhite == BanChoi._colNumber - 1 && _rowCellWhite == BanChoi._rowNumber - 1)
            {
                //Góc dưới phải
                _direction = new Direction(true, false, true, false);

            }
            //Ô trắng trên hàng đầu thì không không thể di chuyển lên

            else if (_rowCellWhite == 0)
            {
                _direction = new Direction(false, true, true, true);
            }
            //Ở dòng cuối thì k di đc dưới 
            else if (_rowCellWhite == BanChoi._rowNumber - 1)
            {
                _direction = new Direction(true, false, true, true);
            }
            //Ở cột trái thì k di đc trái
            else if (_colCellWhite == 0)
            {
                _direction = new Direction(true, true, false, true);
            }
            //ở cột phả thì k di đc phải
            else if (_colCellWhite == BanChoi._colNumber - 1)
            {
                _direction = new Direction(true, true, true, false);
            }
            //SX trộn các ô
            else
            {
                _direction = new Direction(true, true, true, true);
            }
            
        }
        public void MixCell()
        {
            _oldDirection = MOVE.NULL;
            for (int i = 0; i < 50; i++)
            {
                TimHuongDi();
                //Nếu hướng trước đã đi thì k đc đi hướg ngược lại
                if (_oldDirection == MOVE.UP) _direction.Del(MOVE.DOWN);
                if (_oldDirection == MOVE.DOWN) _direction.Del(MOVE.UP);
                if (_oldDirection == MOVE.LEFT) _direction.Del(MOVE.RIGHT);
                if (_oldDirection == MOVE.RIGHT) _direction.Del(MOVE.LEFT);
                //Chọn ngãu nhiêm 1 hướng đi
                _oldDirection = _direction.RandomChooseDirection();
                switch(_oldDirection)
                {
                    case MOVE.DOWN: MoveDown(); break;
                    case MOVE.LEFT: MoveLeft(); break;
                    case MOVE.RIGHT: MoveRight(); break;
                    default: MoveUP(); break;
                }
            }
        }

        #endregion
    }
}
