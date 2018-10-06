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
* "More generally, every time you add a field to an object (or a tuple),you’re creating a Cartesian product and adding a dimension to the space of the possible values of the object"
* "The main takeaway is that you should model objects in a way that gives you fine control over the range of inputs that your functions will need to handle" (**discuss**)

## Modeling the absence of data: Unit

* Unit is the empty tuple.
* Why isn't void ideal?
  * Using `Func<Void>` to represent a function that returns nothing is not possible.
    * 
    
> The Void structure is used in the System.Reflection namespace, but is rarely useful in a typical application. The Void structure has no members other than the ones all types inherit from the Object class.
>
>There's no reason really to use it in code.
>
>Also:
>
>var nothing = new void();
>
>This doesn't compile for me. What do you mean when saying it "works"?
>
>A method void Foo() does not return anything. System.Void is there so that if you ask (through Reflection) "what is the type of the return value that method?", you can get the answer typeof(System.Void). There is no technical reason it could not return null instead, but that would introduce a special case in the Reflection API, and special cases are to be avoided if possible.
>
>Finally, it is not legal for a program to contain the expression typeof(System.Void). However, that is a compiler-enforced restriction, not a CLR one. Indeed, if you try the allowed typeof(void) and look at its value in the debugger, you will see it is the same value it would be if typeof(System.Void) were legal."

https://stackoverflow.com/questions/5450748/what-is-system-void#5450765


* Need to have both `Func<>` and `Action<>`;

* Unit
  * Unit is not void: it is an empty element that exists with 1 possible value. The set Unit contains **one** value, not no values! (**discuss**)
  * "The empty tuple has no members, so it can only have one possible value"
  * Avoid implementing a custom `Unit` and end up with incompatible libraries. Better to use `using Unit = System.ValueTuple;`
  * `Unit` is useful to have gain consistency in dealing with Funcs and Actions.

## Modeling the possible absence of data with `Option`

* `NameValueCollection` and `Dictionary<string, T>` are inconsistent in the way they deal with absence of value: `NameValueCollection` returns `null` (which is not even the same type!), while `Dictionary` throws an excetion


```haskell
data Maybe a = Empty | Some a
```

```csharp
Option<T> = None | Some(T)
```

`Option<T>` can be in one of two states;
