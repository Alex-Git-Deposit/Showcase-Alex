# einfacher passwort generator
import random
import string

# Generiere eine zufällige Zeichenkette
def generate_random_string(length):
    # Mischung aus Großbuchstaben, Kleinbuchstaben und Ziffern
    characters = string.ascii_letters + string.digits
    return ''.join(random.choice(characters) for _ in range(length))

# Beispielaufruf
# print(generate_random_string(10))
# Beispielaufruf
# print(generate_random_string_with_choices(10))

# Generiere eine zufällige Zeichenkette
# Das hier ist effizienter laut phind
def generate_random_string_with_choices(length):
    characters = string.ascii_letters + string.digits + string.punctuation
    chosen_chars = random.choices(characters, k=length)
    random.shuffle(chosen_chars)
    return ''.join(chosen_chars)


def user_choosing():
    try:
        user_input = int(input("Gib bitte die Länge an zufälligen Zeichen an die ich generieren soll: "))
    except:
        print("You Failed! Ich benutze jetzt die Zahl 8")
        user_input = 8
    finally:
        print(generate_random_string_with_choices(user_input))
    
# user_choosing()
def explain_generate_rbc():
    print("Du willst etwas Zufälliges?")
    print(" ")
    print("1 für: ascii_letters_uppercase")
    print("2 für: ascii_letters_lowercase")
    print("3 für: digits")
    print("4 für: punctuation")
    print("5 für: all together")
    print(" ")
    print("Wenn du eine kombination haben möchtest dann Tippe . dazu")
    print("Beispiel: 1.2 kombiniert dir uppercase und lowercase")
    print(" ")
    print("Du kannst auch mehrere Kombinationen bilden wie 1.4.2.5")
    print(" ")

def generate_random_by_choice():
    try:
        explain_generate_rbc()
        user_input = str(input("Bitte gib die Zahlen ein die du wünschst: "))
        parts = user_input.split('.')
        print(" ")
        user_input_2 = input("Gib mir die Länge an die du haben möchtest: ")
        length = int(user_input_2)
        
        characters = ''
        for x in parts:
            if (x == "1"):
                characters += string.ascii_uppercase
            if (x == "2"):
                characters += string.ascii_lowercase
            if (x == "3"):
                characters += string.digits
            if (x == "4"):
                characters += string.punctuation
            if (x == "5"):
                characters += string.ascii_letters + string.digits + string.punctuation
        chosen_chars = random.choices(characters, k=length)
        random.shuffle(chosen_chars)
        random.shuffle(chosen_chars)
        random.shuffle(chosen_chars)
        return ''.join(chosen_chars)
    except:
        print("Invalid input")
            
print(generate_random_by_choice())
    


'''

import random
import string
import hashlib

def hash_input(input_string):
    """
    Generiert einen Hash-Wert aus der Eingabe-Zeichenfolge.
    """
    return int(hashlib.sha256(input_string.encode()).hexdigest(), 16)

def generate_random_string_from_seed(seed):
    """
    Generiert eine zufällige Zeichenkette basierend auf einem Seed.
    """
    random.seed(seed)
    characters = string.ascii_letters + string.digits
    return ''.join(random.choices(characters, k=10))  # Beispiel für eine Zeichenkette von 10 Zeichen

def generate_random_string_with_hashed_input(input_string, length=10):
    """
    Akzeptiert eine Zeichenkette als Eingabe, generiert einen Hash-Wert daraus,
    und verwendet diesen als Seed für die Generierung einer zufälligen Zeichenkette.
    """
    seed = hash_input(input_string)
    return generate_random_string_from_seed(seed)

# Beispielaufrufe
input1 = "wfwnofadfo"
input2 = "sdjdpikpdjo2"

print(generate_random_string_with_hashed_input(input1))
print(generate_random_string_with_hashed_input(input2))

'''