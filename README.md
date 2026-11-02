# Exercise: Friends of Friends

Exercise code for the lesson **Exercise: Friends of Friends** in the course *Data Structures and Big-O, Demystified* on [ask-codey.com](https://www.ask-codey.com).

## The ticket

> **From:** the product lead, a small social app
>
> Our app is growing past its first town. Two features are next on the list.
>
> 1. **People you may know.** On your home screen, show the friends of your friends who aren't your friends yet, each once, A to Z.
> 2. **How far apart are we?** On anyone's profile, show the fewest friendships between you and them: "1 step" for a friend, "2 steps" for a friend of a friend, and so on, or nothing when no chain of friends joins you.
>
> The app stores each friendship as a pair, the moment someone taps **Add friend**. Both features have to stay quick as we grow: the test town has 90,000 people, and "how far apart?" must answer from one corner of it to the other in under 2 seconds.

There are three functions to finish:

- `buildNetwork` (`BuildNetwork` in C#, `build_network` in Python): turns the app's list of friendship pairs into a network, a dictionary from each person to the list of their friends
- `suggestFriends` (`SuggestFriends`, `suggest_friends`): the people exactly two steps away, the friends of friends who aren't friends yet
- `stepsBetween` (`StepsBetween`, `steps_between`): the fewest friendships between two people, or -1 when nothing joins them

Ten tests check them, ending with the town of 90,000 people, in under 2 seconds.

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

The last test is the town of 90,000 people, a 300 by 300 grid of friendships, and it has to finish in under 2 seconds. If it runs for a long time, stop it with **Ctrl+C**: it's telling you to change your approach, not that your answer is wrong.

<details>
<summary>Stuck?</summary>

- **Where to look:** the ticket asks for what the chapter builds, a **graph** kept as an **adjacency list** (*Who Knows Whom? When Anything Can Connect: Graphs*), searched with **breadth-first search** (*How Many Steps Apart? Breadth-First Search*).
- **Write every friendship twice,** once in each person's list. A friendship goes both ways.
- **Friends of friends are two loops:** for each friend, look at each of *their* friends. Skip the person themselves, anyone who's already their friend, and anyone you've already suggested. Then sort.
- **"How far apart?" is breadth-first search** with a queue and a visited set. Remember how many steps away each person is when you first find them; a dictionary of steps is also the visited set. In Python the queue is `deque` from `collections`; in Java, `ArrayDeque`; in JavaScript and TypeScript, an array with a `head` index.
- **A stack overflow or a recursion error on the line of 2,000 people** means the search goes deep with recursion: switch to the queue.
- **A slow town of 90,000** usually means one of two things: the code tries every possible route, or "already seen" is a list, which is read from the top for every person. Use a set or a dictionary.

</details>

<details>
<summary>Why Should I Choose This?</summary>

Open this once your tests are green. The town is 90,000 people and 179,400 friendships, and the question is corner to corner, 598 steps.

- **The pairs as they are:** every "who are this person's friends?" reads all 179,400 pairs. A search asks that once for each of the 90,000 people: about 16 billion pair reads.
- **Every route, keep the shortest:** a 300 by 300 grid has more shortest routes from corner to corner than there are atoms in the Earth. It never finishes.
- **Rounds with a list for "already seen":** correct, but checking a list reads it from the top. With 90,000 people that took more than 4 minutes in Python (we stopped it there) and 68 seconds in C#.
- **A dictionary of friend lists and breadth-first search:** each person joins the queue once and each friendship is looked at from both ends, O(V + E). It took under a fifth of a second in Python, and about a twentieth in C#.
- **What it gives up:** breadth-first search counts friendships, not how close they are. For "the fastest drive", where every road takes a different time, the next chapter needs something more.

</details>

## Hand it in

When it works, push your solution to a public repo of your own on GitHub, then paste that repo's address into **Submit your exercise** on the lesson page at [ask-codey.com](https://www.ask-codey.com). That's what completes the lesson. The lesson page shows the Git commands, step by step.
