# Presentation

import ctypes
import time
from ctypes import wintypes

# Konstanten für den Sound
SND_DEVICE_DEFAULT = 0x00
SND_FILENAME = 0x20000

# Konstanten für Maus-Events
MOUSEEVENTF_MOVE = 0x0001
MOUSEEVENTF_ABSOLUTE = 0x8000
MOUSEEVENTF_LEFTDOWN = 0x0002
MOUSEEVENTF_LEFTUP = 0x0004

# Konstanten für Tastaturevents
KEYEVENTF_KEYUP = 0x0002

# Tastencodes (hier nur einige Beispiele, weitere können hinzugefügt werden)
VK_BACK = 0x08       # Backspace-Taste
VK_TAB = 0x09        # Tabulator-Taste
VK_RETURN = 0x0D     # Enter-Taste
VK_SHIFT = 0x10      # Umschalttaste
VK_CONTROL = 0x11    # Steuerungstaste (Strg)
VK_MENU = 0x12       # Alt-Taste
VK_PAUSE = 0x13      # Pause-Taste
VK_CAPITAL = 0x14    # Feststelltaste (Caps Lock)
VK_ESCAPE = 0x1B      # Escape-Taste
VK_SPACE = 0x20       # Leertaste
VK_OEM_2 = 0xBF     # VK_OEM_2: Forward Slash (/)
VK_OEM_MINUS = 0xBD  # VK_OEM_MINUS: Underscore (_)
VK_Exclamation_Mark = 0x31          # VK_Exclamation_Mark: Exclamation Mark (!)

# Buchstaben
VK_A = 0x41
VK_B = 0x42
VK_C = 0x43
VK_D = 0x44
VK_E = 0x45
VK_F = 0x46
VK_G = 0x47
VK_H = 0x48
VK_I = 0x49
VK_J = 0x4A
VK_K = 0x4B
VK_L = 0x4C
VK_M = 0x4D
VK_N = 0x4E
VK_O = 0x4F
VK_P = 0x50
VK_Q = 0x51
VK_R = 0x52
VK_S = 0x53
VK_T = 0x54
VK_U = 0x55
VK_V = 0x56
VK_W = 0x57
VK_X = 0x58
VK_Y = 0x59
VK_Z = 0x5A

# Zahlen
VK_0 = 0x30
VK_1 = 0x31
VK_2 = 0x32
VK_3 = 0x33
VK_4 = 0x34
VK_5 = 0x35
VK_6 = 0x36
VK_7 = 0x37
VK_8 = 0x38
VK_9 = 0x39

# Numpad
VK_NUMPAD0 = 0x60
VK_NUMPAD1 = 0x61
VK_NUMPAD2 = 0x62
VK_NUMPAD3 = 0x63
VK_NUMPAD4 = 0x64
VK_NUMPAD5 = 0x65
VK_NUMPAD6 = 0x66
VK_NUMPAD7 = 0x67
VK_NUMPAD8 = 0x68
VK_NUMPAD9 = 0x69
VK_MULTIPLY = 0x6A
VK_ADD = 0x6B
VK_SEPARATOR = 0x6C
VK_SUBTRACT = 0x6D
VK_DECIMAL = 0x6E
VK_DIVIDE = 0x6F

# Funktionstasten
VK_F1 = 0x70
VK_F2 = 0x71
VK_F3 = 0x72
VK_F4 = 0x73
VK_F5 = 0x74
VK_F6 = 0x75
VK_F7 = 0x76
VK_F8 = 0x77
VK_F9 = 0x78
VK_F10 = 0x79
VK_F11 = 0x7A
VK_F12 = 0x7B

# Pfeiltasten
VK_LEFT = 0x25
VK_UP = 0x26
VK_RIGHT = 0x27
VK_DOWN = 0x28

# Sonstige
VK_OEM_COMMA = 0xBC
VK_OEM_PERIOD = 0xBE

# Definition der notwendigen Windows-APIs und Strukturen
user32 = ctypes.WinDLL('user32', use_last_error=True)
kernel32 = ctypes.windll.kernel32
EnumWindows = user32.EnumWindows
GetWindowText = user32.GetWindowTextW
GetClassName = user32.GetClassNameW
GetWindowRect = user32.GetWindowRect
# Konstante für das Minimieren eines Fensters
SW_MINIMIZE = 6

# Vordefinierte Konstanten für die Verwendung in der Funktion Unmute
HWND_BROADCAST = 0xFFFF
WM_APPCOMMAND = 0x0319
APPCOMMAND_VOLUME_UP = 0xE017

# Konstanten für Fensterzustände
SW_RESTORE = 9

