using Cairo;


namespace RecoilCompensator;

public static class Utils
{
    
    public static Color Invert(this Color color) => new(255 - color.R, 255 - color.G, 255 - color.B);
    
}
