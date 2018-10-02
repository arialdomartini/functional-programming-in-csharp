# Chapter 3 - Designing function signatures and types

## Arrow notation

```haskell
f : int -> string
```

corresponding to:

```csharp
Func<int, string>
```

in strict Haskell would be

```haskell
f :: Int -> String
```

Also

```haskell
(int, int) -> int
```

actually should be

```haskell
int -> int -> int
```

Similarly:

```csharp
f : IEnumerable<A>, IEnumerable<B>, ((A, B) -> C), -> IEnumerable<C>
```

in Haskell would be

```haskell
f :: [a] -> [b] -> (a -> b -> c) -> [c]
```

## Capturing data with Data Objects

* Data Objects, or *anemic objects*: object that contains data but no logic.
  * There's no negative connotation is *anemic objects*: in FP it is natural to draw a separation between logic an data.
  
  
### Primitive Obsession

The overall goal is to make invalid states unrepresentable (**discuss**)

* Example with function expecting an age.
  * `dynamic` is too less specific
  * but also `int` is too less specific, as it allows negative ages to be input
    * The first istinct is to add some validation to the function.
      * Issue: code duplication everywhere age is used.
      * "Duplication is often a sign that Separation Of Concerns has been violated."A function that should only concern itself with calculation also concerns ifself with validation (**discuss**)
  * A more solid approach is to wrap `int` in another class (Primitive Obsession)
  * Operators `<` and `>` can be defined to make `Age` more similar to an ordinary integer.
  
  * Honest and dishonest functions
    * Honest Functions honor their signature, always.
    * their behaviour can be predicted by their signature.
    
  
