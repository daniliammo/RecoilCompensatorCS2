namespace RecoilCompensator;


public struct Vector2(float x, float y)
{
    
    public float X = x;
    public float Y = y;
    
    public static Vector2 operator *(Vector2 vector, float scalar)
    {
        return new Vector2(vector.X * scalar, vector.Y * scalar);
    }
    
    public static Vector2 operator +(Vector2 vector, Vector2 vector2)
    {
        return new Vector2(vector.X += vector2.X, vector.Y += vector2.Y);
    }
    
    public static Vector2 operator -(Vector2 vector, Vector2 vector2)
    {
        return new Vector2(vector.X += vector2.X, vector.Y += vector2.Y);
    }
    
    public static Vector2 operator /(Vector2 vector, float d)
    {
        return new Vector2(vector.X / d, vector.Y / d);
    }
    
}
