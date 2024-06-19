# Palindronm_checker

user_input = input("Gib bitte ein Wort ein: ")

def palindrom_check():
    global user_input
    
    if (user_input[::-1] == user_input):
        print("Dieses Wort ist ein Palindrom!")
    else:
        print("Dieses Wort ist kein Palindrom!")
        
palindrom_check()