namespace RecoilCompensator;


public struct Vector2Decimal(decimal x, decimal y)
{
    
    public decimal X = x;
    public decimal Y = y;
    
    
    public static Vector2Decimal operator *(Vector2Decimal vector, decimal scalar)
    {
        return new Vector2Decimal(vector.X * scalar, vector.Y * scalar);
    }
    
    public static Vector2Decimal operator +(Vector2Decimal vector, Vector2Decimal vector2)
    {
        return new Vector2Decimal(vector.X += vector2.X, vector.Y += vector2.Y);
    }
    
    public static Vector2Decimal operator -(Vector2Decimal vector, Vector2Decimal vector2)
    {
        return new Vector2Decimal(vector.X += vector2.X, vector.Y += vector2.Y);
    }
    
    public static Vector2Decimal operator /(Vector2Decimal vector, decimal d)
    {
        return new Vector2Decimal(vector.X / d, vector.Y / d);
    }
    
}