# Struktur für RECT
class RECT(ctypes.Structure):
    _fields_ = [
        ('left', wintypes.LONG),
        ('top', wintypes.LONG),
        ('right', wintypes.LONG),
        ('bottom', wintypes.LONG)
    ]

# Struktur für WAVEOUTCAPS
class WAVEOUTCAPS(ctypes.Structure):
    _fields_ = [
        ("wMid", ctypes.c_uint16),
        ("wPid", ctypes.c_uint16),
        ("vDriverVersion", ctypes.c_uint32),
        ("szPname", ctypes.c_char * 32),
        ("dwFormats", ctypes.c_uint32),
        ("wChannels", ctypes.c_uint16),
        ("wReserved1", ctypes.c_uint16),
        ("dwSupport", ctypes.c_uint32)
    ]

MAX_TITLE_LENGTH = 255

def find_window_by_title(title):
    found_window = None
    
    def callback(hwnd, _):
        nonlocal found_window
        window_title = ctypes.create_unicode_buffer(MAX_TITLE_LENGTH + 1)
        GetWindowText(hwnd, window_title, MAX_TITLE_LENGTH + 1)
        
        if window_title.value.startswith(title):
            found_window = hwnd
            return False  # Stoppt das Durchlaufen, sobald das Fenster gefunden wurde
        return True
    
    EnumWindows(callback, None)
    return found_window

def get_window_rect(hwnd):
    rect = ctypes.wintypes.RECT()
    GetWindowRect(hwnd, ctypes.byref(rect))
    return rect

def perform_mouse_action_in_window_neu(window_title, action):
    hwnd = find_window_by_title(window_title)
    if hwnd:
        print(f"Found window with title: {window_title}")
        rect = get_window_rect(hwnd)
        x = rect.left + action['x']
        y = rect.top + action['y']
        print(f"Moving mouse to: {x}, {y} in window with title '{window_title}'")
        # Hier würde die Mausaktion stattfinden, z.B. move_mouse(x, y)
        if action.get('click', False):
            print("Performing click action")
            # Hier würde die Mausklick-Aktion stattfinden, z.B. click_mouse()
        if 'pause' in action:
            print(f"Pausing for {action['pause']} seconds")
            # Hier würde die Pause stattfinden, z.B. time.sleep(action['pause'])
    else:
        print(f"Window with title '{window_title}' not found.")

# Funktionen zur Steuerung der Maus
def move_mouse(x, y):
    ctypes.windll.user32.SetCursorPos(x, y)

def click_mouse():
    ctypes.windll.user32.mouse_event(2, 0, 0, 0, 0)  # MOUSEEVENTF_LEFTDOWN
    ctypes.windll.user32.mouse_event(4, 0, 0, 0, 0)  # MOUSEEVENTF_LEFTUP

# Funktionen zur Steuerung der Tastatur
def press_key(key_code):
    user32.keybd_event(key_code, 0, 0, 0)
    user32.keybd_event(key_code, 0, 2, 0)  # Key up event
    precise_sleep(0.1)

def get_precise_time():
    freq = wintypes.LARGE_INTEGER()
    counter = wintypes.LARGE_INTEGER()
    kernel32.QueryPerformanceFrequency(ctypes.byref(freq))
    kernel32.QueryPerformanceCounter(ctypes.byref(counter))
    return counter.value / freq.value
        
def precise_sleep(duration):
    start_time = time.perf_counter()
    while (time.perf_counter() - start_time) < duration:
        pass

def minimize_window(window_title, index=None):
    windows = get_window_list(window_title)
    
    if index is not None:
        if index < 0 or index >= len(windows):
            print(f"Index {index} is out of range for the windows list.")
            return
        hwnd, _ = windows[index]
    else:
        hwnd = user32.FindWindowW(None, window_title)
    
    if hwnd:
        print(f"Minimizing window with hwnd: {hwnd}")
        user32.ShowWindow(hwnd, SW_MINIMIZE)
    else:
        print(f"Window '{window_title}' not found.")

def minimize_window_by_title_and_index(window_title, index=None):
    windows = get_window_list(window_title)
    
    if index is not None:
        if index < 0 or index >= len(windows):
            print(f"Index {index} is out of range for the windows list.")
            return
        hwnd, _ = windows[index]
    else:
        hwnd = user32.FindWindowW(None, window_title)
    
    if hwnd:
        minimize_window(hwnd)
        print(f"Minimized window '{window_title}' with hwnd: {hwnd}")
    else:
        print(f"Window '{window_title}' not found.")

