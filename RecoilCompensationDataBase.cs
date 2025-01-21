namespace RecoilCompensator;

public static class RecoilCompensationDataBase
{
    public static readonly Vector2[] Ak47CompensationData =
    [
        new(-1.1f, 2.2f),
        new(-1.1f, 2.2f),
        new(-1.1f, 2.2f),
        new(-1.1f, 2.2f),
        
        new(1.1f, 4.4f),
        new(1.1f, 4.4f),
        new(1.1f, 4.4f),
        new(1.1f, 4.4f),
        
        new(1.1f, 7.7f),
        
        new(0, 8.8f),
        
        new(0, 7.7f),
        
        new(0, 8.8f),
        new(0, 8.8f),
        new(0, 8.8f),
        new(0, 8.8f),
        new(0, 8.8f),
        
        new(2.2f, 7.7f),
        new(2.2f, 7.7f),
        new(2.2f, 7.7f),
        new(2.2f, 7.7f),
        new(2.2f, 7.7f),
        new(2.2f, 7.7f),
        new(2.2f, 7.7f),
        new(2.2f, 7.7f),
        
        new(3.3f, 6.6f),
        new(3.3f, 6.6f),
        new(3.3f, 6.6f),
        new(3.3f, 6.6f),
        
        new(-4.4f, 4.4f),
        new(-4.4f, 4.4f),
        new(-4.4f, 4.4f),
        new(-4.4f, 4.4f),
        
        new(-8.8f, -1.1f),
        
        new(-9.9f, -1.1f),
        new(-9.9f, -1.1f),
        
        new(-8.8f, -1.1f),
        
        new(-6.6f, 1.1f),
        new(-6.6f, 1.1f),
        new(-6.6f, 1.1f),
        new(-6.6f, 1.1f),
        
        new(3.3f, 2.2f),
        
        new(4.4f, 2.2f),
        new(4.4f, 2.2f),
        
        new(3.3f, 2.2f),
        
        new(-3.3f, 1.1f),
        new(-3.3f, 1.1f),
        new(-3.3f, 1.1f),
        new(-3.3f, 1.1f),
        
        new(-7.7f, -2.2f),
        new(-7.7f, -2.2f),
        new(-7.7f, -2.2f),
        new(-7.7f, -2.2f),
        
        new(-1.1f, 1.1f),
        
        new(0, 1.1f),
        new(0, 1.1f),
        new(0, 1.1f),
        
        new(9.9f, 1.1f),
        
        new(11, 2.2f),
        new(11, 2.2f),
        
        new(9.9f, 1.1f),
        
        new(5.5f, 1.1f),
        
        new(5.5f, 2.2f),
        
        new(5.5f, 1.1f),
        
        new(5.5f, 2.2f),
        
        new(3.3f, 1.1f),
        new(3.3f, 1.1f),
        new(3.3f, 1.1f),
        new(3.3f, 1.1f),
        
        new(7.7f, -1.1f),
        new(7.7f, -1.1f),
        new(7.7f, -1.1f),
        new(7.7f, -1.1f),
        
        new(8.8f, -1.1f),
        new(8.8f, -1.1f),
        new(8.8f, -1.1f),
        new(8.8f, -1.1f),
        
        new(0f, 0f),
        new(0f, 0f),
        new(0f, 0f),
        
        new(-20.9f, 0f),
        
        new(1.1f, 0f),
        new(1.1f, 0f),
        new(1.1f, 0f),
        new(1.1f, 0f),
        
        new(-2.2f, 0f),
        new(-2.2f, 0f),
        new(-2.2f, 0f),
        new(-2.2f, 0f),
        new(-2.2f, 0f),
        new(-2.2f, 0f),
        new(-2.2f, 0f),
        new(-2.2f, 0f),
        
        new(5.5f, 0f),
        new(5.5f, 0f),
        new(5.5f, 0f),
        new(5.5f, 0f),
        
        new(2.2f, 0f),
        new(2.2f, 0f),
        new(2.2f, 0f),
        new(2.2f, 0f),
        
        new(-5.5f, 0f),
        new(-5.5f, 0f),
        new(-5.5f, 0f),
        new(-5.5f, 0f),
        
        new(-8.8f, 0f),
        new(-8.8f, 0f),
        new(-8.8f, 0f),
        new(-8.8f, 0f),
        
        new(-13.2f, -5.5f),
        new(-13.2f, -5.5f),
        new(-13.2f, -5.5f),
        new(-13.2f, -5.5f),
        
        new(-4.4f, 0),
        new(-4.4f, 0),
        new(-4.4f, 0),
        new(-4.4f, 0)
    ];

    public const float Ak47Sleep = 0.0233f;


    public static void RewriteDataBaseForCurrentSensitivity()
    {
        new Thread(RewriteDataBaseForCurrentSensitivityInternal).Start();
    }

    private static void RewriteDataBaseForCurrentSensitivityInternal()
    {
        // Временно. Моя сенса = 2.52 (макс. dpi на мышке). Возвращение к обычным значениям. Под сенсу 1. 
        for (var i = 0; i < Ak47CompensationData.Length; i++)
            Ak47CompensationData[i] /= 2.52f;
        
        for (var i = 0; i < Ak47CompensationData.Length; i++)
            Ak47CompensationData[i] *= Config.Sensitivity;
    }
    
}
