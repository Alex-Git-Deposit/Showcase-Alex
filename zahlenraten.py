#!/usr/bin/python3

import random     # für Zufallszahlen
import time       # für Pause (sleep)

def PrintKopf():
   print("\n\n----------------------- Zahlenraten ----------------------------")
   print("ich würfle eine Zahl zwischen 0 und 100")
   print("und du sollst sie in so wenigen Versuchen wie möglich zu erraten")
   print("\nwenn du nicht mehr magst, gib eine negative Zahl ein")
#   ende der Funktion PrintKopf

def zuviel_falsch():
   print("\n\nDas wars! Du willst offenbar nur Unsinn treiben und darauf habe ich keine Lust!")


run = True

while( run ):

   PrintKopf()

   wuerfelzahl =  int(random.random() * 100)

   richtig = False
   durchlauf = 0
   eingabe_falsch = 0

   while( not richtig ):

      durchlauf += 1
   
      try:
         zahl = int(input("dein Tip:"))
      
      except ValueError:
         eingabe_falsch += 1
         print("\nDas ist gar keine Zahl!\n")
         if eingabe_falsch > 4:
            zuviel_falsch()
            run = False
            time.sleep(1)
            break
         continue
 

      if( zahl < 0 ):   # nach Eingabe einer negativen Zahl
         run = False    # Hauptschleife beenden
         break          # innere Schleife hier abbrechen

      if( zahl > 100 ):
         eingabe_falsch += 1
         print("\nDas war viel zu hoch! Du solltest eine Zahl zwischen 0 und 100 wählen.")
         if eingabe_falsch > 4:
            zuviel_falsch()
            run = False
            time.sleep(1)
            break

      if( wuerfelzahl > zahl ):
         print("\ndu liegst zu niedrig\n")
      elif ( wuerfelzahl < zahl ):
         print("\netwas kleiner bitte\n")
      else:
         print("Du liegst richtig und hast die Zahl in {0} Versuchen erraten".format(durchlauf))
         durchlauf = 0
         eingabe_falsch = 0
         richtig = True
         time.sleep(3)

      # ende des if/elif/else-Blocks
   #  ende der inneren While-Schleife
# ende der Hauptschleife

print("\n\nbis zum nächsten mal, Servus\n\n")

