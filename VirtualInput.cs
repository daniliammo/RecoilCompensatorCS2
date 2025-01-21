using System.Diagnostics;

namespace RecoilCompensator;


public static class VirtualInput
{
    
    private static Process _daemonProcess;

    private static XdgSessionType _sessionType = Config.SessionType;


    public static void Start()
    {
        if (_sessionType == XdgSessionType.Wayland)
        {
            var startYdoToolDaemonThread = new Thread(StartYdoToolDaemon);
            startYdoToolDaemonThread.Name = "(VirtualInput): StartYdoToolDaemonThread";
            startYdoToolDaemonThread.Start();
        }
    }

    public static void PressKey(int key)
    {
        if (_sessionType == XdgSessionType.Wayland)
            Process.Start("/bin/ydotool", $"key {key}:1 {key}:0");
    }

    public static void PressKeyWithDelay(int key, int delayMilliseconds)
    {
        if (_sessionType == XdgSessionType.Wayland)
            Process.Start("/bin/ydotool", $"key {key}:1 -d {delayMilliseconds} {key}:0");
    }
    
    public static void MouseMove(Vector2 position)
    {
        if (_sessionType == XdgSessionType.Wayland)
            Process.Start("/bin/ydotool", $"mousemove -x {position.X} -y {position.Y}");
    }
    
    public static void Stop()
    {
        if (_sessionType == XdgSessionType.Wayland)
            StopWayland();
    }

    #region Wayland
    private static void StartYdoToolDaemon()
    {
        _daemonProcess = Process.Start("/bin/ydotoold");
    }

    private static void StopWayland()
    {
        _daemonProcess.Close();
    }
    #endregion
}
