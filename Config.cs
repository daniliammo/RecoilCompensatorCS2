using Cairo;

namespace RecoilCompensator;

public struct Config
{
    public static readonly Color CrosshairColor = new(0, 255, 127); // rgb(0, 255, 127)
    public const bool DrawCrosshair = true;

    public static readonly Vector2Int CenterOffset = new(0, -32);

    public const byte FontSize = 32;

    public const Mode StartMode = Mode.Mode0;

    public const bool CounterStrafe = false;
    
    public static readonly XdgSessionType SessionType = GetSessionType();

    public const bool DebugMode = true;
    
    public const float Sensitivity = 2.52f;

    public const Keys Rotate180 = Keys.Alt;
    // public const int OriginalDpi = 8000;
    // public const int UserDpi = 800;
    
    // public static int DropC4Bind = Keys.Q;
    
    
    private static XdgSessionType GetSessionType()
    {
        var xdgSessionType = Environment.GetEnvironmentVariable("XDG_SESSION_TYPE");
        
        if (xdgSessionType == "wayland")
            return XdgSessionType.Wayland;
        
        if (xdgSessionType == "x11")
            return XdgSessionType.X11;
        
        throw new Exception($"Unknown XdgSessionType: {xdgSessionType}");
    }
}