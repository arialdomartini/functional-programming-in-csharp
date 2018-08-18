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
  * 'The reason why this is such a terrible idea is that the behavior of the method is now tightly coupled with that of the caller: the caller relies on the method to perform its side effect, and the callee relies on the caller to initialize the list. As such, both methods must be aware of the implementation details of the other, making it impossible to reason about the methods in isolation.'
  * It is always possible to structure a method's code  so that it does not mutate its arguments
  
## Purity and concurrency

  
