# Python_Client

import sys
from tkinter import *
from tkinter import messagebox
from Schnittstellen import Ethernet_Module as Eth
import time
import threading
import re


fenster = Tk()

fenster.title("My chat client")
fenster.geometry('600x400')

myPort = None
messages = []

entries_count = 100
current_entry_index = entries_count - 10

max_input_length = 50

makro_1_string = "Makro 1"
makro_2_string = "Makro 2"
makro_3_string = "Makro 3"

password_string = "7792"
connect_adress_string = "localhost"
port_int = 7777
register_string = "Nobody"

#   password_user_string = "8832"
#   password_admin_string = "7792"

unlock_admin_string = "unlock_admin"
request_user_info_string = "request_user_info8"
all_user_info = ""
color_case = 5
counter = 1

def send_message (message):

    global myPort
    global max_input_length
    global display_main

    if len(message) > max_input_length:
        display_main.insert('end',"Nachricht zu lang! Bitte kürzen!")
    else: 
        if(myPort != None and myPort.isConnected()):
            update_display(message)
            myPort.send(message.encode())
        else:
            display_main.insert('end',"\n" "not connected")

def button_action_makro_1 ():

    global makro_1_string
    global color_case
    color_case = 1
    send_message (str(makro_1_string))

def button_action_makro_2 ():

    global makro_2_string
    global color_case
    color_case = 2
    send_message (str(makro_2_string))


def button_action_makro_3 ():

    global makro_3_string
    global color_case
    color_case = 3
    send_message (str(makro_3_string))


def button_action_kick_user_fenster ():

    messagebox.showinfo(message ="Benutze: \n/block [Name]\n um diesen Nutzer zu blocken.", title = "Wie man jemanden blockt")

def button_action_show_user_fenster ():
    global request_user_info_string
    send_message(str(request_user_info_string))
 
def start_show_user_fenster ():
    global all_user_info

    show_user_fenster = Toplevel(fenster)
    show_user_fenster.title("Show all Users")
    show_user_fenster.geometry('250x400')

    display_su = Label(show_user_fenster, text=all_user_info, anchor='w')
    display_su.place(x=0, y=5)
    display_su.config(width=35, height=20)

    def button_action_exit_show_user_fenster ():

        show_user_fenster.destroy()

    exit_show_user_button = Button(show_user_fenster, text="OK", command=button_action_exit_show_user_fenster)
    exit_show_user_button.place(x = 95, y = 350, width=60, height=30)

def button_action_whisper ():

    messagebox.showinfo(message ="Benutze: \n/w [Name]\n um mit diesem zu Flüstern.", title = "Wie man flüstert")

def reader(data):

    update_display(data.decode())

def update_display(message):
    global messages
    global display_main
    global current_entry_index
    global entries_count
    global all_user_info
    global color_case
    global counter

    if message == unlock_admin_string:
        unhide()
        return
    
    if message.startswith("Error:"):
        return
    
    if message.startswith("request_user_info9"):
        all_user_info = message[18:]
        start_show_user_fenster()
        return

    if message.startswith("request_user_info8"):
        return
    if message.startswith("unlock_admin"):
        return

    messages.append(message)
    if len(messages) > entries_count:
        messages.pop(0)

    counter += 1
    tag_name = f"{message}_{counter}"
    color_return = switch_color_case(color_case)
    display_main.insert(END,"\n" f"{message}")
    display_main.tag_add(f"{tag_name}", "end-1l", "end")
    display_main.tag_configure(f"{tag_name}", foreground=f"{color_return}")
    display_main.see(END)
    display_main.after(0)


def add_message(event):
    
    send_message (eingabefeld.get())

def switch_color_case(color_case):
    try:
        return {
            0: "black", # case
            1: "purple", # whisper
            2: "red", 
            3: "green",
            4: "pink",
            5: "blue",
        }[color_case]
    except KeyError:
        return "black"


def quit_fenster ():
    global myPort
    if myPort != None: 
        myPort.close()
        myPort = None
    fenster.quit()

