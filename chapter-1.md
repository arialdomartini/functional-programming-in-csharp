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
  * Example: sorting 2 lists in parallel, once with immutable structures, then with in-place modification 
    * [code](FunctionalProgramming/ImmutabilityAndConcurrency/)
* Comparison of Functional and Object Oriented
  * Object Oriented principles
    * Encapsulation
    * Data Abstraction
  * But often OO relies on imperative style, with state mutation, with explicit control flow
    * "they use OO design in the large, and imperative programming in the small"
  * General programming goals (valid both for OO and FP):
    * Structuring large, complex applications
      * Modularity: dividing software into reusable components
      * Separation of concerns
      * Layering: building high level components on top of low level components, which in turn have no dependency on the high level ones
      * Loose coupling: changes on a component should not affect components that depend on it
    * Out of the tar tip
      * "The classical ways to approach the difficulty of state include object oriented programming which tightly couples state together with related be haviour, and functional programming which — in its pure form — eschews state and side-effects all together."
      * "These approaches each suffer from various (and differing) problems when applied to traditional large-scale systems."
    * C# 
      * shortcomings:
        * everything is mutable by default: it doesn't discourage in-place updates
        * language support for immutable data type is poor
      * Benefits
        * LINQ is a functional
          * 'Notice how Where, OrderBy, and Select all take functions as arguments and don’t mutate the given IEnumerable, but return a new IEnumerable instead, illustrating both tenets of FP you saw earlier.'
          * 'On the other hand, when working with other types, C# programmers generally stick to the imperative style of using flow-control statements to express the program’s intended behavior'
          
          
      

## Functional features of C#

* 
* Most of the functional features (see [code sample](FunctionalProgramming/FunctionalFeatures/Circle.cs)) has been introduced in C# 3. C# 6 and 7 introduced better syntax, but generally not new features
* Author's preference:
  * He always tries to use expression bodies
  * If possible, functions are static
    * 'In FP, we prefer functions whose behavior relies only on their input arguments because we can reason about and test these functions in isolation (contrast this with instance methods, whose implementation typically interacts with instance variables). These functions are implemented as static methods in C#, so a functional library in C# will consist mainly of static methods.'
    

