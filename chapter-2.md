# Chapter 2 - Function Purity
## What's Function Purity
* Mathematical functions vs Side effects
  * Mathematical functions exist in a vacuum: their result are determined strictly by their input
  * Programming costructs we use for representing functions access to a context.
    * This leads to the difference between pure and impure functions
* A function with Side Effects may either
  * Mutate global state
  * Mutate to input arguments
  * Throw Exceptions
  * Perform IO Operations
  
### Pure Functions
* Pure functions
  * They are deterministic (they always return the same output given the same input)
    * Consequently they are
      * Easy to test
      * Easy to reason about
      * Execution order does not matter
        * They are parallelizable
        * Lazy evaluation can be leveraged
        * They are referencially transparent, and they can be memoized (cache the result, so the computation is performed only once)
* Impurity is unavoidable
  * FP promotes isolating the code that performs side effects
    * Pure core, impure shell (**discuss**)
      * The computational part of a program can be made entirely of pure functions
      
#### Strategies for managing side effects
* Isolate I/O effecs
* Avoid mutating arguments
  * 'The reason why this is such a terrible idea is that the behavior of the method is now tightly coupled with that of the caller: the caller relies on the method to perform its side effect, and the callee relies on the caller to initialize the list. As such, both methods must be aware of the implementation details of the other, making it impossible to reason about the methods in isolation.**
  * It is always possible to structure a method's code  so that it does not mutate its arguments
  
## Purity and concurrency
* '*Why do you have to explicitly instruct the runtime to parallelize the operation? Why can’t it just figure out that it’s a good idea to parallelize the operation, just like it figures out when it’s a good time to run the garbage collector?*' (**discuss**) 
* Surprisingly, the functional version is slowr (**discuss**)

### Pure functions parallelize well
* In fact, the functional version of the preceeding exercise get faster than the non-functional one once parallelized.
  * To "parallelize well" means that pure functions are immune to the issues that make concurrency hard.
  * If we try to parallelize the non-functional version, it yields random, unexpected results
  * Very interesting the use of `Select` vs `Any`, and the optimization made by the compiler.
  
### Types of concurrency

* Asynchrony: non-blocking operations
* Parallelism: hardware, like singing in the shower
* Multi-threading: software, like chatting with multiple people

In all cases, the order is not granted.

### How to make things parallelizable

* Avoid state mutation: never user shared state; this removes the problem at the source.

### The case for static methods

Static methods can cause pain:

* if they act on mutable, shared, static fields
* if they perform I/O: in this case, testability is jeopardized

It becomes harder to compose via dependency inject => with Functional Programming a different kind of composition must be used. (**discuss**)

## Purity and testability

* "whereas mutation is an implementation detail, I/O is usually a requirement"

### A validation scenario

* "The standard object-oriented (OO) technique for ensuring that unit tests behave consistently is to abstract I/O operations in an interface, and to use a deterministic implementation in the tests. I’ll call this the *interface-based approach*; it’s considered a best practice, but I’ve come to think of it as an anti-pattern because of the amount of boilerplate it entails."
  * Primary Constructors will ease the injection of dependencies (**discuss**)
  
```csharp
class Point(int x, int y) { … }
```

will be equivalent to

```csharp
class Point
{
q
    private int x;
    private int y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
```

* Has a class with dependencies injected became pure? It depends! (**discuss**)

## Why testing impure functions is hard

* taking control on the state of the external world for the Arrange phase is not easy
* `void` returning functions have no explicit output to assert against
* the execution results in a new state on non local variables.

"A impure function could be seen as a pure function that takes as input its arguments along with the current state of the program and the world, and returns its own output plus a new state of the program and the world.

In other words, an impure function has implicit inputs and outputs" (*discuss*)

### Parametrized tests
* They tend to be more functional, because they make you think in terms of input and outputs (*discuss*) (show Spock data driven tests)

### Avoiding header interfaces

1. Define an interface that abstracts the impure operations consumed in the system under test
2. Put the impure implementation aside
3. Inject the dependency in terms of interface
4. Introduce some bootstrapping logic to inject the correct implementation whenever the class is used.
5. Create and inject fake implementations for the purpose of unit testing

* Header interfaces are interfaces defined for one single implementations

#### Pushing the pure boundaries outwards

* Instead of injecting an interface, inject a value.
* We have pushed the impure code outside, to the caller

```csharp
public class DateNotPastValidator : IDateValidator
{
    private DateTimeOffset _today;

    public DateTimeOffset(DateTimeOffset today)
    {
        _today = today;_
    }

    public bool IsValid(DateTimeOffset date) =>
        today <= date;
}
```

#### Injecting functions as dependencies

* Why? Because functions, if pure, are referencially transparent, so they are just values.
* "A function signature is an interface" (**discuss**)
