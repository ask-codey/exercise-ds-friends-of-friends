# The tests for your three functions. You don't need to change this file.
# Each message says what was expected, so a failing test tells you what to look at.
from friends_of_friends import build_network, steps_between, suggest_friends

FRIENDSHIPS = [("Ava", "Ben"), ("Ava", "Cara"), ("Ben", "Dev"), ("Cara", "Dev"), ("Dev", "Eli"), ("Eli", "Fay"), ("Gus", "Hana")]


def sample():
    # The same network, written out, so the suggestion and steps tests don't depend on build_network.
    return {
        "Ava": ["Ben", "Cara"],
        "Ben": ["Ava", "Dev"],
        "Cara": ["Ava", "Dev"],
        "Dev": ["Ben", "Cara", "Eli"],
        "Eli": ["Dev", "Fay"],
        "Fay": ["Eli"],
        "Gus": ["Hana"],
        "Hana": ["Gus"],
    }


def link(network, first, second):
    network.setdefault(first, []).append(second)
    network.setdefault(second, []).append(first)


def test_build_network_writes_each_friendship_both_ways():
    network = build_network(FRIENDSHIPS)
    assert "Ava" in network, "Not yet: Ava is in a friendship, so she needs a key in the network."
    assert network["Ava"] == ["Ben", "Cara"], "Not yet: Ava's friends are Ben and Cara, in that order."
    assert "Ben" in network, 'Not yet: ("Ava", "Ben") goes both ways, so Ben needs a key too.'
    assert network["Ben"] == ["Ava", "Dev"], "Not yet: Ben's friends are Ava and Dev: each friendship is written in both lists."


def test_build_network_has_every_person_once():
    network = build_network(FRIENDSHIPS)
    assert len(network) == 8, "Not yet: 7 friendships between 8 people make 8 keys."
    assert network["Dev"] == ["Ben", "Cara", "Eli"], "Not yet: Dev's friends are Ben, Cara and Eli."
    assert network["Hana"] == ["Gus"], "Not yet: Hana's one friend is Gus."


def test_suggest_friends_finds_friends_of_friends():
    assert suggest_friends(sample(), "Ava") == ["Dev"], "Not yet: Ava's friends Ben and Cara both know Dev, so Dev is the one suggestion."
    assert suggest_friends(sample(), "Fay") == ["Dev"], "Not yet: Fay's friend Eli knows Dev."


def test_suggest_friends_skips_friends_and_lists_each_once_from_a_to_z():
    assert suggest_friends(sample(), "Dev") == ["Ava", "Fay"], \
        "Not yet: Dev may know Ava (through Ben and Cara) and Fay (through Eli), listed once each, A to Z."
    assert suggest_friends(sample(), "Ben") == ["Cara", "Eli"], \
        "Not yet: Ben may know Cara and Eli, but not Ava or Dev, who are already his friends."


def test_suggest_friends_can_be_empty():
    assert suggest_friends(sample(), "Cara") == ["Ben", "Eli"], "Not yet: Cara may know Ben and Eli."
    assert suggest_friends(sample(), "Gus") == [], \
        "Not yet: Gus's only friend is Hana, who knows nobody else, so there's nobody to suggest."


def test_steps_between_counts_the_fewest_friendships():
    assert steps_between(sample(), "Ava", "Ben") == 1, "Not yet: Ava and Ben are friends: 1 step."
    assert steps_between(sample(), "Ava", "Dev") == 2, "Not yet: Dev is a friend of Ava's friends: 2 steps."
    assert steps_between(sample(), "Ava", "Fay") == 4, "Not yet: Ava to Fay is 4 steps: Ben or Cara, Dev, Eli, Fay."
    assert steps_between(sample(), "Fay", "Ava") == 4, "Not yet: friendships go both ways, so Fay to Ava is 4 steps too."
    assert steps_between(sample(), "Ava", "Ava") == 0, "Not yet: a person is 0 steps from themselves."


def test_steps_between_is_minus_one_when_nothing_joins_them():
    assert steps_between(sample(), "Ava", "Hana") == -1, "Not yet: no chain of friendships joins Ava and Hana."
    assert steps_between(sample(), "Ava", "Zoe") == -1, "Not yet: Zoe isn't in the network."


def test_steps_between_works_on_a_long_chain():
    chain = {}
    for i in range(1999):
        link(chain, f"Person {i}", f"Person {i + 1}")
    assert steps_between(chain, "Person 0", "Person 1999") == 1999, "Not yet: 2,000 people in a line are 1,999 steps from end to end."


def test_steps_between_finds_the_fewest_steps_in_a_big_grid():
    grid = {}
    for row in range(30):
        for column in range(30):
            if column < 29:
                link(grid, f"R{row}C{column}", f"R{row}C{column + 1}")
            if row < 29:
                link(grid, f"R{row}C{column}", f"R{row + 1}C{column}")
    assert steps_between(grid, "R0C0", "R0C5") == 5, "Not yet: 5 steps along the top row."
    assert steps_between(grid, "R0C0", "R29C29") == 58, "Not yet: corner to corner of a 30 by 30 grid is 58 steps."
