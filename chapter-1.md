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
          * structs (for example, `KeyValuePair`) and strings are immutable
        * No algebraic data types
        * Very limited Pattern Matching
      * Benefits
        * LINQ is a functional
          * 'Notice how Where, OrderBy, and Select all take functions as arguments and don’t mutate the given IEnumerable, but return a new IEnumerable instead, illustrating both tenets of FP you saw earlier.'
          * 'On the other hand, when working with other types, C# programmers generally stick to the imperative style of using flow-control statements to express the program’s intended behavior'
          
          
      

## Functional features of C#

* 
* Most of the functional features (see [code sample](FunctionalProgramming/FunctionalFeatures/Circle.cs)) has been introduced in C# 3. C# 6 and 7 introduced better syntax, but generally not new features
* Author's preference:
  * He always tries to use expression bodies **(discuss)**
* Features
  * Importing static members with `using static`
    * If possible, functions are static
    * 'In FP, we prefer functions whose behavior relies only on their input arguments because we can reason about and test these functions in isolation (contrast this with instance methods, whose implementation typically interacts with instance variables). These functions are implemented as static methods in C#, so a functional library in C# will consist mainly of static methods.'
  * Easier immutable types with getter only properties
    * 'When you declare a getter-only auto-property, such as Radius, the compiler implicitly declares a readonly backing field. As a result, these properties can only be assigned a value in the constructor or inline'
  * More concise functions with expression-bodied syntax
    * Introduced with C# 6
    * 'In FP, we tend to write lots of simple functions, many of them one-liners, and then compose them into more complex workflows. Expression-bodied methods allow you to do this with minimal syntactic noise. This is particularly evident when you want to write a function that returns a function.'
    * C# 7 allows to make this explicit by declaring methods within the scope of a method
  * Better syntax for tuples
    * The most important feature of C# 7
      * 'You may end up with a data type whose only purpose is to capture the information returned by one function, and that’s expected as input by another function' without the need to define a new type with `class`
      
    

## Thinking in functions
* Mathematical definition: Domain and Codomain
* The value that a function yields is determined *exclusively* by its input
* In programming, Domains and Codomains are defined by types

### Ways for representing functions in C#
* Methods
* Delegates
  * Local functions
* Lambdas
* Dictionaries

#### Delegates
* Delegates are type-safe function pointers (uhm... No, they are types declaration, or the like)
* `Func` and `Action` delegates
  * Since the introduction of `Func` it has become rare to define custom delegates
  * `Action` is not funcional! **(discuss)**
  * `Predicate<T>` were introduced in .NET 3 for `ListAll`, which not just takes a `Func<T, bool>`
    * The trend is to define things in-line (**discuss**)
    
#### Lambdas

* A way to define functions inline
* when there's no need to re-use the function
* The compiler makes use of type-inference for lambda parameters (**discuss**)

##### Closures
* 'Just like methods, delegates and lambdas have access to the variables in the scope in which they’re declared.'
* 'A closure is the combination of the lambda expression itself along with the context in which that lambda is declared'
* In a closure the arity is somehow implicit

```csharp
var days = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
// => [Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday]

IEnumerable<DayOfWeek> DayStartingWith(string pattern) 
  => days.Where(d => d.ToString().StartsWith(pattern));
```
What's the arity of `DayStartingWith`? It seems unary, but actually depends on `days`, so it might be considered binary (**discuss**)

#### Dictionaries
* Very direct representation of functions
* Used in Memoization
* C# 6 introduced a new initializer syntax (see [code sample](FunctionalProgramming/FunctionalFeatures/DictionarySyntax.cs)))


## High-order functions
* 'Some HOFs take other functions as arguments and invoke them in order to do their work, somewhat like a company may subcontract some of its work to another company.'
  * Is it a form of Dependency Injection? (**discuss**)
    * 'List.Sort, when called with a Comparison delegate, is a method that says: “OK, I’ll sort myself, as long as you tell me how to compare any two elements that I contain.” Sort does the job of sorting, but the caller can decide what logic to use for comparing. Similarly, Where does the job of filtering, and the caller decides what logic determines whether an element should be included.'
    
