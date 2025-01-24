using System.Globalization;
using Cairo;
using Gtk;
using Window = Gtk.Window;
using Application = Gtk.Application;
using Color = Cairo.Color;
using Path = System.IO.Path;

namespace RecoilCompensator;


public static class WindowController
{
    
    private static Context _context;
    private static Widget _widget;
    private static Window _window;
    
    
    private static void Main()
    {
        new Thread(RecoilCompensation.Start).Start();
        
        new Thread(StartApplication).Start();
    }

    private static void StartApplication()
    {
        Application.Init();
        
        // Создаем окно с заголовком
        _window = new Window("Recoil Compensator");
        
        _window.DeleteEvent += delegate
        {
            Application.Quit();
            RecoilCompensation.Stop();
        };
        
        // Настройка параметров окна
        _window.AppPaintable = true;
        
        _window.Drawn += ExposeDraw;
        
        _window.ButtonPressEvent += Clicked;

        // Добавление KeyPressEvent
        _window.KeyPressEvent += KeyPressed;
        
        _window.ShowAll();
        
        Application.Run();
    }
    
    public static void Stop()
    {
        _window.Close(); // This invokes _window.DeleteEvent
    }
    
    public static void QueueRedraw()
    {
        _window?.QueueDraw();
    }

    private static void ExposeDraw(object sender, DrawnArgs args)
    {
        _widget ??= sender as Widget;
        
        _context = Gdk.CairoHelper.Create(_widget.Window);
        
        // Установим прозрачный фон
        _context.SetSourceColor(new Color(0, 0, 0, 0));
        _context.Paint();
        
        DrawAllObjects();
        
        // Завершить рисование
        _context.Dispose();
    }

    private static void DrawAllObjects()
    {
        if (Config.DrawCrosshair)
            DrawCrosshair();
        
        DrawScreenBounds();
        DrawCurrentSlot();
        DrawIsPaused();
        DrawSmokeTimer();
        DrawCurrentWeaponTimer();
        DrawCurrentWeaponAmmo();
    }

    private static void DrawCurrentWeaponAmmo()
    {
        DrawText($"{RecoilCompensation.CurrentWeapon.CurrentAmmo}/{RecoilCompensation.CurrentWeapon.Magazine}", new Vector2Int(100, 100), new Color(255, 255, 255));
    }
    
    private static void DrawSmokeTimer()
    {
        var color = new Color(0, 255, 0);
        
        if (Timers.SmokeTimerTime < 9)
            color = new Color(255, 255, 0);

        if (Timers.SmokeTimerTime < 5)
            color = new Color(255, 0, 0);
        
        DrawText(Timers.SmokeTimerTime.ToString(CultureInfo.CurrentCulture), new Vector2Int(752, 400), color);
    }
    
    private static void DrawIsPaused()
    {
        var color = new Color(0, 255, 0);

        var text = "Play";
        
        if (RecoilCompensation.IsPaused)
        {
            color = new Color(255, 0, 0);
            text = "Pause";
        }

        DrawText(text, new Vector2Int(500, 259), color);
    }

    private static void DrawCurrentWeaponTimer()
    {
        var text = Timers.WeaponTimerTime.ToString(CultureInfo.CurrentCulture);
        
        var color = new Color(255, 0, 0);
        
        if (Timers.WeaponTimerTime == 0)
        {
            text = "ГОТОВ";
            color = new Color(0, 255, 0);
        }

        DrawText(text, new Vector2Int(150, 100), color);
    }
    
    private static void DrawCurrentSlot()
    {
        // +1 потому что счет начинается с нуля. Что бы пользователь не запутался в слотах.
        var text = $"{RecoilCompensation.CurrentSlot + 1} ({RecoilCompensation.CurrentWeapon.WeaponType})";
        var color = new Color(0, 255, 0);
        
        if (RecoilCompensation.IsRealSlotUnknown)
        {
            color = new Color(255, 0, 0);
            text = "?";
        }

        DrawText(text, new Vector2Int(250, 250), color);
    }
    
    private static void DrawCrosshair()
    {
        _context.SetSourceColor(Config.CrosshairColor);

        const int w = 10;
        const int h = 10;
        
        var crossHairPosition = new Vector2Int(_widget.Window.Width - w / 2, _widget.Window.Height - h / 2) / 2f;
        crossHairPosition += Config.CenterOffset;
        
        DrawBox(new Rectangle(crossHairPosition.X, crossHairPosition.Y, w, h), Config.CrosshairColor);
    }
    
    private static void DrawScreenBounds()
    {
        // Цвет круга (зеленый)
        _context.SetSourceColor(new Color(0, 1, 0));
        
        DrawCircle(new Vector2Int(0, 0), 15);
        DrawCircle(new Vector2Int(_widget.Window.Width, 0), 15);
        DrawCircle(new Vector2Int(_widget.Window.Width, _widget.Window.Height), 15);
        DrawCircle(new Vector2Int(0, _widget.Window.Height), 15);
    }

    private static void DrawCircle(Vector2Int position, double radius)
    {
        _context.NewPath();
        _context.Arc(position.X, position.Y, radius, 0, 2 * Math.PI);
        _context.FillPreserve();
    }
    
    private static void DrawText(string text, Vector2Int position, Color color)
    {
        _context.NewPath();

        _context.SetFontSize(Config.FontSize); 

        // Установить черный цвет для обводки
        _context.SetSourceColor(Config.FontOutlineColor);
    
        // Получить ширину и высоту текста
        var textExtents = _context.TextExtents(text);
        
        // Нарисовать текст в черном цвете с небольшими смещениями
        for (var x = -1; x <= 1; x++)
        {
            for (var y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue; // Не рисуем в центре
                _context.MoveTo(position.X + x, position.Y + y);
                _context.ShowText(text);
            }
        }
    
        // Теперь нарисуем текст нужного цвета поверх
        _context.SetSourceColor(color);
        // Вертикально центрируем текст
        _context.MoveTo(position.X + textExtents.Height / Config.FontOutlineSize, position.Y + textExtents.Height / Config.FontOutlineSize);
        _context.ShowText(text); // Отрисовать текст
    }

    
    private static void DrawBox(Rectangle bounds, Color color)
    {
        _context.NewPath();
        _context.SetSourceColor(color);
        _context.Rectangle(bounds); // Рисуем квадрат
        _context.Stroke(); // Обводим квадрат
    }
    
    private static void Clicked(object sender, ButtonPressEventArgs args)
    {
        var window = sender as Window;
        window.Decorated = !window.Decorated; // Переключение оконных рамок
    }

    private static void KeyPressed(object sender, KeyPressEventArgs args)
    {
        // Проверяем, была ли нажата клавиша F3
        if (args.Event.Key == Gdk.Key.F3)
        {
            var window = sender as Window;
            window.Iconify(); // Свести окно
            args.RetVal = true; // Сообщаем, что событие обработано
        }
    }
    
}
