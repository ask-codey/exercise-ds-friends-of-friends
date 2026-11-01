# Tries your functions on a small social network. Run it with: python demo.py
from friends_of_friends import build_network, steps_between, suggest_friends

friendships = [("Ava", "Ben"), ("Ava", "Cara"), ("Ben", "Dev"), ("Cara", "Dev"), ("Dev", "Eli"), ("Eli", "Fay"), ("Gus", "Hana")]
network = build_network(friendships)

print(f"Network: {len(network)} people")
print("Ava's friends: " + ", ".join(network.get("Ava", [])))
print("People Ava may know: " + ", ".join(suggest_friends(network, "Ava")))
print("People Dev may know: " + ", ".join(suggest_friends(network, "Dev")))
print(f"Steps from Ava to Fay: {steps_between(network, 'Ava', 'Fay')}")
print(f"Steps from Ava to Hana: {steps_between(network, 'Ava', 'Hana')}")
