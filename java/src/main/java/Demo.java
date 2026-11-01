import java.util.List;
import java.util.Map;

// Tries your methods on a small social network. Run it with: java src/main/java/Demo.java
public class Demo {
    public static void main(String[] args) {
        String[][] friendships = {{"Ava", "Ben"}, {"Ava", "Cara"}, {"Ben", "Dev"}, {"Cara", "Dev"}, {"Dev", "Eli"}, {"Eli", "Fay"}, {"Gus", "Hana"}};
        Map<String, List<String>> network = Network.buildNetwork(friendships);

        System.out.println("Network: " + network.size() + " people");
        System.out.println("Ava's friends: " + String.join(", ", network.getOrDefault("Ava", List.of())));
        System.out.println("People Ava may know: " + String.join(", ", Network.suggestFriends(network, "Ava")));
        System.out.println("People Dev may know: " + String.join(", ", Network.suggestFriends(network, "Dev")));
        System.out.println("Steps from Ava to Fay: " + Network.stepsBetween(network, "Ava", "Fay"));
        System.out.println("Steps from Ava to Hana: " + Network.stepsBetween(network, "Ava", "Hana"));
    }
}
