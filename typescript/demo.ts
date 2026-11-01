// Tries your functions on a small social network. Run it with: node demo.ts
import { buildNetwork, stepsBetween, suggestFriends } from "./friends-of-friends.ts";

const friendships: [string, string][] = [["Ava", "Ben"], ["Ava", "Cara"], ["Ben", "Dev"], ["Cara", "Dev"], ["Dev", "Eli"], ["Eli", "Fay"], ["Gus", "Hana"]];
const network = buildNetwork(friendships);

console.log(`Network: ${network.size} people`);
console.log(`Ava's friends: ${(network.get("Ava") ?? []).join(", ")}`);
console.log(`People Ava may know: ${suggestFriends(network, "Ava").join(", ")}`);
console.log(`People Dev may know: ${suggestFriends(network, "Dev").join(", ")}`);
console.log(`Steps from Ava to Fay: ${stepsBetween(network, "Ava", "Fay")}`);
console.log(`Steps from Ava to Hana: ${stepsBetween(network, "Ava", "Hana")}`);
