# Your three functions. The comment under each one says what it should
# return, with examples. Replace each return line with your own code.


def build_network(friendships):
    """
    Turn a list of friendships into a network: a dictionary with a key
    for every person, whose value is the list of that person's friends.
    A friendship goes both ways, so ("Ava", "Ben") puts Ben in Ava's
    list AND Ava in Ben's list. Keep each list in the order the
    friendships come in.

    [("Ava", "Ben"), ("Ava", "Cara")]
      -> { Ava: [Ben, Cara], Ben: [Ava], Cara: [Ava] }
    """
    return {}


def suggest_friends(network, person):
    """
    Suggest "people you may know": everyone exactly two steps away from
    person, a friend of a friend who isn't person and isn't already one
    of their friends. List each one once, in A to Z order.

    In the sample network (the demo's):
    suggest_friends(network, "Ava") -> ["Dev"]
    suggest_friends(network, "Dev") -> ["Ava", "Fay"]
    suggest_friends(network, "Gus") -> []
    """
    return []


def steps_between(network, start, end):
    """
    The fewest friendships between start and end: 1 for friends, 2 for a
    friend of a friend, and so on. A person is 0 steps from themselves.
    Return -1 when no chain of friendships joins them, or when either
    person isn't in the network.

    In the sample network (the demo's):
    steps_between(network, "Ava", "Fay") -> 4
    steps_between(network, "Ava", "Ava") -> 0
    steps_between(network, "Ava", "Hana") -> -1
    """
    return 0