def enum_windows_by_creation_time(window_title):
    windows = []
    
    def enum_windows_proc(hwnd, _):
        length = user32.GetWindowTextLengthW(hwnd)
        buff = ctypes.create_unicode_buffer(length + 1)
        user32.GetWindowTextW(hwnd, buff, length + 1)
        
        if window_title in buff.value:
            creation_time = get_precise_time()
            windows.append((hwnd, creation_time))
            print(f"Found window: {buff.value} (HWND: {hwnd}) at time {creation_time}")
        return True
    
    enum_windows_proc_func = ctypes.WINFUNCTYPE(wintypes.BOOL, wintypes.HWND, wintypes.LPARAM)(enum_windows_proc)
    user32.EnumWindows(enum_windows_proc_func, 0)
    
    windows.sort(key=lambda x: x[1])  # Sortieren nach Erstellungszeit
    return windows

def set_mouse_position_in_window(window_title, relative_x, relative_y, precise_creation_time=None, click=False):
    def enum_windows_proc(hwnd, _):
        length = user32.GetWindowTextLengthW(hwnd)
        buff = ctypes.create_unicode_buffer(length + 1)
        user32.GetWindowTextW(hwnd, buff, length + 1)
        
        if window_title in buff.value:
            rect = RECT()
            user32.GetWindowRect(hwnd, ctypes.byref(rect))
            creation_time = get_precise_time()
            
            if precise_creation_time and abs(creation_time - precise_creation_time) > 0.1:
                return True
            
            x = rect.left + relative_x
            y = rect.top + relative_y
            user32.SetCursorPos(x, y)
            
            if click:
                user32.mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
                user32.mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
            
            return False
        return True

    enum_windows_proc_func = ctypes.WINFUNCTYPE(wintypes.BOOL, wintypes.HWND, wintypes.LPARAM)(enum_windows_proc)
    user32.EnumWindows(enum_windows_proc_func, 0)

def restore_window(hwnd):
    user32.ShowWindow(hwnd, SW_RESTORE)

def restore_window_by_title_and_index(window_title, index=None):
    windows = enum_windows_by_creation_time(window_title)
    print(f"Windows found for '{window_title}': {windows}")
    
    if index is not None:  # Check if index is not None before comparison
        if index < len(windows):
            hwnd, _ = windows[index]
            print(f"Restoring window: {hwnd}")
            restore_window(hwnd)
        else:
            print(f"No window found at index {index} with title {window_title}")
    else:
        print("Index is None. Cannot compare.")

def get_window_list(window_title, class_name=None):
    windows = []

    def enum_windows_proc(hwnd, _):
        length = user32.GetWindowTextLengthW(hwnd)
        buff = ctypes.create_unicode_buffer(length + 1)
        user32.GetWindowTextW(hwnd, buff, length + 1)

        if class_name:
            class_name_buffer = ctypes.create_unicode_buffer(256)
            user32.GetClassNameW(hwnd, class_name_buffer, 256)
            current_class_name = class_name_buffer.value

            if window_title in buff.value and current_class_name == class_name:
                rect = RECT()
                if user32.GetWindowRect(hwnd, ctypes.byref(rect)):
                    windows.append((hwnd, rect))
        else:
            if window_title in buff.value:
                rect = RECT()
                if user32.GetWindowRect(hwnd, ctypes.byref(rect)):
                    windows.append((hwnd, rect))
        
        return True

    enum_windows_proc_func = ctypes.WINFUNCTYPE(wintypes.BOOL, wintypes.HWND, wintypes.LPARAM)(enum_windows_proc)
    user32.EnumWindows(enum_windows_proc_func, 0)
    return windows

def perform_mouse_action_in_window_mit_index(window_title, index, action):
    windows = get_window_list(window_title)
    if index < 0 or index >= len(windows):
        print(f"Index {index} is out of range for the windows list.")
        return

    hwnd, rect = windows[index]  # Unpack tuple
    x = rect.left + action['x']
    y = rect.top + action['y']
    
    print(f"Moving mouse to: {x}, {y} in window with title '{window_title}' at index {index}")
    move_mouse(x, y)
    if action.get('click', False):
        click_mouse()
    if 'pause' in action:
        precise_sleep(action['pause'])

# Kernstück des ganzen
def execute_actions(actions):
    for action in actions:
        if action['type'] == 'mouse':
            move_mouse(action['x'], action['y'])
            if action.get('click', False):
                click_mouse()
        elif action['type'] == 'key':
            press_key(action['key'])
        elif action['type'] == 'function':
            if 'parameters' in action:
                action['function'](*action['parameters'])
            else:
                action['function']()
        
        if 'pause' in action:
            precise_sleep(action['pause'])

