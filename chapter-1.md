# Chapter 1 - Introduction

* Functional vs other (he mainly refers to "imperative" programming)
* "after reading this book, you’ll never look at code with the same eyes as before!"
* "The learning process can be a bumpy ride. You’re likely to go from frustration at concepts that seem obscure or useless to exhilaration when something clicks in your mind, and you’re able to replace a mess of imperative code with just a couple of lines of elegant, functional code."

## Functional Programming
* "a programming style that emphasizes functions while avoiding state mutation"
* Example of function as first-class citizen:

```
Func<int, int> triple = x => x * 3;
var range = Enumerable.Range(1, 3);
var triples = range.Select(triple);

triples // => [3, 6, 9]
```

* Avoiding state mutation
  * "we should refrain from state mutation altogether: once created, an object never changes, and variables should never be reassigned.**
  * Destructive updates
  * Example: sorting 2 lists in parallel, once with immutable structures, then with in-place modification [code](FunctionalProgramming/ImmutabilityAndConcurrency/)

