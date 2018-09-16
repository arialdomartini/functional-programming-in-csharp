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
