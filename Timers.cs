namespace RecoilCompensator;

public static class Timers
{

    public static decimal SmokeTimerTime = 19;
    public static decimal WeaponTimerTime = 1;
    
    public static Timer SmokeTimer;
    public static Timer WeaponTimer;

    private const int Period = 100;
    
    
    public static void Start()
    {
        UserInputGrabber.KeyPressed += OnKeyPressed;
    }
    
    private static void OnKeyPressed(Keys key)
    {
        if (key == Keys.RightControl)
        {
            StartSmokeTimer();
        }
        
        // if (key == Keys.RightShift)
        // {
        //     _timerThread = new Thread(StartSmokeTimer);
        //     _timerThread.Start();
        // }
    }

    private static void SmokeTimerCallback(object obj)
    {
        SmokeTimerTime -= 0.1m;
        WindowController.QueueRedraw();
        
        if (SmokeTimerTime == 0)
        {
            SmokeTimer.Dispose();
            SmokeTimer = null;
        }
    }
    
    private static void WeaponTimerCallback(object obj)
    {
        WeaponTimerTime -= 0.1m;
        WindowController.QueueRedraw();
        
        if (WeaponTimerTime <= 0)
        {
            WeaponTimer.Dispose();
            WeaponTimer = null;
        }
    }
    
    private static void StartSmokeTimer()
    {
        if (SmokeTimer == null)
        {
            SmokeTimer = new Timer(SmokeTimerCallback, null, 0, Period);
            return;
        }

        if (SmokeTimer != null)
        {
            SmokeTimer.Dispose();
            SmokeTimer = null;
            SmokeTimerTime = 19m;
            StartSmokeTimer();
        }
    }

    public static void Stop()
    {
        StopWeaponTimer();
        StopSmokeTimer();
    }

    public static void StopWeaponTimer()
    {
        if (WeaponTimer != null)
        {
            WeaponTimer.Dispose();
            WeaponTimer = null;
        }
    }
    
    public static void StopSmokeTimer()
    {
        if (SmokeTimer != null)
        {
            SmokeTimer.Dispose();
            SmokeTimer = null;
        }
    }
    
    public static void StartWeaponTimer(decimal weaponTimerTime)
    {
        if (WeaponTimer == null)
        {
            WeaponTimerTime = weaponTimerTime;
            WeaponTimer = new Timer(WeaponTimerCallback, null, 0, Period);
            return;
        }

        if (WeaponTimer != null)
        {
            WeaponTimer.Dispose();
            WeaponTimer = null;
            WeaponTimerTime = weaponTimerTime;
            StartWeaponTimer(weaponTimerTime);
        }
    }
}
