using System.Diagnostics;
using FriendsOfFriends;

namespace FriendsOfFriends.Tests;

// The tests for your three methods. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.

[TestClass]
public class NetworkTests
{
    private static readonly List<(string, string)> Friendships = new List<(string, string)> { ("Ava", "Ben"), ("Ava", "Cara"), ("Ben", "Dev"), ("Cara", "Dev"), ("Dev", "Eli"), ("Eli", "Fay"), ("Gus", "Hana") };

    // The same network, written out, so the suggestion and steps tests don't depend on BuildNetwork.
    private static Dictionary<string, List<string>> Sample()
    {
        return new Dictionary<string, List<string>>
        {
            ["Ava"] = new List<string> { "Ben", "Cara" },
            ["Ben"] = new List<string> { "Ava", "Dev" },
            ["Cara"] = new List<string> { "Ava", "Dev" },
            ["Dev"] = new List<string> { "Ben", "Cara", "Eli" },
            ["Eli"] = new List<string> { "Dev", "Fay" },
            ["Fay"] = new List<string> { "Eli" },
            ["Gus"] = new List<string> { "Hana" },
            ["Hana"] = new List<string> { "Gus" },
        };
    }

    private static void Link(Dictionary<string, List<string>> network, string first, string second)
    {
        if (!network.ContainsKey(first)) network[first] = new List<string>();
        if (!network.ContainsKey(second)) network[second] = new List<string>();
        network[first].Add(second);
        network[second].Add(first);
    }

    [TestMethod]
    public void BuildNetworkWritesEachFriendshipBothWays()
    {
        Dictionary<string, List<string>> network = Network.BuildNetwork(Friendships);
        Assert.IsTrue(network.ContainsKey("Ava"), "Not yet: Ava is in a friendship, so she needs a key in the network.");
        CollectionAssert.AreEqual(new List<string> { "Ben", "Cara" }, network["Ava"], "Not yet: Ava's friends are Ben and Cara, in that order.");
        Assert.IsTrue(network.ContainsKey("Ben"), "Not yet: (\"Ava\", \"Ben\") goes both ways, so Ben needs a key too.");
        CollectionAssert.AreEqual(new List<string> { "Ava", "Dev" }, network["Ben"], "Not yet: Ben's friends are Ava and Dev: each friendship is written in both lists.");
    }

    [TestMethod]
    public void BuildNetworkHasEveryPersonOnce()
    {
        Dictionary<string, List<string>> network = Network.BuildNetwork(Friendships);
        Assert.AreEqual(8, network.Count, "Not yet: 7 friendships between 8 people make 8 keys.");
        CollectionAssert.AreEqual(new List<string> { "Ben", "Cara", "Eli" }, network["Dev"], "Not yet: Dev's friends are Ben, Cara and Eli.");
        CollectionAssert.AreEqual(new List<string> { "Gus" }, network["Hana"], "Not yet: Hana's one friend is Gus.");
    }

    [TestMethod]
    public void SuggestFriendsFindsFriendsOfFriends()
    {
        CollectionAssert.AreEqual(new List<string> { "Dev" }, Network.SuggestFriends(Sample(), "Ava"), "Not yet: Ava's friends Ben and Cara both know Dev, so Dev is the one suggestion.");
        CollectionAssert.AreEqual(new List<string> { "Dev" }, Network.SuggestFriends(Sample(), "Fay"), "Not yet: Fay's friend Eli knows Dev.");
    }

    [TestMethod]
    public void SuggestFriendsSkipsFriendsAndListsEachOnceFromAToZ()
    {
        CollectionAssert.AreEqual(new List<string> { "Ava", "Fay" }, Network.SuggestFriends(Sample(), "Dev"), "Not yet: Dev may know Ava (through Ben and Cara) and Fay (through Eli), listed once each, A to Z.");
        CollectionAssert.AreEqual(new List<string> { "Cara", "Eli" }, Network.SuggestFriends(Sample(), "Ben"), "Not yet: Ben may know Cara and Eli, but not Ava or Dev, who are already his friends.");
    }

