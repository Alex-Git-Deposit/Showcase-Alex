import threading 	#für mulitherading  - alternativ asyncio
import socket		# für TCP/IP oder UDP - Soccet (Ethernet Verbindungen)
import time
import struct

def printhex( text, data ):
	'''gibt text gefolgt von data in HEX auf der console aus'''
	message = gethex(data)
	print(text + " >" + message + "<\n")

def gethex( data ):
	'''gibt data als HEX-String zurück  Beispiel A0 6F 3D ...'''
	message = ""
	xbyte = "{0:02X} "
	for x in data:
		message += xbyte.format(x)
	return message	

class EthernetClient(threading.Thread):
	'''Ein TCP/IP socket client'''
	def __init__(self, host, port, callback, conhandler = None): 
		'''host = Hostname oder IP-Adresse des Servers
			port = Portnummer auf der der Server erreichbar ist
			callback = Funktion die bei Empfang einer Message aufgerufen werden soll (Reader( data ))
			conhandler = Funktion die bei Connect und Disconnect aufgerufen wird 
			Conhandler(connected:bool, details:string, soc:socket)'''
		super().__init__() 
		self.host = host
		self.port = port
		self.callback = callback
		self.conhandler = conhandler
		self.terminate = 0
		self.mySocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
		self.connected = False

	def run(self):  # thread-Funktion - hängt ständig lesend am Port
		'''startet den Reader-Thread um Messages vom Ethernet empfangen zu können.
			Wenn Messages empfangen werden, werden diese an die callback-Funktion übergeben'''
		while(self.terminate == 0):

			#print("trying to connect")
			self.mySocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
			result = self.mySocket.connect_ex((self.host, self.port))
			if( result == 0):			# 0 ist erfolgreich connected
				#print("connected ....")
				self.connected = True
				if self.conhandler:
					addr = self.host + ":" + str(self.port)
					self.conhandler(True, addr, self.mySocket)
				while(self.terminate == 0):
					try:
						data = self.mySocket.recv(1024)
					except(Exception):
						data = None
						#print("exception on recv")

					if(data):
						# Ab zur Verarbeitung
						if (len(data)>10 and data[0] == 0x27 and data[1] == 0xA2 and data[2] == 0x53 and data[3] == 0xFF):
							telegramlength = data[4] * 256 + data[5]
							TL = telegramlength + 10
							if (len(data) == TL and data[TL-4] == 0x53 and data[TL-3] == 0x27 and data[TL-2] == 0xA2  and data[TL-1] == 0x00 ):

								message = data[6:TL-4:]
							else:
								message = "Error: XT7" # falscher Trailer oder falsche Länge
						else:
							message = "Error: DZ9" # falscher Header oder zu kurz

						if( self.callback):

							self.callback(message)
							#self.callback(data)
						else:
							print("no callback defined!")
							printhex( "Telegram gelesen", data )
					else:
						self.connected = False
						if self.conhandler:
							addr = self.host + ":" + str(self.port)
							self.conhandler(False, addr, self.mySocket)
						#print("connection broken")
						break

				# end inner while
			else:
				if( self.mySocket ):
					self.mySocket.close()
				time.sleep(0.5)
			# end if result
		# end outher while
	#end run
	
	def send(self, data):
		'''sendet die übergebene Message an den Server
			und hängt zusätzlich einen Sicherheitscode an'''
		if( self.mySocket and self.connected):
			message_length = len(data)

			header = bytearray([0x27,0xA2,0x53,0xFF,0x00,0x00])
			header[4] = message_length // 256
			header[5] = message_length % 256
			
			trailer = bytearray([0x53,0x27,0xA2,0x00])

			self.mySocket.sendall( header + data + trailer)
		else:
			print("send ERROR not connected!")
	# end send
		
	def isConnected(self):
		'''True wenn die Verbindung zum Server besteht'''
		return self.connected

	def isRunning(self):
		'''True wenn der Reader-Thread läuft'''
		return self.terminate == 0

	def close(self):
		'''stopt die threads und schließt den socket
			ein Neustart ist nicht vorgesehen'''
		self.terminate = 1
		self.mySocket.close()
	# end close
