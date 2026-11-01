# Exercise: Friends of Friends

Exercise code for the lesson **Exercise: Friends of Friends** in the course *Data Structures and Big-O, Demystified* on [ask-codey.com](https://www.ask-codey.com).

A small social app needs two features: a "people you may know" box, and "how many steps apart are these two people?" You'll build its network as a graph, then write both features. There are three functions to finish:

- `buildNetwork` (`BuildNetwork` in C#, `build_network` in Python): turns a list of friendships into a network, a dictionary from each person to the list of their friends
- `suggestFriends` (`SuggestFriends`, `suggest_friends`): the people exactly two steps away, the friends of friends who aren't friends yet
- `stepsBetween` (`StepsBetween`, `steps_between`): the fewest friendships between two people, found with breadth-first search, or -1 when nothing joins them

## Pick your language

The same exercise is here in five languages. Work in the folder for the language you set up; you only need one.

| Folder | Your code | The tests |
|---|---|---|
| `csharp/` | `FriendsOfFriends/Network.cs` | `FriendsOfFriends.Tests/NetworkTests.cs` |
| `java/` | `src/main/java/Network.java` | `src/test/java/NetworkTest.java` |
| `python/` | `friends_of_friends.py` | `test_friends_of_friends.py` |
| `javascript/` | `friends-of-friends.js` | `friends-of-friends.test.js` |
| `typescript/` | `friends-of-friends.ts` | `friends-of-friends.test.ts` |

Each function returns an empty network, an empty list or `0` for now. The comment above it says what it should return, with examples. Replace the `return` line with your own code.

Every folder also has a small demo program that tries your functions on the course's eight-person network. You don't need to change the tests or the demo.

## Run it and check your work

Open your language's folder in VS Code (**File > Open Folder**), then open its terminal (**Terminal > New Terminal**) and run these commands there.

**C#**

```bash
dotnet run --project FriendsOfFriends
dotnet test
```

**Java** (the Maven Wrapper downloads Maven the first time, so the first run takes a minute)

```bash
java src/main/java/Demo.java
./mvnw test
```

On Windows in PowerShell, write `.\mvnw test` instead of `./mvnw test`.

**Python** (once: make a virtual environment and install pytest into it)

```bash
python -m venv .venv
```

Then turn it on: `.venv\Scripts\Activate.ps1` in PowerShell on Windows, or `source .venv/bin/activate` on macOS (where the command is `python3`, not `python`). Then:

```bash
pip install -r requirements.txt
python demo.py
python -m pytest
```

**JavaScript**

```bash
node demo.js
npm test
```

**TypeScript** (once: `npm install`, which installs TypeScript)

```bash
node demo.ts
npm test
```

At first every test fails: that is expected. Each function you finish turns its tests green. A failing test's message starts with **Not yet:** and says what it expected.

## Tips

- **Write every friendship twice.** A friendship goes both ways, so it goes in both people's lists.
- **Friends of friends are two loops:** for each friend, look at each of *their* friends. Skip the person themselves, anyone who's already their friend, and anyone you've already suggested.
- **Steps between is breadth-first search** with a queue and a visited set, as in *Breadth-First Search: Ripples in a Pond*. Remember how many steps away each person is when you first find them.
- **Two tests use big networks:** a line of 2,000 people and a 30 by 30 grid. Breadth-first search handles both in a blink. If one of them seems stuck, or stops with a stack overflow or a recursion error, the code is trying every possible path, or going deep with recursion: switch to the queue.

## Hand it in

When it works, push your solution to a public repo of your own on GitHub, then paste that repo's address into **Submit your exercise** on the lesson page at [ask-codey.com](https://www.ask-codey.com). That's what completes the lesson. The lesson page shows the Git commands, step by step.
