using System.Diagnostics;

namespace RecoilCompensator;


public class Weapon()
{
    
    public WeaponType WeaponType;
    public WeaponSlotType SlotType;
    public int CurrentAmmo;
    public readonly int MaxAmmo;
    public int Magazine;
    public readonly int MaxMagazine;
    public decimal PreparationTime;
    public decimal ReloadTime;
    public float FireRate;
    
    public delegate void OnReady();
    public event OnReady WeaponReady;
    
    public bool IsReady;
    public bool IsSilent = false;
    
    private bool _isRecoilCompensationNeeded;
    private bool _abortingPrepare;
    
    
    public Weapon(WeaponType weaponType) : this()
    {
        WeaponType = weaponType;

        switch (WeaponType)
        {
            case WeaponType.Ak47:
                MaxAmmo = 30;
                MaxMagazine = 90;
                Magazine = MaxMagazine;
                PreparationTime = 1m;
                ReloadTime = 1.43m;
                FireRate = 0.01f;
                CurrentAmmo = MaxAmmo;
                break;
            case WeaponType.M4A4:
                MaxAmmo = 30;
                MaxMagazine = 90;
                Magazine = MaxMagazine;
                PreparationTime = 0.5m;
                ReloadTime = 3.1m;
                FireRate = 0.0111f;
                CurrentAmmo = MaxAmmo;
                break;
            case WeaponType.M4A1S:
                MaxAmmo = 20;
                MaxMagazine = 80;
                Magazine = MaxMagazine;
                IsSilent = true;
                PreparationTime = 0.5m;
                ReloadTime = 3.1m;
                FireRate = 0.01f;
                CurrentAmmo = MaxAmmo;
                break;
            case WeaponType.Galil:
                MaxAmmo = 35;
                MaxMagazine = 90;
                Magazine = MaxMagazine;
                PreparationTime = 0.5m;
                ReloadTime = 3;
                FireRate = 0.0111f;
                CurrentAmmo = MaxAmmo;
                break;
            case WeaponType.Famas:
                MaxAmmo = 25;
                MaxMagazine = 90;
                Magazine = MaxMagazine;
                PreparationTime = 0.5m;
                ReloadTime = 3.3m;
                FireRate = 0.0111f;
            
                CurrentAmmo = MaxAmmo;
                break;
            case WeaponType.Usp:
                MaxAmmo = 12;
                MaxMagazine = 24;
                Magazine = MaxMagazine;
                PreparationTime = 0.5m;
                ReloadTime = 2.1m;
                FireRate = 0.1f;
                CurrentAmmo = MaxAmmo;
                break;
            case WeaponType.FiveSeven:
                break;
            case WeaponType.Mp9:
                MaxAmmo = 30;
                MaxMagazine = 120;
                Magazine = MaxMagazine;
                PreparationTime = 0.5m;
                ReloadTime = 2.1m;
                FireRate = 0.014283333f;
                CurrentAmmo = MaxAmmo;
                break;
            case WeaponType.Mp7:
                MaxAmmo = 30;
                MaxMagazine = 120;
                Magazine = MaxMagazine;
                PreparationTime = 0.5m;
                ReloadTime = 3.1m;
                FireRate = 0.013333333f;
                CurrentAmmo = MaxAmmo;
                break;
            case WeaponType.Knife:
                MaxAmmo = 255;
                MaxMagazine = 255;
                Magazine = MaxMagazine;
                PreparationTime = 1m;
                ReloadTime = 2;
                FireRate = 1;
                CurrentAmmo = MaxAmmo;
                break;
            case WeaponType.Unknown:
                MaxAmmo = 0;
                MaxMagazine = 0;
                Magazine = MaxMagazine;
                PreparationTime = 5.2m;
                ReloadTime = 1.2m;
                FireRate = 0;
                CurrentAmmo = MaxAmmo;
                break;
            case WeaponType.Mac10:
                break;
        }
    }

    public void StartRecoilCompensation()
    {
        new Thread(StartRecoilCompensationInternal).Start();
    }

    private void StartRecoilCompensationInternal()
    {
        _isRecoilCompensationNeeded = true;
        
        if (WeaponType == WeaponType.Ak47)
        {
            var flag = true;
            
            foreach (var vector2 in RecoilCompensationDataBase.Ak47CompensationData)
            {
                if(!_isRecoilCompensationNeeded && IsReady)
                    return;
                
                if (flag)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(RecoilCompensationDataBase.Ak47Sleep));
                    flag = false;
                }

                // Создание и запуск таймера
                // var stopwatch = Stopwatch.StartNew();
                VirtualInput.MouseMove(vector2);
                // Остановка таймера
                // stopwatch.Stop();

                // Console.WriteLine(stopwatch.Elapsed.Nanoseconds);
                
                Thread.Sleep(TimeSpan.FromSeconds(RecoilCompensationDataBase.Ak47Sleep));
            }
            return;
        }
    }
    
    public void StopRecoilCompensation()
    {
        _isRecoilCompensationNeeded = false;
    }
    
    public void AbortPrepare()
    {
        IsReady = false;
        _abortingPrepare = true;
        Timers.StopWeaponTimer();
        AbortReload();
    }

    public void AbortReload()
    {
        _abortingReload = true;
    }
    
    public void Prepare()
    {
        new Thread(PrepareInternal).Start();
        Timers.StartWeaponTimer(this.PreparationTime);
    }

    private void PrepareInternal()
    {
        for (var i = 0; i < 15; i ++)
        {
            if (_abortingPrepare)
            {
                _abortingPrepare = false;
                return;
            }

            Thread.Sleep(TimeSpan.FromSeconds((float)(PreparationTime / 15)));
        }
        
        IsReady = true;
        WeaponReady?.Invoke();
        
        if (Config.DebugMode)
            Console.WriteLine($"Weapon {WeaponType} is ready");
    }

    private bool _abortingReload;
    
    public void Reload()
    {
        AbortPrepare();
        new Thread(ReloadInternal).Start();
        Timers.StartWeaponTimer(this.ReloadTime);
    }

    private void ReloadInternal()
    {
        for (var i = 0; i < 25; i ++)
        {
            if (_abortingReload)
            {
                _abortingReload = false;
                return;
            }
            
            Thread.Sleep(TimeSpan.FromSeconds((float)ReloadTime / 25));
        }


        var diff = MaxAmmo - CurrentAmmo;
        
        if (diff > Magazine)
            diff = Magazine - diff;
        
        if (diff < 0)
            diff = 0;
        
        Magazine -= diff;
        CurrentAmmo = MaxAmmo;
        
        WindowController.QueueRedraw();
        Prepare();
    }
    
    // Called on new round start
    public void Restart()
    {
        CurrentAmmo = MaxAmmo;
        Magazine = MaxMagazine;
    }
    
}