def execute_actions_with_loop(actions, num_repeats):
    for _ in range(num_repeats):
        execute_actions(actions)

# Meine Konstanten
# Script Console sind besonders anfällig für Namensänderungen
# In Snake / Tetris auf die FilePath variablen schauen! Sonst lädt Highscore nicht
# Chat Server und Client auf "Schnittstellen Ordner" prüfen!

# AUFBAU von hinten nach vorne
# Danke
# Textadventure
# Strukturierter Code
# Tetris
# Snake Console
# Snake
# Clients und Server Minimiert
# Alexander Fleig
# Das Script hier

window_title_af = "Alexander Fleig"
window_title_danke = "Danke"
window_title_sc = "SC"
window_title_script = "Presentation_2_0.py - 2024_HTML_AF_portfolio_test - Visual Studio Code [Administrator]"
window_title_server = "My chat Server"
window_title_client = "My chat client"
window_title_console_snake = "C:\\Users\\FP2402389\\Documents\\A\\Präsentation A Fleig\\Snake\\bin\\Debug\\net8.0-windows\\Snake.exe"
window_title_snake = "Snake" # wird nicht richtig erkannt
window_title_tetris = "Tetris"
window_title_ta = "Textadventure - Microsoft Visual Studio (Administrator)"

# Eröffnung der Presentation: Zeit einplanen
actions_set_1 = [
    {"type": "function", "function": minimize_window, "parameters": [window_title_script], "pause": 20},
    {"type": "function", "function": minimize_window, "parameters": [window_title_af], "pause": 1}
]
# Chat Programm zeigen

