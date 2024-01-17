using HelloWorld.Layers;

using SharpStone;
using SharpStone.Core;
using SharpStone.Setup;

var builder = Application.Create(args)
    .WindowConfig(() => new() { 
        Title = "Test",
        Width = 1280,
        Height = 720,
        
    })
    .AddLayer<MainLayer>();

var app = builder.Build();

app.Run();