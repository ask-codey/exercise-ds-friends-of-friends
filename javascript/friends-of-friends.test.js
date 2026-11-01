// The tests for your three functions. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.
import assert from "node:assert/strict";
import { test } from "node:test";
import { buildNetwork, stepsBetween, suggestFriends } from "./friends-of-friends.js";

const FRIENDSHIPS = [["Ava", "Ben"], ["Ava", "Cara"], ["Ben", "Dev"], ["Cara", "Dev"], ["Dev", "Eli"], ["Eli", "Fay"], ["Gus", "Hana"]];

// The same network, written out, so the suggestion and steps tests don't depend on buildNetwork.
function sample() {
  return new Map([
    ["Ava", ["Ben", "Cara"]],
    ["Ben", ["Ava", "Dev"]],
    ["Cara", ["Ava", "Dev"]],
    ["Dev", ["Ben", "Cara", "Eli"]],
    ["Eli", ["Dev", "Fay"]],
    ["Fay", ["Eli"]],
    ["Gus", ["Hana"]],
    ["Hana", ["Gus"]],
  ]);
}

function link(network, first, second) {
  if (!network.has(first)) network.set(first, []);
  if (!network.has(second)) network.set(second, []);
  network.get(first).push(second);
  network.get(second).push(first);
}

test("buildNetwork writes each friendship both ways", () => {
  const network = buildNetwork(FRIENDSHIPS);
  assert.ok(network.has("Ava"), "Not yet: Ava is in a friendship, so she needs a key in the network.");
  assert.deepEqual(network.get("Ava"), ["Ben", "Cara"], "Not yet: Ava's friends are Ben and Cara, in that order.");
  assert.ok(network.has("Ben"), 'Not yet: ["Ava", "Ben"] goes both ways, so Ben needs a key too.');
  assert.deepEqual(network.get("Ben"), ["Ava", "Dev"], "Not yet: Ben's friends are Ava and Dev: each friendship is written in both lists.");
});

test("buildNetwork has every person once", () => {
  const network = buildNetwork(FRIENDSHIPS);
  assert.equal(network.size, 8, "Not yet: 7 friendships between 8 people make 8 keys.");
  assert.deepEqual(network.get("Dev"), ["Ben", "Cara", "Eli"], "Not yet: Dev's friends are Ben, Cara and Eli.");
  assert.deepEqual(network.get("Hana"), ["Gus"], "Not yet: Hana's one friend is Gus.");
});

test("suggestFriends finds friends of friends", () => {
  assert.deepEqual(suggestFriends(sample(), "Ava"), ["Dev"], "Not yet: Ava's friends Ben and Cara both know Dev, so Dev is the one suggestion.");
  assert.deepEqual(suggestFriends(sample(), "Fay"), ["Dev"], "Not yet: Fay's friend Eli knows Dev.");
});

test("suggestFriends skips friends and lists each once, from A to Z", () => {
  assert.deepEqual(suggestFriends(sample(), "Dev"), ["Ava", "Fay"],
    "Not yet: Dev may know Ava (through Ben and Cara) and Fay (through Eli), listed once each, A to Z.");
  assert.deepEqual(suggestFriends(sample(), "Ben"), ["Cara", "Eli"],
    "Not yet: Ben may know Cara and Eli, but not Ava or Dev, who are already his friends.");
});

test("suggestFriends can be empty", () => {
  assert.deepEqual(suggestFriends(sample(), "Cara"), ["Ben", "Eli"], "Not yet: Cara may know Ben and Eli.");
  assert.deepEqual(suggestFriends(sample(), "Gus"), [],
    "Not yet: Gus's only friend is Hana, who knows nobody else, so there's nobody to suggest.");
});

test("stepsBetween counts the fewest friendships", () => {
  assert.equal(stepsBetween(sample(), "Ava", "Ben"), 1, "Not yet: Ava and Ben are friends: 1 step.");
  assert.equal(stepsBetween(sample(), "Ava", "Dev"), 2, "Not yet: Dev is a friend of Ava's friends: 2 steps.");
  assert.equal(stepsBetween(sample(), "Ava", "Fay"), 4, "Not yet: Ava to Fay is 4 steps: Ben or Cara, Dev, Eli, Fay.");
  assert.equal(stepsBetween(sample(), "Fay", "Ava"), 4, "Not yet: friendships go both ways, so Fay to Ava is 4 steps too.");
  assert.equal(stepsBetween(sample(), "Ava", "Ava"), 0, "Not yet: a person is 0 steps from themselves.");
});

test("stepsBetween is -1 when nothing joins them", () => {
  assert.equal(stepsBetween(sample(), "Ava", "Hana"), -1, "Not yet: no chain of friendships joins Ava and Hana.");
  assert.equal(stepsBetween(sample(), "Ava", "Zoe"), -1, "Not yet: Zoe isn't in the network.");
});

test("stepsBetween works on a long chain", () => {
  const chain = new Map();
  for (let i = 0; i < 1999; i++) {
    link(chain, `Person ${i}`, `Person ${i + 1}`);
  }
  assert.equal(stepsBetween(chain, "Person 0", "Person 1999"), 1999, "Not yet: 2,000 people in a line are 1,999 steps from end to end.");
});

test("stepsBetween finds the fewest steps in a big grid", () => {
  const grid = new Map();
  for (let row = 0; row < 30; row++) {
    for (let column = 0; column < 30; column++) {
      if (column < 29) link(grid, `R${row}C${column}`, `R${row}C${column + 1}`);
      if (row < 29) link(grid, `R${row}C${column}`, `R${row + 1}C${column}`);
    }
  }
  assert.equal(stepsBetween(grid, "R0C0", "R0C5"), 5, "Not yet: 5 steps along the top row.");
  assert.equal(stepsBetween(grid, "R0C0", "R29C29"), 58, "Not yet: corner to corner of a 30 by 30 grid is 58 steps.");
});
