using HelloWorld.Layers;
using SharpStone;
using SharpStone.Setup;

var builder = Application.Create(args)
    .AddLayer<MainLayer>();

var app = builder.Build();

app.Run();