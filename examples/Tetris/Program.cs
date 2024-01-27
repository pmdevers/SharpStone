using SharpStone;
using Tetris.Layers;

Application
    .Create(o => {
        o.Name = "SharpStone Tetris";
        o.Width = 400;
        o.Height = 600;
     })
    .PushLayer(new Main())
    .Run();
