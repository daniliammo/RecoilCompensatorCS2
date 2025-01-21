﻿using System.Globalization;
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
        RecoilCompensation.Start();
        
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
        
        // window.ScreenChanged += ScreenChanged;
        _window.Drawn += ExposeDraw;
        
        _window.ButtonPressEvent += Clicked;

        // Добавление KeyPressEvent
        _window.KeyPressEvent += KeyPressed;
        
        _window.ShowAll();
        Application.Run();
    }
    
    public static void QueueRedraw()
    {
        _window?.QueueDraw();
    }
    
    private static void ScreenChanged(object o, ScreenChangedArgs args)
    {
        _widget = o as Widget;
        var screen = _widget.Window.Screen;

        // _widget.Visual = screen.SystemVisual;
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
        DrawText(Timers.WeaponTimerTime.ToString(CultureInfo.CurrentCulture), new Vector2Int(150, 100), new Color(255, 0, 255));
    }
    
    private static void DrawCurrentSlot()
    {
        // +1 потому что счет начинается с нуля. Что бы пользователь не запутался в слотах.
        var text = (RecoilCompensation.CurrentSlot + 1).ToString(CultureInfo.CurrentCulture);
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

        var crossHairPosition = new Vector2Int(_widget.Window.Width / 2, _widget.Window.Height / 2);
        crossHairPosition.X += Config.CenterOffset.X;
        crossHairPosition.Y += Config.CenterOffset.Y;
        
        DrawBox(new Rectangle(crossHairPosition.X, crossHairPosition.Y, 10, 10), Config.CrosshairColor);
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
        // Установить шрифт и размер
        _context.SetFontSize(Config.FontSize); // Размер шрифта 
        _context.MoveTo(position.X, position.Y); // Позиция для текста
    
        // Получить ширину текста для центрирования
        var textExtents = _context.TextExtents(text);
        _context.MoveTo(position.X - textExtents.Width + 50, position.Y + textExtents.Height - 50); // Вертикально центрируем текст
        
        _context.SetSourceColor(color);
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
