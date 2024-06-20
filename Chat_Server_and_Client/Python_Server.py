# Python_Server

from tkinter import simpledialog
from tkinter import *
from tkinter import messagebox
import time
import random
import threading
import sys
import select
from Schnittstellen import Ethernet_Module as Eth
import re

fenster = Tk()

fenster.title("My chat Server")
fenster.geometry('350x400')

myPort = None
messages = []

password_user_string = "8832"
password_admin_string = "7792"
connect_adress_string = "localhost"
port_string = "7777"

unlock_admin_string = "unlock_admin"
request_user_info_string = "request_user_info9"

file_path = "C:\\Meins\\Coding\\develop\\Modules\\Python_Server_Banlist.txt"

class person:
	def __init__(self,name,ip,soc):
		self.name = name
		self.ip = ip
		self.Mysocket = soc

connectedPersons = {}
connectedAdmins = {}
blockedPersons = {}
bannedPersons = {}

def reader(data, soc, addr):
	global myPort
	global connectedPersons
	global connectedAdmins
	global blockedPersons
	global request_user_info_string

	msg = data.decode()

	if msg.startswith("/block "):
		if id(soc) in connectedPersons:
			try:
				for key in connectedPersons:
					pers = connectedPersons[key]

					if pers.name == msg[7:]:
						conPers_blockban(pers.Mysocket, pers.ip, 0)
						myPort.kill(pers.Mysocket)
			except:
				myPort.send(str("Name nicht gefunden").encode(),soc)
				
	if msg.startswith("/ban "):
		if id(soc) in connectedPersons:
			try:
				for key in connectedPersons:
					pers = connectedPersons[key]

					if pers.name == msg[7:]:
						conPers_blockban(pers.Mysocket, pers.ip, 1)
						myPort.kill(pers.Mysocket)
			except:
				myPort.send(str("Name nicht gefunden").encode(),soc)

	elif msg.startswith("login_name:"):

		pers = person(msg[11:],addr,soc)
		connectedPersons[id(soc)] = pers
		text = pers.name + " > hat sich registriert"
		myPort.sendAll(text.encode(), None)

	elif msg.startswith("login_password:"):

		pwadmincheck = msg[15:]
		pwusercheck = msg[15:]

		if pwadmincheck == str(password_admin_string):
			
			global unlock_admin_string
			
			if( id(soc) in  connectedPersons):
				pers = connectedPersons[id(soc)]
				connectedAdmins[id(soc)] = pers
				myPort.send(str(unlock_admin_string).encode(),soc)

			else:
				print("unbekannte Person soll als Admin registriert werden")

		elif pwusercheck != str(password_user_string):

			myPort.send("WRONG PASSWORD!".encode(),soc)
			connectedPersons.popitem()
			myPort.kill(soc)

	elif msg.startswith("/w "):
		if id(soc) in connectedPersons:
			try:
				sender = connectedPersons[id(soc)]
				x = re.split("\s", msg)
				whispered_string = x[1]

				for key in connectedPersons:
					pers = connectedPersons[key]

					if pers.name == whispered_string:
						count = len(x)
						text1 = ""
						for i in range(2,count):
							text1 += x[i]
							text1 += " "
						text = sender.name + " > flüstert: " + text1
						myPort.send(str(text).encode(), pers.Mysocket)
						break	# richtigen client gefunden -> ende

			except Exception as e:
				print("Exception: " + str(e))
				myPort.send(str("Name nicht gefunden").encode(),soc)

	elif msg.startswith("request_user_info8"):
		
		UserInfo = ""
		for key in connectedPersons:
			pers = connectedPersons[key]
			UserInfo += pers.name
			UserInfo += "\n" 

		text = str(request_user_info_string) + str(UserInfo)
		myPort.send(text.encode(), soc)

	elif msg.startswith("Error:"):
		
		# die Person nach soc suchen und den Namen melden
		pers:person = connectedPersons[id(soc)]
		text = msg + " >> " + pers.name

		for key in connectedAdmins:
			pers = connectedAdmins[key]
			myPort.send(text.encode(), pers.Mysocket)
		

	else:
		if id(soc) in connectedAdmins:
			pers:person = connectedPersons[id(soc)]
			text = "Admin: " + pers.name + " > " + msg
		
		elif id(soc) in connectedPersons:
			pers:person = connectedPersons[id(soc)]
			text = pers.name + " > " + msg

		else:
			text = str(id(soc)) + " > " + msg

		myPort.sendAll(text.encode(), soc)

	update_display()

def conPers_blockban(soc, addr, ban):
	'''ban kann 0 oder 1 sein\n
	Bei 0 erfolgt der Eintrag in die bockedPersons und bei 1 in die bannedPersons'''
	global myPort
	global connectedPersons
	global connectedAdmins
	global blockedPersons

	if id(soc) in connectedAdmins:
		connectedAdmins.pop(id(soc))

	if id(soc) in connectedPersons:

		item1 = connectedPersons.popitem()
		print(str(item1))
		if ban == 1:
			bannedPersons[addr[0]] = item1[1]
			SaveBannedList()
		else:		
			blockedPersons[addr[0]] = item1[1]
		return

	else:
		print(f"The key {id(soc)} does not exist in the connectedPersons dictionary")


	update_display()
	
