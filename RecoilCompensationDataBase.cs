namespace RecoilCompensator;

public struct RecoilCompensationDataBase
{
    
    #region AK47 Recoil compensation data
    public static readonly Vector2Decimal[] Ak47CompensationData =
    [
        new(-1.1m, 2.2m),
        new(-1.1m, 2.2m),
        new(-1.1m, 2.2m),
        new(-1.1m, 2.2m),
        
        new(1.1m, 4.4m),
        new(1.1m, 4.4m),
        new(1.1m, 4.4m),
        new(1.1m, 4.4m),
        
        new(1.1m, 7.7m),
        
        new(0, 8.8m),
        
        new(0, 7.7m),
        
        new(0, 8.8m),
        new(0, 8.8m),
        new(0, 8.8m),
        new(0, 8.8m),
        new(0, 8.8m),
        
        new(2.2m, 7.7m),
        new(2.2m, 7.7m),
        new(2.2m, 7.7m),
        new(2.2m, 7.7m),
        new(2.2m, 7.7m),
        new(2.2m, 7.7m),
        new(2.2m, 7.7m),
        new(2.2m, 7.7m),
        
        new(3.3m, 6.6m),
        new(3.3m, 6.6m),
        new(3.3m, 6.6m),
        new(3.3m, 6.6m),
        
        new(-4.4m, 4.4m),
        new(-4.4m, 4.4m),
        new(-4.4m, 4.4m),
        new(-4.4m, 4.4m),
        
        new(-8.8m, -1.1m),
        
        new(-9.9m, -1.1m),
        new(-9.9m, -1.1m),
        
        new(-8.8m, -1.1m),
        
        new(-6.6m, 1.1m),
        new(-6.6m, 1.1m),
        new(-6.6m, 1.1m),
        new(-6.6m, 1.1m),
        
        new(3.3m, 2.2m),
        
        new(4.4m, 2.2m),
        new(4.4m, 2.2m),
        
        new(3.3m, 2.2m),
        
        new(-3.3m, 1.1m),
        new(-3.3m, 1.1m),
        new(-3.3m, 1.1m),
        new(-3.3m, 1.1m),
        
        new(-7.7m, -2.2m),
        new(-7.7m, -2.2m),
        new(-7.7m, -2.2m),
        new(-7.7m, -2.2m),
        
        new(-1.1m, 1.1m),
        
        new(0, 1.1m),
        new(0, 1.1m),
        new(0, 1.1m),
        
        new(9.9m, 1.1m),
        
        new(11, 2.2m),
        new(11, 2.2m),
        
        new(9.9m, 1.1m),
        
        new(5.5m, 1.1m),
        
        new(5.5m, 2.2m),
        
        new(5.5m, 1.1m),
        
        new(5.5m, 2.2m),
        
        new(3.3m, 1.1m),
        new(3.3m, 1.1m),
        new(3.3m, 1.1m),
        new(3.3m, 1.1m),
        
        new(7.7m, -1.1m),
        new(7.7m, -1.1m),
        new(7.7m, -1.1m),
        new(7.7m, -1.1m),
        
        new(8.8m, -1.1m),
        new(8.8m, -1.1m),
        new(8.8m, -1.1m),
        new(8.8m, -1.1m),
        
        new(0m, 0m),
        new(0m, 0m),
        new(0m, 0m),
        
        new(-20.9m, 0m),
        
        new(1.1m, 0m),
        new(1.1m, 0m),
        new(1.1m, 0m),
        new(1.1m, 0m),
        
        new(-2.2m, 0m),
        new(-2.2m, 0m),
        new(-2.2m, 0m),
        new(-2.2m, 0m),
        new(-2.2m, 0m),
        new(-2.2m, 0m),
        new(-2.2m, 0m),
        new(-2.2m, 0m),
        
        new(5.5m, 0m),
        new(5.5m, 0m),
        new(5.5m, 0m),
        new(5.5m, 0m),
        
        new(2.2m, 0m),
        new(2.2m, 0m),
        new(2.2m, 0m),
        new(2.2m, 0m),
        
        new(-5.5m, 0m),
        new(-5.5m, 0m),
        new(-5.5m, 0m),
        new(-5.5m, 0m),
        
        new(-8.8m, 0m),
        new(-8.8m, 0m),
        new(-8.8m, 0m),
        new(-8.8m, 0m),
        
        new(-13.2m, -5.5m),
        new(-13.2m, -5.5m),
        new(-13.2m, -5.5m),
        new(-13.2m, -5.5m),
        
        new(-4.4m, 0),
        new(-4.4m, 0),
        new(-4.4m, 0),
        new(-4.4m, 0)
    ];
    public const float Ak47Sleep = 0.0233f;
    #endregion


    #region Methods
    public static void RewriteDataBaseForCurrentSensitivity()
    {
        new Thread(RewriteDataBaseForCurrentSensitivityInternal).Start();
    }

    private static void RewriteDataBaseForCurrentSensitivityInternal()
    {
        #region AK47
        // Временно. Моя сенса = 2.52 (макс. dpi на мышке). Возвращение к обычным значениям. Под сенсу 1. 
        for (var i = 0; i < Ak47CompensationData.Length; i++)
            Ak47CompensationData[i] /= 2.52m;
        
        for (var i = 0; i < Ak47CompensationData.Length; i++)
            Ak47CompensationData[i] *= (decimal)Config.Sensitivity;
        #endregion
    }
    #endregion
    
}
