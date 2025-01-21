namespace RecoilCompensator;

public static class KeysDataBase
{
    // from /usr/include/linux/input-event-codes.h
    public const int KeyW = 17;
    public const int KeyA = 30;
    public const int KeyS = 31;
    public const int KeyD = 32;
}

public enum Keys
{
    // Mouse
    RightMouse, // mr (in main.rs)
    LeftMouse, // ml (in main.rs)
    
    // Keyboard
    Escape,
    // Numerics
    F1,
    F2,
    F3,
    F5,
    F6,
    F7,
    F8,
    F9,
    Key1,
    Key2,
    Key3,
    Key4,
    Key5,
    // Player Movement
    W,
    A,
    S,
    D,
    Space,
    R,
    // Other
    AltGr,
    RightControl
}