# end class EthernetClient

##################################################################
###################################################################

class EthernetServerEx(threading.Thread):
	'''ein TCP/IP Server der mehrere Verbindungen annimmt'''
	def __init__(self, host, port, callback, conhandler = None, maxConnections = 0): 
		'''host = IP-Adresse auf der der Server horchen soll - leerstring wenn keine Einschränkung besteht
			port = Portnummer auf der der Server erreichbar ist
			callback = Funktion die bei Empfang einer Message aufgerufen werden soll (Reader( data:bytes, soc:socket, addr:string ))
			conhandler = Funktion die bei Connect und Disconnect aufgerufen wird 
			Conhandler(connected:bool, details:string, soc:socket)
			maxConnections = Anzahl von gleichzeitigen Verbindungen die bedient werden sollen
			0 = unbegrenzt'''
		super().__init__() 
		#print("Eth NEW " + str(id(self)))
		self.host = host
		self.port = port
		self.callback = callback
		self.conhandler = conhandler
		self.maxConnections = maxConnections
		self.connections = 0
		self.terminate = 0
		self.myAcceptSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
		self.mySockets = [None]

		self.myAcceptSocket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)	# damit der Port nach schließen gleich wieder verwendbar ist
		self.myAcceptSocket.bind((self.host, self.port))
		self.myAcceptSocket.listen(10)

	def run(self):  # thread-Funktion - hängt ständig lesend am Port
		'''startet den Reader-Thread um Messages vom Ethernet empfangen zu können
			Wenn Messages empfangen werden, werden diese an die callback-Funktion übergeben'''

		while( not self.terminate):

			#print("Eth hauptschleife self.terminate=" + str(self.terminate) + " id="  + str(id(self)))

			while( self.maxConnections > 0 and self.connections >= self.maxConnections and not self.terminate):
				time.sleep(0.5)		# kein weiteres accept solange maxConnections erreicht ist

			#print("Eth innere Schleifeself.terminate=" + str(self.terminate) + " id="  + str(id(self)))

			soc = None

			if( not self.terminate ):
				try:
					#print("Eth Accept" + " id="  + str(id(self)))
					soc, addr = self.myAcceptSocket.accept()
				except Exception as e:
					print(e)
					time.sleep(1)

			#print("Eth 1 accepted " + str(soc) + " id="  + str(id(self)))
				
			if( not self.terminate and soc):
				#print("Eth 2 accepted " + str(soc) + " id="  + str(id(self)))
				self.mySockets.append(soc)
				accepted = True
				if self.conhandler:
					accepted = self.conhandler(True, addr, soc)
				#print("Eth connected " + str(addr) + " id="  + str(id(self)) + "\n")
				if (not accepted):
					self.disconnect(soc)
					soc.close()
				else:
					self.connections += 1
					t = threading.Thread(target=self.socketReadThread, args=(soc,addr))
					t.start()
			#else:
				#print("Eth - war nix" + " id="  + str(id(self)))

			# end if result
		# end outher while
		#print("Eth - hauptschleife verlassen self.terminate=" + str(self.terminate) + " id="  + str(id(self)))
	#end run
	
	def send(self, data, soc):
		'''sendet die übergebene Message an den client mit dem übergebenen Socket soc 
		der Socket wird den beiden callback Funktionen übergeben'''
		try:
			if soc in self.mySockets:
				'''sendet die übergebene Message an Alle connections außer an den übergebenen Socket
				der Socket wird den beiden callback Funktionen übergeben
				mit zusätzlichem Sicherheitscode'''
				message_length = len(data)
				
				header = bytearray([0x27,0xA2,0x53,0xFF,0x00,0x00])
				header[4] = message_length // 256
				header[5] = message_length % 256
			
				trailer = bytearray([0x53,0x27,0xA2,0x00])

				soc.sendall(header + data + trailer)
			else:
				print("send ERROR not connected!")
		except:
			print("send ERROR not connected!")
	# end send

	def sendAll(self, data, soc):
		for s in self.mySockets:
			if s != soc and s != None:
				try:
					self.send(data, s)
				except:
					print("send ERROR not connected!") # client hat gerade die Verbindung geschlossen
	# end send


	def isConnected(self):
		'''True wenn mindestens eine Verbindung besteht'''
		return (len(self.mySockets) > 0)
	
	def disconnect(self, soc):
		'''interne Funktion!  zum Abbrechen einer Verbindung ist die Funktion kill() zu verwenden!'''
		self.mySockets.remove(soc)

	def kill(self,soc):
		'''schließt den übergebenen Socket und beendet damit die Verbindung'''
		try:
			soc.shutdown(socket.SHUT_RDWR)
		except(Exception):
			None

	def close(self):
		'''stopt die threads und schließt alle offenen sockets
			ein Neustart ist nicht vorgesehen. Das Serverobjekt ist danach zu entsorgen'''
		#print("Eth close " + " id="  + str(id(self)))
		self.terminate = 1
		s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
		s.connect_ex(("127.0.0.1", self.port))		# beendet das wartende accept

		try:
			for soc in self.mySockets:
				try:
					if( soc ):		# der erste in der Liste ist None
						soc.shutdown(socket.SHUT_RDWR)
				except Exception as e:
					print("soc.shutdown - " + str(e))

			self.myAcceptSocket.shutdown(socket.SHUT_RDWR)
		except Exception as e:
			print("AcceptSocket.shutdown - " + str(e))

			# sauberes Schließen und wieder öffnen - wie geht das?

	# end close

	def __del__(self):
		'''destructor - ruft nötigenfalls implizit close() auf'''
		if not self.terminate:	# bereits geschlossen?
			self.close()
	

	def socketReadThread(self,mySocket:socket,addr:str):
		'''interne thread-Funktion'''
		#print("Eth socketReadThread start")

		while(self.terminate == 0):
			try:
				data = mySocket.recv(1024)
			except(Exception):
				data = None


			if(data):
				# Ab zur Verarbeitung
				if (len(data)>10 and data[0] == 0x27 and data[1] == 0xA2 and data[2] == 0x53 and data[3] == 0xFF):
					telegramlength = data[4] * 256 + data[5]
					TL = telegramlength + 10
					if (len(data) == TL and data[TL-4] == 0x53 and data[TL-3] == 0x27 and data[TL-2] == 0xA2  and data[TL-1] == 0x00 ):

						message = data[6:TL-4:]
					else:
						message = "Error: XT7" # falscher Trailer oder falsche Länge
				else:
					message = "Error: DZ9" # falscher Header oder zu kurz

				if( self.callback):
					self.callback(message, mySocket, addr)
					#self.callback(data, mySocket, addr)
				else:
					print("no callback defined!")
			else:
				if( self.conhandler):
					self.conhandler(False,addr,mySocket)
				mySocket.close()
				self.disconnect(mySocket)
				self.connections -= 1
				break
	# ende socketReadThread
	#print("Eth socketReadThread stop")

# end class EthernetServerEx

##################################################################


class EthernetServer(EthernetServerEx):
	'''ein einfacher TCP/IP Server der _EINE_ Verbindung annimmt
		Der Server wartet auf ein connect, bedient den client dann bis dieser die connection schließt
		und wartet dann wieder auf ein connect'''
	def __init__(self, host, port, callback, conhandler = None): 
		'''host = IP-Adresse auf der der Server horchen soll - leerstring wenn keine Einschränkung besteht
			port = Portnummer auf der der Server erreichbar ist
			callback = Funktion die bei Empfang einer Message aufgerufen werden soll (Reader( data:bytes ))
			conhandler = Funktion die bei Connect und Disconnect aufgerufen wird 
			Conhandler(connected:bool, details:string, soc:socket)'''
		super().__init__(host, port, callback, conhandler, maxConnections = 1)
# end class EthernetServer