def LoadBannedList():
	'''lädt die gespeicherte bannedPersons Liste'''
	global bannedPersons
	global file_path
	try:
		with open(file_path, "r") as file:
			for line in file:
				key, value = line.strip().split(";")
				bannedPersons[key.strip()] = value.strip()
	except:
		print("file not found")

def SaveBannedList():
	global bannedPersons
	global file_path
	try:
		with open(file_path, "w") as file:
			for key, value in bannedPersons.items():
				file.write(f"{key};{value}\n")
	except:
		print("error could not write file")		

def serverstart():
	
	LoadBannedList()	
	menu_button_action_server_start()

def quit_fenster ():
    global myPort
    if myPort != None:
        myPort.close()
    fenster.quit()

def update_display():
	global display_users_loggedin
	global connectedPersons
	global myPort

	displayed_text = ""
	cleanup_range = len(connectedPersons)

	for _ in range (cleanup_range):
	
		displayed_text = ""
	display_users_loggedin.config(text=displayed_text)

	for _ in range (1):

		for key, pers in connectedPersons.items():
			try:
				displayed_text += f"{pers.ip} > {pers.name}\n"
			except KeyError:
				displayed_text += "KeyError\n"

	display_users_loggedin.config(text=displayed_text)

exit_button = Button(fenster, text="Beenden", command=quit_fenster)
exit_button.place(x = 200, y = 300, width=120, height=30)

fenster.protocol("WM_DELETE_WINDOW", quit_fenster) # click auf den Close-Button in der Titelleiste

Start_button = Button(fenster, text="Start", command=serverstart)
Start_button.place(x = 50, y = 300, width=120, height=30)

display_users_loggedin = Label(fenster, text=str(connectedPersons), bg="white", anchor='w')
display_users_loggedin.place(x=50, y=30)
display_users_loggedin.config(width=30, height=10)

# Menu --------------------------------------------------

info_text = Label(fenster, text = "")

menuleiste = Menu(fenster)

datei_menu = Menu(menuleiste, tearoff=0)
help_menu = Menu(menuleiste, tearoff=0)

def menu_button_action_server_start():
    
	global myPort
	info_text = Label(fenster, text = "Server gestartet!")
	info_text.pack()
	myPort = Eth.EthernetServerEx("",7777,reader,handleConnections)
	myPort.start()
	print("Start")
	
def handleConnections(connected, addr, soc):
	global connectedPersons
	if(not connected):
		if (id(soc)) in connectedPersons:
			pers = connectedPersons.pop(id(soc))
			text = pers.name + " hat sich abgemeldet"
		else:
			text = str(id(soc)) + " hat sich abgemeldet"
		myPort.sendAll(text.encode(), soc)

	else:
		print(str(addr))
		print(str(addr[0]))
		if addr[0] in blockedPersons:

			myPort.send("YOU ARE BLOCKED!".encode(),soc)
			return False
		print(str(addr))
		print(str(addr[0]))
		if addr[0] in bannedPersons:

			myPort.send("YOU ARE BANNED!".encode(),soc)
			return False
	
	return True

def menu_button_action_settings():

    menu_settings_fenster = Toplevel(fenster)
    menu_settings_fenster.title("Einstellungen")
    menu_settings_fenster.geometry('400x250')

    info_text = Label(fenster, text = "Menüpunkt Datei>Einstellungen ausgewählt")
    info_text.pack()
    
    def button_action_menu_exit_settings ():

        menu_settings_fenster.destroy()

    def button_action_menu_settings_password ():

        global password_string
        password_string = menu_settings_eingabefeld_password.get()

    def button_action_menu_settings_connect_adress ():

        global connect_adress_string
        connect_adress_string = menu_settings_eingabefeld_connect_adress.get()

    def button_action_menu_settings_port ():

        global port_string
        port_string = menu_settings_eingabefeld_port.get()

    menu_settings_exit_button = Button(menu_settings_fenster, text="Fertig", command=button_action_menu_exit_settings)
    menu_settings_exit_button.place(x = 250, y = 200, width=60, height=30)

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
    menu_settings_eingabefeld_port.insert(0, str(port_string))

    menu_settings_password_button = Button(menu_settings_fenster, text="Password", command=button_action_menu_settings_password)
    menu_settings_password_button.place(x = 50, y = 50, width=60, height=30)

    menu_settings_connect_adress_button = Button(menu_settings_fenster, text="Adress", command=button_action_menu_settings_connect_adress)
    menu_settings_connect_adress_button.place(x = 50, y = 100, width=60, height=30)

    menu_settings_port_button = Button(menu_settings_fenster, text="Port", command=button_action_menu_settings_port)
    menu_settings_port_button.place(x = 50, y = 150, width=60, height=30)

def action_get_info_dialog():
  m_text = "************\nAutor: Alexander Fleig\n\
   Date: 30.09.23\n\
   Version: 1.00\n\
   *************"
  messagebox.showinfo(message=m_text, title = "Infos")

menuleiste.add_cascade(label="Datei", menu=datei_menu)
menuleiste.add_cascade(label="Help", menu=help_menu)

datei_menu.add_command(label="Server starten", command=menu_button_action_server_start)

datei_menu.add_separator()
datei_menu.add_command(label="Server-Einstellungen", command=menu_button_action_settings)

datei_menu.add_separator()
datei_menu.add_command(label="Exit", command=fenster.quit)

help_menu.add_command(label="Info!", command=action_get_info_dialog)


fenster.config(menu=menuleiste)


fenster.mainloop()
