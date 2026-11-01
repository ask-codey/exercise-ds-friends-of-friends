import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.Queue;

// Your three methods. The comment above each one says what it should
// return, with examples. Replace each return line with your own code.
public class Network {
    /*
     * Turn a list of friendships into a network: a dictionary with a key
     * for every person, whose value is the list of that person's friends.
     * A friendship goes both ways, so ("Ava", "Ben") puts Ben in Ava's
     * list AND Ava in Ben's list. Keep each list in the order the
     * friendships come in.
     *
     * [("Ava", "Ben"), ("Ava", "Cara")]
     *   -> { Ava: [Ben, Cara], Ben: [Ava], Cara: [Ava] }
     */
    public static Map<String, List<String>> buildNetwork(String[][] friendships) {
        return new LinkedHashMap<>();
    }

    /*
     * Suggest "people you may know": everyone exactly two steps away from
     * person, a friend of a friend who isn't person and isn't already one
     * of their friends. List each one once, in A to Z order.
     *
     * In the sample network (the demo's):
     * suggestFriends(network, "Ava") -> ["Dev"]
     * suggestFriends(network, "Dev") -> ["Ava", "Fay"]
     * suggestFriends(network, "Gus") -> []
     */
    public static List<String> suggestFriends(Map<String, List<String>> network, String person) {
        return new ArrayList<>();
    }

    /*
     * The fewest friendships between two people: 1 for friends, 2 for a
     * friend of a friend, and so on. A person is 0 steps from themselves.
     * Return -1 when no chain of friendships joins them, or when either
     * person isn't in the network.
     *
     * In the sample network (the demo's):
     * stepsBetween(network, "Ava", "Fay") -> 4
     * stepsBetween(network, "Ava", "Ava") -> 0
     * stepsBetween(network, "Ava", "Hana") -> -1
     */
    public static int stepsBetween(Map<String, List<String>> network, String from, String to) {
        return 0;
    }
}
