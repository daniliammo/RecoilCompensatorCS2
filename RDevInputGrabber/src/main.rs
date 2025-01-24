use rdev::{listen, Button, Event, EventType, Key};

fn main() {
    if let Err(error) = listen(callback) {
        eprintln!("RDev Input Grabber Error: {:?}", error);
    }
}

fn callback(event: Event) {
    match event.event_type {
        EventType::Wheel { delta_x: _delta_x, delta_y } => {
            if delta_y == 1 {
                println!("w1");
                return;
            }
            if delta_y == -1 {
                println!("w0");
                return;
            }
        }

        // EventType::MouseMove { x: _x, y: _y } => {
        //     println!("ydotool mousemove -x {_x} -y {_y}")
        // }
        
        EventType::ButtonPress(key) => {
            if key == Button::Right {
                println!("mr");
                return;
            }
            if key == Button::Left {
                println!("ml");
                return;
            }
        }

        EventType::ButtonRelease(key) => {
            if key == Button::Right {
                println!("mrr");
                return;
            }
            if key == Button::Left {
                println!("mlr");
                return;
            }
        }
        
        EventType::KeyPress(key) => {
            if key == Key::Escape {
                println!("e");
                return;
            }

            // Drop C4 Bind
            if key == Key::KeyQ {
                println!("q");
                return;
            }
            
            // Reload
            if key == Key::KeyR {
                println!("r");
                return;
            }
            
            // Player Movement
            if key == Key::KeyW {
                println!("w");
                return;
            }
            if key == Key::KeyA {
                println!("a");
                return;
            }
            if key == Key::KeyS {
                println!("s");
                return;
            }
            if key == Key::KeyD {
                println!("d");
                return;
            }
            if key == Key::Space {
                println!("sp");
                return;
            }
            
            // F1,F2,F3 ...
            if key == Key::F1 {
                println!("f1");
                return;
            }
            if key == Key::F2 {
                println!("f2");
                return;
            }
            if key == Key::F3 {
                println!("f3");
                return;
            }
            if key == Key::F5 {
                println!("f5");
                return;
            }
            if key == Key::F6 {
                println!("f6");
                return;
            }
            if key == Key::F7 {
                println!("f7");
                return;
            }
            if key == Key::F8 {
                println!("f8");
                return;
            }
            if key == Key::F9 {
                println!("f9");
                return;
            }

            // Key1, Key2, Key3 ...
            if key == Key::Num1 {
                println!("1");
                return;
            }
            if key == Key::Num2 {
                println!("2");
                return;
            }
            if key == Key::Num3 {
                println!("3");
                return;
            }
            if key == Key::Num4 {
                println!("4");
                return;
            }
            if key == Key::Num5 {
                println!("5");
                return;
            }

            // Other
            if key == Key::AltGr {
                println!("al");
                return;
            }

            if key == Key::Alt {
                println!("at")
            }
            
            if key == Key::ControlRight {
                println!("rc");
                return;
            }

            if key == Key::ControlLeft {
                println!("lc");
                return;
            }
        }

        EventType::KeyRelease(key) => {
            if key == Key::Escape {
                println!("er");
                return;
            }

            // Drop C4 Bind
            if key == Key::KeyQ {
                println!("qr");
                return;
            }
            
            // Reload
            if key == Key::KeyR {
                println!("rr");
                return;
            }
            
            // Player Movement
            if key == Key::KeyW {
                println!("wr");
                return;
            }
            if key == Key::KeyA {
                println!("ar");
                return;
            }
            if key == Key::KeyS {
                println!("sr");
                return;
            }
            if key == Key::KeyD {
                println!("dr");
                return;
            }
            if key == Key::Space {
                println!("spr");
                return;
            }

            // F1,F2,F3 ...
            if key == Key::F1 {
                println!("f1r");
                return;
            }
            if key == Key::F2 {
                println!("f2r");
                return;
            }
            if key == Key::F3 {
                println!("f3r");
                return;
            }
            if key == Key::F5 {
                println!("f5r");
                return;
            }
            if key == Key::F6 {
                println!("f6r");
                return;
            }
            if key == Key::F7 {
                println!("f7r");
                return;
            }
            if key == Key::F8 {
                println!("f8r");
                return;
            }
            if key == Key::F9 {
                println!("f9r");
                return;
            }

            // Key1, Key2, Key3 ...
            if key == Key::Num1 {
                println!("1r");
                return;
            }
            if key == Key::Num2 {
                println!("2r");
                return;
            }
            if key == Key::Num3 {
                println!("3r");
                return;
            }
            if key == Key::Num4 {
                println!("4r");
                return;
            }
            if key == Key::Num5 {
                println!("5r");
                return;
            }
            
            // Other
            if key == Key::AltGr {
                println!("alr");
                return;
            }
            if key == Key::Alt {
                println!("atr")
            }
            if key == Key::ControlRight {
                println!("rcr");
                return;
            }
            if key == Key::ControlLeft {
                println!("lcr");
                return;
            }
        }
        _ => { return; } // Игнорируем все другие события
    }
}
