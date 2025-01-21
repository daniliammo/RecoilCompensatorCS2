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
    public float ReloadTime;
    public float FireRate;
    
    public delegate void OnReady();
    public event OnReady WeaponReady;
    
    public bool IsReady;
    public bool IsSilent = false;
    
    
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
                ReloadTime = 2.5f;
                FireRate = 0.01f;
                CurrentAmmo = MaxAmmo;
                break;
            case WeaponType.M4A4:
                MaxAmmo = 30;
                MaxMagazine = 90;
                Magazine = MaxMagazine;
                PreparationTime = 0.5m;
                ReloadTime = 3.1f;
                FireRate = 0.0111f;
                CurrentAmmo = MaxAmmo;
                break;
            case WeaponType.M4A1S:
                MaxAmmo = 20;
                MaxMagazine = 80;
                Magazine = MaxMagazine;
                IsSilent = true;
                PreparationTime = 0.5m;
                ReloadTime = 3.1f;
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
                ReloadTime = 3.3f;
                FireRate = 0.0111f;
            
                CurrentAmmo = MaxAmmo;
                break;
            case WeaponType.Usp:
                break;
            case WeaponType.FiveSeven:
                break;
            case WeaponType.Mp9:
                MaxAmmo = 30;
                MaxMagazine = 120;
                Magazine = MaxMagazine;
                PreparationTime = 0.5m;
                ReloadTime = 2.1f;
                FireRate = 0.014283333f;
                CurrentAmmo = MaxAmmo;
                break;
            case WeaponType.Mp7:
                MaxAmmo = 30;
                MaxMagazine = 120;
                Magazine = MaxMagazine;
                PreparationTime = 0.5m;
                ReloadTime = 3.1f;
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
                PreparationTime = 0;
                ReloadTime = 0;
                FireRate = 0;
                CurrentAmmo = MaxAmmo;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        Prepare();
    }

    private bool _isRecoilCompensationNeeded;

    public void StartRecoilCompensation()
    {
        new Thread(StartRecoilCompensationInternal).Start();
    }

    private void StartRecoilCompensationInternal()
    {
        _isRecoilCompensationNeeded = true;
        
        if (WeaponType == WeaponType.Ak47)
        {
            foreach (var vector2 in RecoilCompensationDataBase.Ak47CompensationData)
            {
                if(!_isRecoilCompensationNeeded && IsReady)
                    return;
                
                Thread.Sleep((int)(RecoilCompensationDataBase.Ak47Sleep * 1000));
                VirtualInput.MouseMove(vector2);
            }
            return;
        }
    }
    
    public void StopRecoilCompensation()
    {
        _isRecoilCompensationNeeded = false;
    }
    
    private bool _abortingPrepare;
    
    public void AbortPrepare()
    {
        IsReady = false;
        _abortingPrepare = true;
    }

    public void AbortReload()
    {
        _abortingReload = true;
    }
    
    public void Prepare()
    {
        new Thread(PrepareInternal).Start();
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
        Console.WriteLine($"Weapon {WeaponType} is ready");
    }

    private bool _abortingReload;
    
    public void Reload()
    {
        new Thread(ReloadInternal).Start();
        
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

            Thread.Sleep(TimeSpan.FromSeconds(ReloadTime / 25));
        }


        var diff = MaxAmmo - CurrentAmmo;
        
        if (diff > Magazine)
            diff = Magazine - diff;
        
        if (diff < 0)
            diff = 0;
        
        Magazine -= diff;
        CurrentAmmo = MaxAmmo;
    }
    
    // Called on new round start
    public void Restart()
    {
        CurrentAmmo = MaxAmmo;
        Magazine = MaxMagazine;
    }
    
}