    [TestMethod]
    public void SuggestFriendsCanBeEmpty()
    {
        CollectionAssert.AreEqual(new List<string> { "Ben", "Eli" }, Network.SuggestFriends(Sample(), "Cara"), "Not yet: Cara may know Ben and Eli.");
        CollectionAssert.AreEqual(new List<string>(), Network.SuggestFriends(Sample(), "Gus"), "Not yet: Gus's only friend is Hana, who knows nobody else, so there's nobody to suggest.");
    }

    [TestMethod]
    public void StepsBetweenCountsTheFewestFriendships()
    {
        Assert.AreEqual(1, Network.StepsBetween(Sample(), "Ava", "Ben"), "Not yet: Ava and Ben are friends: 1 step.");
        Assert.AreEqual(2, Network.StepsBetween(Sample(), "Ava", "Dev"), "Not yet: Dev is a friend of Ava's friends: 2 steps.");
        Assert.AreEqual(4, Network.StepsBetween(Sample(), "Ava", "Fay"), "Not yet: Ava to Fay is 4 steps: Ben or Cara, Dev, Eli, Fay.");
        Assert.AreEqual(4, Network.StepsBetween(Sample(), "Fay", "Ava"), "Not yet: friendships go both ways, so Fay to Ava is 4 steps too.");
        Assert.AreEqual(0, Network.StepsBetween(Sample(), "Ava", "Ava"), "Not yet: a person is 0 steps from themselves.");
    }

    [TestMethod]
    public void StepsBetweenIsMinusOneWhenNothingJoinsThem()
    {
        Assert.AreEqual(-1, Network.StepsBetween(Sample(), "Ava", "Hana"), "Not yet: no chain of friendships joins Ava and Hana.");
        Assert.AreEqual(-1, Network.StepsBetween(Sample(), "Ava", "Zoe"), "Not yet: Zoe isn't in the network.");
    }

    [TestMethod]
    public void StepsBetweenWorksOnALongChain()
    {
        Dictionary<string, List<string>> chain = new Dictionary<string, List<string>>();
        for (int i = 0; i < 1999; i++)
        {
            Link(chain, "Person " + i, "Person " + (i + 1));
        }
        Assert.AreEqual(1999, Network.StepsBetween(chain, "Person 0", "Person 1999"), "Not yet: 2,000 people in a line are 1,999 steps from end to end.");
    }

    [TestMethod]
    public void StepsBetweenFindsTheFewestStepsInABigGrid()
    {
        Dictionary<string, List<string>> grid = new Dictionary<string, List<string>>();
        for (int row = 0; row < 30; row++)
        {
            for (int column = 0; column < 30; column++)
            {
                if (column < 29) Link(grid, "R" + row + "C" + column, "R" + row + "C" + (column + 1));
                if (row < 29) Link(grid, "R" + row + "C" + column, "R" + (row + 1) + "C" + column);
            }
        }
        Assert.AreEqual(5, Network.StepsBetween(grid, "R0C0", "R0C5"), "Not yet: 5 steps along the top row.");
        Assert.AreEqual(58, Network.StepsBetween(grid, "R0C0", "R29C29"), "Not yet: corner to corner of a 30 by 30 grid is 58 steps.");
    }

    [TestMethod]
    public void StepsBetweenIsFastForATownOf90000()
    {
        Dictionary<string, List<string>> town = new Dictionary<string, List<string>>();
        for (int row = 0; row < 300; row++)
        {
            for (int column = 0; column < 300; column++)
            {
                if (column < 299) Link(town, "R" + row + "C" + column, "R" + row + "C" + (column + 1));
                if (row < 299) Link(town, "R" + row + "C" + column, "R" + (row + 1) + "C" + column);
            }
        }
        Stopwatch watch = Stopwatch.StartNew();
        int steps = Network.StepsBetween(town, "R0C0", "R299C299");
        watch.Stop();
        Assert.AreEqual(598, steps, "Not yet: corner to corner of a 300 by 300 grid of 90,000 people is 598 steps.");
        Assert.IsTrue(watch.Elapsed.TotalSeconds < 2, "Not yet: a town of 90,000 people took " + watch.Elapsed.TotalSeconds.ToString("0.0") + " seconds. The right approach takes well under one.");
    }
}