def button_action_cm_fenster ():

    cm_fenster = Toplevel(fenster)
    cm_fenster.title("Change Makro")
    cm_fenster.geometry('400x200')

    def button_action_exit_cm_fenster ():
       
       cm_fenster.destroy()

    def button_action_cm_makro_1 ():

        global makro_1_string
        makro_1_string = cm_eingabefeld.get()

    def button_action_cm_makro_2 ():

        global makro_2_string
        makro_2_string = cm_eingabefeld.get()

    def button_action_cm_makro_3 ():

        global makro_3_string
        makro_3_string = cm_eingabefeld.get()

    cm_exit_button = Button(cm_fenster, text="Fertig", command=button_action_exit_cm_fenster)
    cm_exit_button.place(x = 250, y = 150, width=120, height=30)

    cm_label = Label(cm_fenster, text="Wähle ein Makro dass du ändern möchtst!")
    cm_label.place (x= 50, y = 20, width=300, height=10)

    cm_eingabefeld = Entry(cm_fenster, bd=5, width=50)
    cm_eingabefeld.place(x = 50, y = 100)

    cm_makro_1_change_button = Button(cm_fenster, text="Makro 1", command=button_action_cm_makro_1)
    cm_makro_1_change_button.place(x = 100, y = 50, width=60, height=30)

    cm_makro_2_change_button = Button(cm_fenster, text="Makro 2", command=button_action_cm_makro_2)
    cm_makro_2_change_button.place(x = 160, y = 50, width=60, height=30)

    cm_makro_3_change_button = Button(cm_fenster, text="Makro 3", command=button_action_cm_makro_3)
    cm_makro_3_change_button.place(x = 220, y = 50, width=60, height=30)

exit_button = Button(fenster, text="Beenden", command=quit_fenster)
exit_button.place(x = 450, y = 350, width=120, height=30)

fenster.protocol("WM_DELETE_WINDOW", quit_fenster) # click auf den Close-Button in der Titelleiste

eingabefeld = Entry(fenster, bd=5, width=75)
eingabefeld.place(x = 75, y = 300)

change_makro_button = Button(fenster, text="Change Makros", command=button_action_cm_fenster)
change_makro_button.place(x = 450, y = 20, width=120, height=30)

makro_1_button = Button(fenster, text=str(makro_1_string), command=button_action_makro_1)
makro_1_button.place(x = 450, y = 50, width=120, height=30)

makro_2_button = Button(fenster, text=str(makro_2_string), command=button_action_makro_2)
makro_2_button.place(x = 450, y = 80, width=120, height=30)

makro_3_button = Button(fenster, text=str(makro_3_string), command=button_action_makro_3)
makro_3_button.place(x = 450, y = 110, width=120, height=30)

kick_user_fenster_button = Button(fenster, text="Block", command=button_action_kick_user_fenster)
kick_user_fenster_button.place(x = 450, y = 170, width=120, height=30)

show_user_button = Button(fenster, text="Show all Users", command=button_action_show_user_fenster)
show_user_button.place(x = 450, y = 200, width=120, height=30)

whisper_button = Button(fenster, text="Whisper", command=button_action_whisper)
whisper_button.place(x = 50, y = 350, width=120, height=30)

display_main = Text(fenster, bg="white", height=15, width=50)
display_main.pack(anchor="w", padx=45)

scrollbar = Scrollbar(makro_1_button, width=10)
scrollbar.pack(side=RIGHT, fill=Y)

display_main.config(yscrollcommand=scrollbar.set)
scrollbar.config(command=display_main.yview)



eingabefeld.bind("<Return>", add_message)

def hide():
    kick_user_fenster_button.place_forget()
    show_user_button.configure(state="disabled")

def unhide():
    kick_user_fenster_button.place(x = 450, y = 170, width=120, height=30)
    show_user_button.configure(state="normal")

hide() # unhide only with adminrights

# Menu --------------------------------------------------

info_text = Label(fenster, text = "")

menuleiste = Menu(fenster)

datei_menu = Menu(menuleiste, tearoff=0)
help_menu = Menu(menuleiste, tearoff=0)



def menu_button_action_connect():

    global myPort
    global port_int
    global connect_adress_string

    myPort = Eth.EthernetClient(connect_adress_string,int(port_int),reader)
    myPort.start()
    time.sleep(0.5)

    msgname = "login_name:" + str(register_string)
    myPort.send(msgname.encode())
    time.sleep(0.5)
    
    msgpw = "login_password:" + str(password_string)
    myPort.send(msgpw.encode())
    time.sleep(0.5)



