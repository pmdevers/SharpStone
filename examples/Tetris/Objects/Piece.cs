using SharpStone.Maths;

namespace Tetris.Objects;
public enum PieceType : int
{
    Square = 0,
    VerticalLine = 1,
    TPiece = 2,
    LPiece = 3,
    ReversedLPiece = 4,
    ZPiece = 5,
    ReversedZPiece = 6,
}

public class Piece
{
    private static Random rnd = new Random();

    public int X { get; set; }
    public int Y { get; set; }
    public Color Color { get; set; }
    public PieceType Type { get; private set; }
    public int Rotation { get; private set; }

    public Piece() : this((PieceType)(rnd.Next() % 7))
    {
    }

    public Piece(PieceType type)
    {
        Type = type;
        Color = type switch
        {
            PieceType.Square => new Color(1f, 1f, 0),
            PieceType.VerticalLine => new Color(0.5f, 0.5f, 0.5f),
            PieceType.TPiece => new Color(0f, 1f, 1f),
            PieceType.LPiece => new Color(1f, 0f, 1f),
            PieceType.ReversedLPiece => new Color(1f, 0f, 0),
            PieceType.ZPiece => new Color(1f, 0f, 1f),
            PieceType.ReversedZPiece => new Color(0f, 0.8f, 0),
            _ => throw new NotSupportedException()
        };
        Rotation = 0;
    }

    public Piece Copy()
    {
        var copy = new Piece();
        copy.Type = Type;
        copy.Rotation = Rotation;
        copy.X = X;
        copy.Y = Y;
        return copy;
    }

    public int[] Rotations()
        => gamePieces[(int)Type][Rotation];

    public void RotatePiece(int dir)
    {
        if (dir > 0)
        {
            if (Rotation == 3)
                Rotation = 0;
            else
                Rotation += dir;
        }
        else
        {
            if (Rotation == 0) Rotation = 3;
            else Rotation += dir;
        }
    }

    const int numPieces = 7;    // Number of different pieces 
    const int numRotations = 4; // Number of turns for each piece 
    const int numSpaces = 8;    // Memory capacity for storing information about each piece 

    private int[][][] gamePieces = [
        [
            [0, 0, 1, 0, 0, 1, 1, 1], // Square 
            [0, 0, 1, 0, 0, 1, 1, 1],
            [0, 0, 1, 0, 0, 1, 1, 1],
            [0, 0, 1, 0, 0, 1, 1, 1],
        ],
        [
            [0, 0, 0, 1, 0, 2, 0, 3], // Vertical line 
            [0, 0, 1, 0, 2, 0, 3, 0],
            [0, 0, 0, 1, 0, 2, 0, 3],
            [0, 0, 1, 0, 2, 0, 3, 0],
        ],
        [
            [0, 0, 0, 1, 1, 1, 0, 2], // T piece 
            [1, 0, 0, 1, 1, 1, 2, 1],
            [0, 1, 1, 0, 1, 1, 1, 2],
            [0, 0, 1, 0, 2, 0, 1, 1]
        ],
        [[0, 0, 1, 0, 0, 1, 0, 2], // L piece 
            [0, 0, 0, 1, 1, 1, 2, 1],
            [1, 0, 1, 1, 0, 2, 1, 2],
            [0, 0, 1, 0, 2, 0, 2, 1]
        ],
        [[0, 0, 1, 0, 1, 1, 1, 2], // Reverse L piece 
            [0, 0, 1, 0, 2, 0, 0, 1],
            [0, 0, 0, 1, 0, 2, 1, 2],
            [2, 0, 0, 1, 1, 1, 2, 1]
        ],
        [[0, 0, 0, 1, 1, 1, 1, 2], // Z piece 
            [1, 0, 2, 0, 0, 1, 1, 1],
            [0, 0, 0, 1, 1, 1, 1, 2],
            [1, 0, 2, 0, 0, 1, 1, 1]
        ],
        [[1, 0, 0, 1, 1, 1, 0, 2], // Reverse Z piece 
            [0, 0, 1, 0, 1, 1, 2, 1],
            [1, 0, 0, 1, 1, 1, 0, 2],
            [0, 0, 1, 0, 1, 1, 2, 1]
        ]
    ];
}

