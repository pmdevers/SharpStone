using SharpStone.Maths;

namespace Tetris.Objects;
public class Game
{
    private static Random rnd = new Random();

    public const int BlockSize = 30;
    public const int Rows = 20;
    public const int Columns = 10;
    const int NumPieces = 7;

    private Piece activePiece;
    private Piece nextPiece;
    private Piece activePieceCopy;

    public Square[,] Maingrid { get; } = new Square[Rows, Columns];
    Square[,] nextPieceGrid = new Square[5, 5];

    public bool Killed { get; set; }
    public bool Paused { get; set; }

    private bool deleteLines;
    private int linesCleared;
    private int shapesCount;
    public int Timer { get; } = 5;

    public Game()
    {
        Restart();
        Timer = 5;
    }

    public void Update()
    {
        if (MoveCollision(0))
        {
            if (activePiece.Y <= 2)
            {
                Killed = true;
            }
            else
            {
                UpdateActiveAfterCollision();
                CheckLine();
                if (deleteLines)
                {
                    ClearLine();
                }
                GenNextPiece();

                ClearNextPieceGrid();
                UpdateNextPieceGrid();

                UpdateActivePiece();
            }
        }
        else
        {
            FixActivePiece();
            activePiece.Y += 1;
            UpdateActivePiece();
        }
    }

    public void Restart()
    {
        ClearMainGrid();
        ClearNextPieceGrid();
        linesCleared = 0;
        shapesCount = 0;

        Killed = false;
        Paused = false;

        deleteLines = false;

        activePiece = new()
        {
            X = Columns / 2,
            Y = 0
        };

        UpdateActivePiece();

        nextPiece = new()
        {
            X = Columns / 2,
            Y = 0
        };
        UpdateNextPieceGrid();
    }

    public void ClearMainGrid()
    {
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                Maingrid[r, c] = new()
                {
                    IsFilled = false,
                    IsActive = false
                };
            }
        }
    }

    private void FixActivePiece()
    {
        var transNext = activePiece.Rotations();
        for (int i = 0; i < 8; i += 2)
        {
            var squareNext = Maingrid[
                activePiece.Y + transNext[i + 1],
                activePiece.X + transNext[i]];
            squareNext.IsFilled = false;
            squareNext.IsActive = false;
        }
    }

    private void UpdateActivePiece()
    {
        var transNext = activePiece.Rotations();
        for (int i = 0; i < 8; i += 2)
        {
            var squareNext = Maingrid[
                activePiece.Y + transNext[i + 1],
                activePiece.X + transNext[i]];
            squareNext.IsFilled = true;
            squareNext.IsActive = true;
            squareNext.Color = activePiece.Color;
        }
    }

    private void UpdateNextPieceGrid()
    {
        var transNext = nextPiece.Rotations();
        for (int i = 0; i < 8; i += 2)
        {
            var squareNext = Maingrid[
                nextPiece.Y + transNext[i],
                nextPiece.X + transNext[i + 1]];
            squareNext.IsFilled = true;
            squareNext.IsActive = true;
            squareNext.Color = nextPiece.Color;
        }
    }

    public void ClearNextPieceGrid()
    {
        for (int r = 0; r < 5; r++)
        {
            for (int c = 0; c < 5; c++)
            {
                nextPieceGrid[r, c] = new()
                {
                    IsFilled = false,
                    IsActive = false,
                    Color = Color.Magenta
                };
            }
        }
    }

    private void GenNextPiece()
    {
        activePiece = nextPiece;
        nextPiece = new Piece((PieceType)(rnd.Next() % NumPieces))
        {
            X = Columns / 2,
            Y = 0,
        };
        shapesCount++;
    }

    private void ClearLine()
    {
        for (int r = Rows - 1; r > 0; r--)
        {
            if (Maingrid[r, 0].ToBeDeleted)
            {
                for (int r2 = r; r2 > 0; r2--)
                {
                    for (int c = 0; c < Columns -1; c++)
                    {
                        Maingrid[r2, c].IsFilled = Maingrid[r2 - 1, c].IsFilled;
                        Maingrid[r2, c].IsActive = Maingrid[r2 - 1, c].IsActive;
                        Maingrid[r2, c].ToBeDeleted = Maingrid[r2 - 1, c].ToBeDeleted;
                        Maingrid[r2, c].Color = Maingrid[r2 - 1, c].Color;
                    }
                }
                r++;
            }
        }
        deleteLines = false;
    }

    private void CheckLine()
    {
        for (int r = 0; r < Rows; r++)
        {
            bool fullRow = false;
            for (int c = 0; c < Columns; c++)
            {
                var square = Maingrid[r, c];
                if (square.IsFilled)
                {
                    fullRow = true;
                }
                else
                {
                    fullRow = false;
                    break;
                }
            }
            if (fullRow)
            {
                for (int c = 0; c < Columns; c++)
                {
                    Maingrid[r, c].ToBeDeleted = true;
                }
                deleteLines = true;
                linesCleared++;
            }
        }
    }

    private void UpdateActiveAfterCollision()
    {
        var trans = activePiece.Rotations();
        for (int i = 0; i < 8; i += 2)
        {
            var square = Maingrid[activePiece.Y + trans[i + 1], activePiece.X + trans[i]];
            square.IsActive = false;
        }
    }

    private bool MoveCollision(int dir)
    {
        int x, y;
        var trans = activePiece.Rotations();
        for (int i = 0; i < 8; i += 2)
        {
            x = activePiece.X + trans[i];
            y = activePiece.Y + trans[i + 1];
            if (dir == 0)
                y += 1;
            else
                x += dir;

            if (x >= Columns || y >= Rows || x < 0 || Maingrid[y, x].IsFilled && !Maingrid[y, x].IsActive)
                return true;
        }
        return false;
    }

    public void Move(int dir)
    {
        if(MoveCollision(dir))
        { 
            return; 
        }

        FixActivePiece();
        activePiece.X += dir;
        UpdateActivePiece();
    }

    public void Rotate(int dir)
    {
        activePieceCopy = activePiece.Copy();
        activePieceCopy.RotatePiece(dir);

        if(CanRotate())
        {
            FixActivePiece();
            activePiece.RotatePiece(dir);
            UpdateActivePiece();
        }
    }

    public bool CanRotate() => !RotationCollision();

    private bool RotationCollision()
    {
        int x, y;
        var trans = activePieceCopy.Rotations();
        for (int i = 0; i < 8; i += 2)
        {
            x = activePieceCopy.X + trans[i];
            y = activePieceCopy.Y + trans[i + 1];

            if(x >= Columns || y >= Rows || x < 0 || (Maingrid[y, x].IsFilled && !Maingrid[y, x].IsActive))
            {
                return true;
            }
        }
        return false;
    }

    internal void Speed(int dir)
    {
        activePieceCopy = activePiece.Copy();
        if (RotationCollision())
        {
            return;
        }
        FixActivePiece();
        activePiece.Y += dir;
        UpdateActivePiece();
    }
}
