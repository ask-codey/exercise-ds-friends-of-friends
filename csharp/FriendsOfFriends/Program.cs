using FriendsOfFriends;

// Tries your methods on a small social network. Run it with: dotnet run --project FriendsOfFriends
List<(string, string)> friendships = new List<(string, string)> { ("Ava", "Ben"), ("Ava", "Cara"), ("Ben", "Dev"), ("Cara", "Dev"), ("Dev", "Eli"), ("Eli", "Fay"), ("Gus", "Hana") };
Dictionary<string, List<string>> network = Network.BuildNetwork(friendships);

Console.WriteLine("Network: " + network.Count + " people");
Console.WriteLine("Ava's friends: " + string.Join(", ", network.GetValueOrDefault("Ava", new List<string>())));
Console.WriteLine("People Ava may know: " + string.Join(", ", Network.SuggestFriends(network, "Ava")));
Console.WriteLine("People Dev may know: " + string.Join(", ", Network.SuggestFriends(network, "Dev")));
Console.WriteLine("Steps from Ava to Fay: " + Network.StepsBetween(network, "Ava", "Fay"));
Console.WriteLine("Steps from Ava to Hana: " + Network.StepsBetween(network, "Ava", "Hana"));
