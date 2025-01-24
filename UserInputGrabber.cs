using System.Diagnostics;
using System.Text;
using GLib;
using Process = System.Diagnostics.Process;
using Thread = System.Threading.Thread;

namespace RecoilCompensator;


public static class UserInputGrabber
{
    
    public delegate void OnMouseWheel(bool isUp);
    public static event OnMouseWheel MouseWheel;
    
    public delegate void OnButtonPress(Keys button);
    public static event OnButtonPress ButtonPressed;
    
    public delegate void OnButtonReleased(Keys button);
    public static event OnButtonReleased ButtonReleased;
    
    public delegate void OnKeyPress(Keys button);
    public static event OnKeyPress KeyPressed;
    
    public delegate void OnKeyRelease(Keys button);
    public static event OnKeyRelease KeyReleased;

    private static Process _process;
    

    public static void Start()
    {
        var startInputGrabberProcess = new Thread(StartInputGrabberProcess);
        startInputGrabberProcess.Name = "(User Input Grabber): Start Input Grabber Process Thread";
        startInputGrabberProcess.Start();
    }

    private static void StartInputGrabberProcess()
    {
        // Создаем объект ProcessStartInfo
        var startInfo = new ProcessStartInfo
        {
            WorkingDirectory = "/",
            FileName = "/home/daniliammo/Projects/RustroverProjects/RustNoRecoil/target/release/RustNoRecoil",
            RedirectStandardOutput = true, // Перенаправляем стандартный вывод
            UseShellExecute = false,        // Необходимо для перенаправления вывода
            StandardOutputEncoding = Encoding.UTF8 // Устанавливаем кодировку для вывода 
        };

        _process = Process.Start(startInfo);
        ArgumentNullException.ThrowIfNull(_process);
        
        // Читаем вывод асинхронно
        new Thread(ReadOutput).Start();
    }
    
    private static void ReadOutput()
    {
        using (var reader = new StreamReader(_process.StandardOutput.BaseStream, Encoding.UTF8))
        {
            while (!_process.HasExited && !reader.EndOfStream)
            {
                var line = reader.ReadLine();
                
                if (Config.DebugMode)
                    Console.WriteLine($"RDev: {line}");
                CheckLine(line);
            }
        }
        _process.WaitForExit();
    }

    private static void CheckLine(string line)
    {
        switch (line)
        {
            case null:
                return;
            // Wheel
            case "w1":
                MouseWheel?.Invoke(true);
                return;
            case "w0":
                MouseWheel?.Invoke(false);
                return;
            // Mouse buttons
            case "mr":
                ButtonPressed?.Invoke(Keys.RightMouse);
                return;
            case "ml":
                ButtonPressed?.Invoke(Keys.LeftMouse);
                return;
            // Keys
            case "e":
                KeyPressed?.Invoke(Keys.Escape);
                return;
            // Keys
            case "q":
                KeyPressed?.Invoke(Keys.Q);
                return;
            // Reload
            case "r":
                KeyPressed?.Invoke(Keys.R);
                return;
            // Player Movement
            case "w":
                KeyPressed?.Invoke(Keys.W);
                return;
            case "a":
                KeyPressed?.Invoke(Keys.A);
                return;
            case "s":
                KeyPressed?.Invoke(Keys.S);
                return;
            case "d":
                KeyPressed?.Invoke(Keys.D);
                return;
            case "sp":
                KeyPressed?.Invoke(Keys.Space);
                return;
            // F1, F2, F3 ...
            case "f1":
                KeyPressed?.Invoke(Keys.F1);
                return;
            case "f2":
                KeyPressed?.Invoke(Keys.F2);
                return;
            case "f3":
                KeyPressed?.Invoke(Keys.F3);
                return;
            case "f5":
                KeyPressed?.Invoke(Keys.F5);
                return;
            case "f6":
                KeyPressed?.Invoke(Keys.F6);
                return;
            case "f7":
                KeyPressed?.Invoke(Keys.F7);
                return;
            case "f8":
                KeyPressed?.Invoke(Keys.F8);
                return;
            case "f9":
                KeyPressed?.Invoke(Keys.F9);
                return;
            // Key1, Key2, Key3 ...
            case "1":
                KeyPressed?.Invoke(Keys.Key1);
                return;
            case "2":
                KeyPressed?.Invoke(Keys.Key2);
                return;
            case "3":
                KeyPressed?.Invoke(Keys.Key3);
                return;
            case "4":
                KeyPressed?.Invoke(Keys.Key4);
                return;
            case "5":
                KeyPressed?.Invoke(Keys.Key5);
                return;
            // Other
            case "al":
                KeyPressed?.Invoke(Keys.AltGr);
                return;
            case "at":
                KeyPressed?.Invoke(Keys.Alt);
                return;
            case "lc":
                KeyPressed?.Invoke(Keys.LeftControl);
                return;
            case "rc":
                KeyPressed?.Invoke(Keys.RightControl);
                return;
            // Release
            // Mouse buttons
            case "mrr":
                ButtonReleased?.Invoke(Keys.RightMouse);
                return;
            case "mlr":
                ButtonReleased?.Invoke(Keys.LeftMouse);
                return;
            // Keys
            case "er":
                KeyReleased?.Invoke(Keys.Escape);
                return;
            // Keys
            // Reload
            case "qr":
                KeyReleased?.Invoke(Keys.Q);
                return;
            case "rr":
                KeyReleased?.Invoke(Keys.R);
                return;
            // Player Movement
            case "wr":
                KeyReleased?.Invoke(Keys.W);
                return;
            case "ar":
                KeyReleased?.Invoke(Keys.A);
                return;
            case "sr":
                KeyReleased?.Invoke(Keys.S);
                return;
            case "dr":
                KeyReleased?.Invoke(Keys.D);
                return;
            case "spr":
                KeyReleased?.Invoke(Keys.Space);
                return;
            // F1, F2, F3 ...
            case "f1r":
                KeyReleased?.Invoke(Keys.F1);
                return;
            case "f2r":
                KeyReleased?.Invoke(Keys.F2);
                return;
            case "f3r":
                KeyReleased?.Invoke(Keys.F3);
                return;
            case "f5r":
                KeyReleased?.Invoke(Keys.F5);
                return;
            case "f6r":
                KeyReleased?.Invoke(Keys.F6);
                return;
            case "f7r":
                KeyReleased?.Invoke(Keys.F7);
                return;
            case "f8r":
                KeyReleased?.Invoke(Keys.F8);
                return;
            case "f9r":
                KeyReleased?.Invoke(Keys.F9);
                return;
            // Key1, Key2, Key3 ...
            case "1r":
                KeyReleased?.Invoke(Keys.Key1);
                return;
            case "2r":
                KeyReleased?.Invoke(Keys.Key2);
                return;
            case "3r":
                KeyReleased?.Invoke(Keys.Key3);
                return;
            case "4r":
                KeyReleased?.Invoke(Keys.Key4);
                return;
            case "5r":
                KeyReleased?.Invoke(Keys.Key5);
                return;
            // Other
            case "alr":
                KeyReleased?.Invoke(Keys.AltGr);
                return;
            case "atr":
                KeyReleased?.Invoke(Keys.Alt);
                return;
            case "lcr":
                KeyReleased?.Invoke(Keys.LeftControl);
                return;
            case "rcr":
                KeyReleased?.Invoke(Keys.RightControl);
                return;
            
            default:
                Console.WriteLine($"(UserInputGrabber): received unknown line from RDev: line='{line}'. Stop.");
                WindowController.Stop();
                return;
        }
    }

    public static void Stop()
    {
        _process.Kill();
    }
}
