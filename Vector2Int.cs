namespace RecoilCompensator;


public class Vector2Int(int x, int y)
{
    public int X = x;
    public int Y = y;
    
    public static Vector2Int operator /(Vector2Int vector, float d)
    {
        return new Vector2Int((int)(vector.X / d), (int)(vector.Y / d));
    }
    
    public static Vector2Int operator +(Vector2Int vector, Vector2Int other)
    {
        return new Vector2Int(vector.X + other.X, vector.Y + other.Y);
    }
}
