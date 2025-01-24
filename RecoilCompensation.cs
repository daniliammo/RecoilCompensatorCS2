namespace RecoilCompensator;


public static class RecoilCompensation
{
    public static Slot[] Slots = new Slot[3];

    public static Mode CurrentMode = Config.StartMode;
    
    public static Team Team = Team.Ct;
    public static bool IsPaused;

    public static int CurrentSlot;

    public static Weapon CurrentWeapon => Slots[CurrentSlot].Weapon;

    private static bool _w;
    private static bool _a;
    private static bool _s;
    private static bool _d;

    public static bool IsRealSlotUnknown;
    

    public static void Start()
    {
        RecoilCompensationDataBase.RewriteDataBaseForCurrentSensitivity();
        
        VirtualInput.Start();
        UserInputGrabber.Start();
        Timers.Start();
        
        UserInputGrabber.MouseWheel += OnMouseWheel;
        UserInputGrabber.KeyPressed += OnKeyPressed;
        UserInputGrabber.KeyReleased += OnKeyReleased;
        UserInputGrabber.ButtonPressed += OnButtonPressed;
        UserInputGrabber.ButtonReleased += OnButtonReleased;
        
        ResetWeapons();
    }
    
    private static void Pause(bool isPaused)
    {
        IsPaused = isPaused;
        WindowController.QueueRedraw();
            
        if (IsPaused)
            CurrentWeapon.StopRecoilCompensation();
    }
    
    private static void OnKeyPressed(Keys key)
    {
        if (key == Keys.Escape && !IsPaused)
            Pause(true);

        if (key == Keys.AltGr)
            Pause(!IsPaused);

        if (IsPaused) return;
        
        // Mode0
        if (CurrentMode == Mode.Mode0)
        {
            if (key == Keys.F1)
            {
                Slots[0].Weapon.AbortPrepare();
                
                Slots[0].Weapon = new Weapon(WeaponType.Ak47);
                ChangeSlot(0);
                return;
            }
            
            if (key == Keys.F2)
            {
                Slots[0].Weapon.AbortPrepare();
                
                Slots[0].Weapon = new Weapon(WeaponType.M4A4);
                ChangeSlot(0);
                return;
            }

            if (key == Keys.F3)
            {
                Slots[0].Weapon.AbortPrepare();
                
                Slots[0].Weapon = new Weapon(WeaponType.M4A1S);
                ChangeSlot(0);
                return;
            }
        }
        
        // Mode1
        if (CurrentMode == Mode.Mode1)
        {
            if (key == Keys.F1)
            {
                Slots[0].Weapon = new Weapon(WeaponType.Mp7);
                ChangeSlot(0);
                return;
            }

            if (key == Keys.F2)
            {
                Slots[0].Weapon = new Weapon(WeaponType.Mp9);
                ChangeSlot(0);
                return;
            }

            if (key == Keys.F3)
            {
                Slots[0].Weapon = new Weapon(WeaponType.Mac10);
                ChangeSlot(0);
                return;
            }
        }
        
        // Mode2
        if (CurrentMode == Mode.Mode2)
        {
            if (key == Keys.F1)
            {
                Slots[0].Weapon = new Weapon(WeaponType.Galil);
                ChangeSlot(0);
                return;
            }

            if (key == Keys.F2)
            {
                Slots[0].Weapon = new Weapon(WeaponType.Famas);
                ChangeSlot(0);
                return;
            }
        }

        
        // Mode 3
        if (CurrentMode == Mode.Mode3)
        {
            if (key == Keys.F1)
            {
                Slots[1].Weapon = new Weapon(WeaponType.Usp);
                ChangeSlot(1);
                return;
            }

            if (key == Keys.F2)
            {
                Slots[1].Weapon = new Weapon(WeaponType.FiveSeven);
                ChangeSlot(1);
                return;
            }
        }
        
        if (key == Keys.R)
            CurrentWeapon.Reload();
        
        // Переключение режима
        if (key == Keys.F5)
            CurrentMode = Mode.Mode0;
        
        if (key == Keys.F6)
            CurrentMode = Mode.Mode1;
        
        if (key == Keys.F7)
            CurrentMode = Mode.Mode2;
        
        // Сброс
        if (key == Keys.F8)
        {   // Игрок жив. Новый раунд. Новые боеприпасы
            RestartRound();
            return;
        }

        if (key == Keys.F9)
        {   // Игрок мертв. Убираем оружие
            ResetWeapons();
            GC.Collect();
            return;
        }
        
        // Player Movement
        if (key == Keys.W)
        {
            
            return;
        }
        
        if (key == Keys.A)
        {
            
            return;
        }
        
        if (key == Keys.S)
        {
            
            return;
        }
        
        if (key == Keys.D)
        {
            
            return;
        }

        // Numerics
        if (key == Keys.Key1)
        {
            ChangeSlot(0, false);
            return;
        }
        
        if (key == Keys.Key2)
        {
            ChangeSlot(1, false);
            return;
        }
        
        if (key == Keys.Key3)
        {
            ChangeSlot(2, false);
            return;
        }
        
        if (key == Keys.Key4)
        {
            ChangeSlot(3, false);
            return;
        }
        
        if (key == Keys.Key5)
        {
            ChangeSlot(4, false);
            return;
        }
    }
    