actions_set_2 = [
    {"type": "function", "function": restore_window_by_title_and_index, "parameters": [window_title_client, 0], "pause": 0.2},
    {"type": "function", "function": restore_window_by_title_and_index, "parameters": [window_title_client, 1], "pause": 0.2},
    {"type": "function", "function": restore_window_by_title_and_index, "parameters": [window_title_client, 2], "pause": 0.2}
]
# Chat Programm ausführen
actions_set_3 = [
    {"type": "function", "function": perform_mouse_action_in_window_mit_index, "parameters": [window_title_client, 0, {"type": "mouse", "x": 500, "y": 180, "click": True, "pause": 0.5}], "pause": 1},
    {"type": "function", "function": perform_mouse_action_in_window_mit_index, "parameters": [window_title_client, 1, {"type": "mouse", "x": 500, "y": 180, "click": True, "pause": 0.5}], "pause": 1},
    {"type": "function", "function": perform_mouse_action_in_window_mit_index, "parameters": [window_title_client, 2, {"type": "mouse", "x": 500, "y": 180, "click": True, "pause": 0.5}], "pause": 1},
    {"type": "function", "function": perform_mouse_action_in_window_mit_index, "parameters": [window_title_client, 2, {"type": "mouse", "x": 200, "y": 360, "click": True, "pause": 0.5}], "pause": 1},
    {'type': 'key', 'key': VK_H, 'pause': 0.1},
    {'type': 'key', 'key': VK_A, 'pause': 0.1},
    {'type': 'key', 'key': VK_L, 'pause': 0.1},
    {'type': 'key', 'key': VK_L, 'pause': 0.1},
    {'type': 'key', 'key': VK_O, 'pause': 0.1},
    
]
# Chat Programm Loop ausführen
actions_set_4 = [
    {'type': 'key', 'key': VK_RETURN, 'pause': 0.5}
]
# Chat Programm ausführen und zu Snake wechseln
actions_set_5 = [
    {"type": "function", "function": minimize_window, "parameters": [window_title_server], "pause": 0.2},
    {"type": "function", "function": minimize_window, "parameters": [window_title_client, 2], "pause": 0.2},
    {"type": "function", "function": minimize_window, "parameters": [window_title_client, 1], "pause": 0.2},
    {"type": "function", "function": minimize_window, "parameters": [window_title_client, 0], "pause": 0.2},
    {"type": "function", "function": set_mouse_position_in_window, "parameters": [window_title_snake, 150, 50, None, True], "pause": 1},
]
# Snake ausführen
actions_set_6 = [
    {"type": "key", "key": VK_RIGHT, "pause": 0.1},
    {"type": "key", "key": VK_RIGHT, "pause": 0.1},
    {"type": "key", "key": VK_RIGHT, "pause": 0.1},
    {"type": "key", "key": VK_RIGHT, "pause": 0.1},
    {"type": "key", "key": VK_RIGHT, "pause": 0.1},
    {"type": "key", "key": VK_RIGHT, "pause": 0.1},
    {"type": "key", "key": VK_RIGHT, "pause": 0.1},
    {"type": "key", "key": VK_RIGHT, "pause": 0.1},
    {"type": "key", "key": VK_RIGHT, "pause": 0.1},
    {"type": "key", "key": VK_RIGHT, "pause": 0.1},
    
    {"type": "key", "key": VK_DOWN, "pause": 0.1},
    {"type": "key", "key": VK_DOWN, "pause": 0.1},
    {"type": "key", "key": VK_DOWN, "pause": 0.1},
    {"type": "key", "key": VK_DOWN, "pause": 0.1},
    
    {"type": "key", "key": VK_LEFT, "pause": 0.1},
    {"type": "key", "key": VK_LEFT, "pause": 0.1},
    {"type": "key", "key": VK_LEFT, "pause": 0.1},
    
    {"type": "key", "key": VK_DOWN, "pause": 0.1},
    {"type": "key", "key": VK_DOWN, "pause": 0.1},
    {"type": "key", "key": VK_DOWN, "pause": 0.1},
    
    {"type": "key", "key": VK_LEFT, "pause": 0.1},
    {"type": "key", "key": VK_LEFT, "pause": 0.1},
    
    {"type": "key", "key": VK_UP, "pause": 0.1},
    {"type": "key", "key": VK_UP, "pause": 0.1},
    {"type": "key", "key": VK_UP, "pause": 0.1},
    
    {"type": "key", "key": VK_LEFT, "pause": 0.1},
    {"type": "key", "key": VK_LEFT, "pause": 0.1},
    
    {"type": "key", "key": VK_UP, "pause": 0.1},
    {"type": "key", "key": VK_UP, "pause": 0.1},
    {"type": "key", "key": VK_UP, "pause": 0.1},
    
    {"type": "key", "key": VK_LEFT, "pause": 0.1},
    {"type": "key", "key": VK_LEFT, "pause": 0.1},
    {"type": "key", "key": VK_LEFT, "pause": 0.1},
    
    {"type": "key", "key": VK_UP, "pause": 0.5}
]
# Wechsel auf Tetris
actions_set_7 = [
    {"type": "function", "function": minimize_window, "parameters": [window_title_console_snake], "pause": 0.5},
    {"type": "function", "function": minimize_window, "parameters": [window_title_snake], "pause": 0.5},
    {"type": "function", "function": set_mouse_position_in_window, "parameters": [window_title_tetris, 650, 450, None, True], "pause": 0.5},
]
# Tetris ausführen
actions_set_8 = [
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_NUMPAD0, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_RIGHT, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_NUMPAD0, "pause": 0.2},
    {"type": "key", "key": VK_NUMPAD0, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_LEFT, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_NUMPAD1, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_NUMPAD0, "pause": 0.2},
    {"type": "key", "key": VK_LEFT, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_RIGHT, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_NUMPAD1, "pause": 0.2},
    {"type": "key", "key": VK_NUMPAD0, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_UP, "pause": 0.2},
    {"type": "key", "key": VK_NUMPAD0, "pause": 0.2},
    {"type": "key", "key": VK_RIGHT, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_DOWN, "pause": 0.2},
    {"type": "key", "key": VK_NUMPAD0, "pause": 0.2},
]
# Tetris minimieren
actions_set_9 = [
    {"type": "function", "function": minimize_window, "parameters": [window_title_tetris], "pause": 5},
]
# TA demonstrieren
actions_set_10 =[
    {"type": "function", "function": minimize_window, "parameters": [window_title_sc], "pause": 2},
    {"type": "function", "function": set_mouse_position_in_window, "parameters": [window_title_ta, 500, 50, None, False], "pause": 3},
    {"type": "function", "function": minimize_window, "parameters": [window_title_ta], "pause": 2},
]

def main():  
    global actions_set_1
    global actions_set_2
    global actions_set_3
    global actions_set_4
    global actions_set_5
    global actions_set_6
    global actions_set_7
    global actions_set_8
    global actions_set_9
    global actions_set_10
    
    # precise_sleep(2)
    
    execute_actions_with_loop(actions_set_1, 1)
    execute_actions_with_loop(actions_set_2, 1)
    execute_actions_with_loop(actions_set_3, 1)
    execute_actions_with_loop(actions_set_4, 5) # Loop
    execute_actions_with_loop(actions_set_5, 1)
    execute_actions_with_loop(actions_set_6, 2) # Snake spielen
    execute_actions_with_loop(actions_set_7, 1)
    execute_actions_with_loop(actions_set_8, 1) # Tetris spielen
    execute_actions_with_loop(actions_set_9, 1)
    execute_actions_with_loop(actions_set_10, 1)


if __name__ == "__main__":
    main()