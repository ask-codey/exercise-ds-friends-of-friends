import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import org.junit.jupiter.api.Test;

// The tests for your three methods. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.
class NetworkTest {
    private static final String[][] FRIENDSHIPS = {{"Ava", "Ben"}, {"Ava", "Cara"}, {"Ben", "Dev"}, {"Cara", "Dev"}, {"Dev", "Eli"}, {"Eli", "Fay"}, {"Gus", "Hana"}};

    // The same network, written out, so the suggestion and steps tests don't depend on buildNetwork.
    private static Map<String, List<String>> sample() {
        Map<String, List<String>> network = new LinkedHashMap<>();
        network.put("Ava", new ArrayList<>(List.of("Ben", "Cara")));
        network.put("Ben", new ArrayList<>(List.of("Ava", "Dev")));
        network.put("Cara", new ArrayList<>(List.of("Ava", "Dev")));
        network.put("Dev", new ArrayList<>(List.of("Ben", "Cara", "Eli")));
        network.put("Eli", new ArrayList<>(List.of("Dev", "Fay")));
        network.put("Fay", new ArrayList<>(List.of("Eli")));
        network.put("Gus", new ArrayList<>(List.of("Hana")));
        network.put("Hana", new ArrayList<>(List.of("Gus")));
        return network;
    }

    private static void link(Map<String, List<String>> network, String first, String second) {
        network.computeIfAbsent(first, key -> new ArrayList<>()).add(second);
        network.computeIfAbsent(second, key -> new ArrayList<>()).add(first);
    }

    @Test
    void buildNetworkWritesEachFriendshipBothWays() {
        Map<String, List<String>> network = Network.buildNetwork(FRIENDSHIPS);
        assertTrue(network.containsKey("Ava"), "Not yet: Ava is in a friendship, so she needs a key in the network.");
        assertEquals(List.of("Ben", "Cara"), network.get("Ava"), "Not yet: Ava's friends are Ben and Cara, in that order.");
        assertTrue(network.containsKey("Ben"), "Not yet: {\"Ava\", \"Ben\"} goes both ways, so Ben needs a key too.");
        assertEquals(List.of("Ava", "Dev"), network.get("Ben"), "Not yet: Ben's friends are Ava and Dev: each friendship is written in both lists.");
    }

    @Test
    void buildNetworkHasEveryPersonOnce() {
        Map<String, List<String>> network = Network.buildNetwork(FRIENDSHIPS);
        assertEquals(8, network.size(), "Not yet: 7 friendships between 8 people make 8 keys.");
        assertEquals(List.of("Ben", "Cara", "Eli"), network.get("Dev"), "Not yet: Dev's friends are Ben, Cara and Eli.");
        assertEquals(List.of("Gus"), network.get("Hana"), "Not yet: Hana's one friend is Gus.");
    }

    @Test
    void suggestFriendsFindsFriendsOfFriends() {
        assertEquals(List.of("Dev"), Network.suggestFriends(sample(), "Ava"), "Not yet: Ava's friends Ben and Cara both know Dev, so Dev is the one suggestion.");
        assertEquals(List.of("Dev"), Network.suggestFriends(sample(), "Fay"), "Not yet: Fay's friend Eli knows Dev.");
    }

    @Test
    void suggestFriendsSkipsFriendsAndListsEachOnceFromAToZ() {
        assertEquals(List.of("Ava", "Fay"), Network.suggestFriends(sample(), "Dev"), "Not yet: Dev may know Ava (through Ben and Cara) and Fay (through Eli), listed once each, A to Z.");
        assertEquals(List.of("Cara", "Eli"), Network.suggestFriends(sample(), "Ben"), "Not yet: Ben may know Cara and Eli, but not Ava or Dev, who are already his friends.");
    }

    @Test
    void suggestFriendsCanBeEmpty() {
        assertEquals(List.of("Ben", "Eli"), Network.suggestFriends(sample(), "Cara"), "Not yet: Cara may know Ben and Eli.");
        assertEquals(List.of(), Network.suggestFriends(sample(), "Gus"), "Not yet: Gus's only friend is Hana, who knows nobody else, so there's nobody to suggest.");
    }

    @Test
    void stepsBetweenCountsTheFewestFriendships() {
        assertEquals(1, Network.stepsBetween(sample(), "Ava", "Ben"), "Not yet: Ava and Ben are friends: 1 step.");
        assertEquals(2, Network.stepsBetween(sample(), "Ava", "Dev"), "Not yet: Dev is a friend of Ava's friends: 2 steps.");
        assertEquals(4, Network.stepsBetween(sample(), "Ava", "Fay"), "Not yet: Ava to Fay is 4 steps: Ben or Cara, Dev, Eli, Fay.");
        assertEquals(4, Network.stepsBetween(sample(), "Fay", "Ava"), "Not yet: friendships go both ways, so Fay to Ava is 4 steps too.");
        assertEquals(0, Network.stepsBetween(sample(), "Ava", "Ava"), "Not yet: a person is 0 steps from themselves.");
    }

    @Test
    void stepsBetweenIsMinusOneWhenNothingJoinsThem() {
        assertEquals(-1, Network.stepsBetween(sample(), "Ava", "Hana"), "Not yet: no chain of friendships joins Ava and Hana.");
        assertEquals(-1, Network.stepsBetween(sample(), "Ava", "Zoe"), "Not yet: Zoe isn't in the network.");
    }

    @Test
    void stepsBetweenWorksOnALongChain() {
        Map<String, List<String>> chain = new LinkedHashMap<>();
        for (int i = 0; i < 1999; i++) {
            link(chain, "Person " + i, "Person " + (i + 1));
        }
        assertEquals(1999, Network.stepsBetween(chain, "Person 0", "Person 1999"), "Not yet: 2,000 people in a line are 1,999 steps from end to end.");
    }

    @Test
    void stepsBetweenFindsTheFewestStepsInABigGrid() {
        Map<String, List<String>> grid = new LinkedHashMap<>();
        for (int row = 0; row < 30; row++) {
            for (int column = 0; column < 30; column++) {
                if (column < 29) link(grid, "R" + row + "C" + column, "R" + row + "C" + (column + 1));
                if (row < 29) link(grid, "R" + row + "C" + column, "R" + (row + 1) + "C" + column);
            }
        }
        assertEquals(5, Network.stepsBetween(grid, "R0C0", "R0C5"), "Not yet: 5 steps along the top row.");
        assertEquals(58, Network.stepsBetween(grid, "R0C0", "R29C29"), "Not yet: corner to corner of a 30 by 30 grid is 58 steps.");
    }
}
