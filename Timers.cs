namespace RecoilCompensator;

public static class Timers
{

    public static decimal SmokeTimerTime = 19;
    public static decimal WeaponTimerTime = 0;
    
    public static Timer smokeTimer;
    public static Timer weaponTimer;

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
            smokeTimer.Dispose();
            smokeTimer = null;
        }
    }
    
    private static void WeaponTimerCallback(object obj)
    {
        WeaponTimerTime -= 0.1m;
        WindowController.QueueRedraw();
        
        if (WeaponTimerTime <= 0)
        {
            weaponTimer.Dispose();
            weaponTimer = null;
        }
    }
    
    private static void StartSmokeTimer()
    {
        if (smokeTimer == null)
        {
            smokeTimer = new Timer(SmokeTimerCallback, null, 0, Period);
            return;
        }

        if (smokeTimer != null)
        {
            smokeTimer.Dispose();
            smokeTimer = null;
            SmokeTimerTime = 19m;
            StartSmokeTimer();
        }
    }

    public static void Stop()
    {
        if (smokeTimer != null)
        {
            smokeTimer.Dispose();
            smokeTimer = null;
        }
        
        if (weaponTimer != null)
        {
            weaponTimer.Dispose();
            weaponTimer = null;
        }
    }

    public static void StartWeaponTimer(decimal weaponTimerTime)
    {
        if (weaponTimer == null)
        {
            WeaponTimerTime = weaponTimerTime;
            weaponTimer = new Timer(WeaponTimerCallback, null, 0, Period);
            return;
        }

        if (weaponTimer != null)
        {
            weaponTimer.Dispose();
            weaponTimer = null;
            WeaponTimerTime = weaponTimerTime;
            StartWeaponTimer(weaponTimerTime);
        }
    }
}
