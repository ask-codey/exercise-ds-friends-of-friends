namespace FriendsOfFriends;

// Your three methods. The comment above each one says what it should
// return, with examples. Replace each return line with your own code.
public static class Network
{
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
    public static Dictionary<string, List<string>> BuildNetwork(List<(string, string)> friendships)
    {
        return new Dictionary<string, List<string>>();
    }

    /*
     * Suggest "people you may know": everyone exactly two steps away from
     * person, a friend of a friend who isn't person and isn't already one
     * of their friends. List each one once, in A to Z order.
     *
     * In the sample network (the demo's):
     * SuggestFriends(network, "Ava") -> ["Dev"]
     * SuggestFriends(network, "Dev") -> ["Ava", "Fay"]
     * SuggestFriends(network, "Gus") -> []
     */
    public static List<string> SuggestFriends(Dictionary<string, List<string>> network, string person)
    {
        return new List<string>();
    }

    /*
     * The fewest friendships between two people: 1 for friends, 2 for a
     * friend of a friend, and so on. A person is 0 steps from themselves.
     * Return -1 when no chain of friendships joins them, or when either
     * person isn't in the network.
     *
     * In the sample network (the demo's):
     * StepsBetween(network, "Ava", "Fay") -> 4
     * StepsBetween(network, "Ava", "Ava") -> 0
     * StepsBetween(network, "Ava", "Hana") -> -1
     */
    public static int StepsBetween(Dictionary<string, List<string>> network, string from, string to)
    {
        return 0;
    }
}