    private static void ChangeSlot(int slot, bool pressKey = true, bool fromWheel = false)
    {
        if (slot > Slots.Length - 1)
        {
            IsRealSlotUnknown = true;
            WindowController.QueueRedraw();
            return;
        }

        if (slot != CurrentSlot)
        {
            CurrentWeapon.AbortPrepare(); // Прежнее оружие не готово.
            Slots[slot].Weapon.Prepare(); // Начать подготовку нового оружия.
        }
        
        /*
        from /usr/include/linux/input-event-codes.h
        #define KEY_ESC                 1
        #define KEY_1                   2
        #define KEY_2                   3
        #define KEY_3                   4
        .....
        1 это Escape. 2 это Key_1. Так как отчет слотов начинаеться с 0 прибавляем 2.
        */
        
        if (pressKey)
            VirtualInput.PressKey(slot + 2);
        
        if (slot > 2 && fromWheel)
            IsRealSlotUnknown = true;
        
        if (!fromWheel)
            IsRealSlotUnknown = false;
        
        CurrentSlot = slot;
        
        WindowController.QueueRedraw();
    }
    
    public static void OnKeyReleased(Keys key)
    {
        if (IsPaused) return;
        
        
        if (key == Keys.W)
        {
            if (Config.CounterStrafe)
            {
                if (!_w || !_s)
                {
                    CounterStrafe(KeysDataBase.KeyS);
                    _s = true;
                    _w = true;
                    return;
                }
                
                _a = false;
                _d = false;
            }

            return;
        }
        
        if (key == Keys.A)
        {
            if (Config.CounterStrafe)
            {
                if (!_a || !_d)
                {
                    CounterStrafe(KeysDataBase.KeyD);
                    _a = true;
                    _d = true;
                    return;
                }
                
                _a = false;
                _d = false;
            }

            return;
        }
        
        if (key == Keys.S)
        {
            if (Config.CounterStrafe)
            {
                if (!_s || !_w)
                {
                    CounterStrafe(KeysDataBase.KeyW);
                    _s = true;
                    _w = true;
                    return;
                }
                
                _s = false;
                _w = false;
            }

            return;
        }
        
        if (key == Keys.D)
        {
            if (Config.CounterStrafe)
            {
                if (!_a || !_d)
                {
                    CounterStrafe(KeysDataBase.KeyA);
                    _a = true;
                    _d = true;
                    return;
                }
                
                _a = false;
                _d = false;
            }

            return;
        }
        Reset();
    }

    private static void Reset()
    {

    }

    private static void CounterStrafe(int key)
    {
        if (CurrentSlot == 0)
            VirtualInput.PressKeyWithDelay(key, 130);
        
        if (CurrentSlot == 1)
            VirtualInput.PressKeyWithDelay(key, 95);
    }
    
    private static void RestartRound()
    {
        foreach (var slot in Slots)
            slot.Weapon.Restart();
        
        GC.Collect(-1, GCCollectionMode.Aggressive);
    }
    
    public static void OnMouseWheel(bool isUp)
    {
        if (IsPaused) return;
        
        
        if (isUp)
        {
            if (CurrentSlot == 0)
            {
                IsRealSlotUnknown = true;
                return;
            }

            ChangeSlot(CurrentSlot - 1, false, true);
            return;
        }

        if (CurrentSlot > 5)
            return;
        ChangeSlot(CurrentSlot + 1, false, true);
    }
    
    private static void OnButtonPressed(Keys button)
    {
        if (IsPaused) return;
        
        if (button == Keys.LeftMouse)
        {
            if (CurrentWeapon.IsReady)
                CurrentWeapon.StartRecoilCompensation();

            else
                CurrentWeapon.WeaponReady += CurrentWeapon.StartRecoilCompensation;
        }
    }
    
    private static void OnButtonReleased(Keys button)
    {
        if (IsPaused) return;
        
        if (button == Keys.LeftMouse)
            CurrentWeapon.StopRecoilCompensation();
    }

    private static void ResetWeapons()
    {
        Slots[0] = new Slot(WeaponSlotType.Rifle, new Weapon(WeaponType.Unknown));
        Slots[1] = new Slot(WeaponSlotType.Pistol, new Weapon(WeaponType.Usp));
        Slots[2] = new Slot(WeaponSlotType.Knife, new Weapon(WeaponType.Knife));
        
        // // Слоты пустышки. Программа не знает точно есть ли эти слоты у игрока.
        // Slots[3] = new Slot(WeaponSlotType.Grenade, new Weapon(WeaponType.Knife));
        // Slots[4] = new Slot(WeaponSlotType.C4, new Weapon(WeaponType.Knife));
    }
    
    public static void Stop()
    {
        VirtualInput.Stop();
        UserInputGrabber.Stop();
        Timers.Stop();
    }
    
}