def menu_button_action_disconnect():

    global myPort
    if myPort != None: 
        myPort.close()

def menu_button_action_settings():

    menu_settings_fenster = Toplevel(fenster)
    menu_settings_fenster.title("Einstellungen")
    menu_settings_fenster.geometry('400x300')

    def button_action_menu_settings_password ():

        global password_string
        password_string = menu_settings_eingabefeld_password.get()

    def button_action_menu_settings_connect_adress ():

        global connect_adress_string
        connect_adress_string = menu_settings_eingabefeld_connect_adress.get()

    def button_action_menu_settings_port ():

        global port_int
        port_int = menu_settings_eingabefeld_port.get()

    def button_action_menu_settings_register ():

        global register_string
        register_string = menu_settings_eingabefeld_register.get()
    
    def button_action_menu_exit_settings ():
        
        button_action_menu_settings_password ()
        button_action_menu_settings_connect_adress ()
        button_action_menu_settings_port ()
        button_action_menu_settings_register ()

        menu_settings_fenster.destroy()

    menu_settings_exit_button = Button(menu_settings_fenster, text="Fertig", command=button_action_menu_exit_settings)
    menu_settings_exit_button.place(x = 250, y = 250, width=60, height=30)

    menu_settings_label = Label(menu_settings_fenster, text="Gib die entsrpechenden Verbindungsdaten ein!")
    menu_settings_label.place (x= 50, y = 20, width=300, height=10)

    menu_settings_eingabefeld_password = Entry(menu_settings_fenster, bd=5, width=30, bg = 'white')
    menu_settings_eingabefeld_password.place(x = 120, y = 50)
    menu_settings_eingabefeld_password.insert(0, str(password_string))

    menu_settings_eingabefeld_connect_adress = Entry(menu_settings_fenster, bd=5, width=30, bg = 'white')
    menu_settings_eingabefeld_connect_adress.place(x = 120, y = 100)
    menu_settings_eingabefeld_connect_adress.insert(0, str(connect_adress_string))
  
    menu_settings_eingabefeld_port = Entry(menu_settings_fenster, bd=5, width=30, bg = 'white')
    menu_settings_eingabefeld_port.place(x = 120, y = 150)
    menu_settings_eingabefeld_port.insert(0, str(port_int))

    menu_settings_eingabefeld_register = Entry(menu_settings_fenster, bd=5, width=30, bg = 'white')
    menu_settings_eingabefeld_register.place(x = 120, y = 200)
    menu_settings_eingabefeld_register.insert(0, str(register_string))

    menu_settings_password_button = Button(menu_settings_fenster, text="Password", command=button_action_menu_settings_password)
    menu_settings_password_button.place(x = 50, y = 50, width=60, height=30)

    menu_settings_connect_adress_button = Button(menu_settings_fenster, text="Adress", command=button_action_menu_settings_connect_adress)
    menu_settings_connect_adress_button.place(x = 50, y = 100, width=60, height=30)

    menu_settings_port_button = Button(menu_settings_fenster, text="Port", command=button_action_menu_settings_port)
    menu_settings_port_button.place(x = 50, y = 150, width=60, height=30)

    menu_settings_register_button = Button(menu_settings_fenster, text="Register", command=button_action_menu_settings_register)
    menu_settings_register_button.place(x = 50, y = 200, width=60, height=30)

def action_get_info_dialog():
	m_text = "*********\nAutor: Alexander Fleig\n\
   Date: 20.11.23\n\
   Version: 1.00\n\
   **********"
	messagebox.showinfo(message=m_text, title = "Infos")

menuleiste.add_cascade(label="Datei", menu=datei_menu)
menuleiste.add_cascade(label="Help", menu=help_menu)

datei_menu.add_command(label="Verbinden", command=menu_button_action_connect)
datei_menu.add_command(label="Verbindung trennen", command=menu_button_action_disconnect)

datei_menu.add_separator()
datei_menu.add_command(label="Einstellungen", command=menu_button_action_settings)

datei_menu.add_separator()
datei_menu.add_command(label="Exit", command=quit_fenster)

help_menu.add_command(label="Info!", command=action_get_info_dialog)



fenster.config(menu=menuleiste)     


fenster.mainloop()
