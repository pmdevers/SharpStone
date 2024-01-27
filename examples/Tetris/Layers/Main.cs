using SharpStone.Core;
using SharpStone.Events;
using SharpStone.Graphics;
using SharpStone.Gui;
using SharpStone.Maths;
using Tetris.Objects;

namespace Tetris.Layers;
internal class Main() : Layer("Main Game Layer")
{
    private int vpWidth = Game.Columns * Game.BlockSize;
    private int vpHeigth = Game.Rows * Game.BlockSize;
    private int margin = 1;

    private Game _game = new();
    private Camera _camera = new OrthographicCamera(0, Game.Columns + 1, Game.Rows + 1, 0);
    public override void OnAttach()
    {
        UserInterface.Visible = false;

        _game = new Game();
        _game.Restart();
    }
    
    private float ellapsedTime;
    public override void OnUpdate(float v)
    {
        RenderCommand.SetViewPort(0, 0, vpWidth, vpHeigth);
        RenderCommand.SetClearColor(Color.Black);
        RenderCommand.Clear();

        
        if (_game.Killed)
        {
            _game.Paused = true;
            _game.ClearMainGrid();
            _game.ClearNextPieceGrid();
        }
        else if (!_game.Paused)
        {
            ellapsedTime += v;
            if (ellapsedTime > _game.Timer)
            {
                _game.Update();
                ellapsedTime = 0;
            }

            Renderer.BeginScene(_camera);

            //Renderer.DrawQuad(new(0,0), new(Game.Rows * Game.BlockSize, Game.Columns * Game.BlockSize), Color.Magenta);

            for (int r = 0; r < Game.Rows; r++)
            {
                for (int c = 0; c < Game.Columns; c++)
                {
                    var square = _game.Maingrid[r, c];
                    if (square.IsFilled)
                    {
                        Renderer.DrawQuad(new(margin + c + .1f, r + .1f), new(1f, 1f), square.Color);
                    }
                    else
                    {
                        square.Color = new Color(.1f, .2f, .2f);
                        Renderer.DrawQuad(new(margin + c, r), new(1f, 1f), square.Color);
                    }
                }
            }

            

            Renderer.EndScene();
        }
    }

    public override void OnEvent(Event @event)
    {
        var dispatcher = new EventDispatcher(@event);
        dispatcher.Dispatch<KeyPressedEvent>(KeyPressed);
    }

    private bool KeyPressed(KeyPressedEvent @event)
    {
        if (_game.Paused && _game.Killed)
        {
            if (@event.KeyCode == 13)
            {
                _game.Killed = false;
                _game.Restart();
            }
        }
        else
        {
            if (@event.KeyCode == 'p' || @event.KeyCode == 27)
            {
                _game.Paused = !_game.Paused;
            }
            else if (!_game.Paused && !_game.Killed)
            {
                switch (@event.KeyCode)
                {
                    case 1073741903:
                        // RIGHT
                        _game.Move(1);
                        break;
                    case 1073741904:
                        _game.Move(-1);
                        break;
                        // LEFT
                    case 1073741906:
                        _game.Rotate(1);
                        break;
                        // UP
                    case 1073741905:
                        _game.Speed(1);
                        // DOWN
                        break;

                    default:
                        break;
                }
            }
        }
        return true;
    }
}
