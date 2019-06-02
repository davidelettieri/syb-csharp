# syb-csharp

I'm trying to implement in C# the "Scrap your boilerplate" [SYB] approach from Haskell 
- https://www.microsoft.com/en-us/research/wp-content/uploads/2003/01/hmap.pdf
- http://hackage.haskell.org/package/syb

**I'm not sure this is possible to achieve** and there is a rather long [article doing the same thing for C++](http://citeseerx.ist.psu.edu/viewdoc/download?doi=10.1.1.472.5297&rep=rep1&type=pdf) so it might be to hard to do.

The SYB approach is meant to avoid to write repetitive code when dealing with complex data structures. The classical Paradise example as found in the cited article is roughly this scenario:
There is a company with departments, subunits, manager and peoples and we want to rise the salary of each person.

Most of the code for solving this problem is about traversing the data structure representing the company while the actual "solving the problem" code is just a multiplication. They propose to split the problem in three parts:
- Writing the actual code to solve the problem, this is manual and specific to the task at hand.
- Generate automatically some functions to traverse the data structure. This is the GMapT function that I wrote by hand but that could possibly be automated using roslyn.
- Write only once a library to support everything.

I have a working example with some shortcomings:
a. I wrote all the code by hand, including the part that should be automatically generated.
b. I wrote just one combinator

Most of the work is done in two generic classes MkT and EveryWhere, this approach follows the C++ article if I understood it correctly:
- MkT takes a Func<T,T> and allows to compute the function to any U even if U != T, in this case it just returns the argument value
- EveryWhere is the class that contains the recursion logic

Next steps:
1. Write more combinators
2. Write a roslyn refactor to automatically generate the GMap method
3. Try to find a way to remove the runtime cast inside the Apply method of MkT class

About 2, I wrote a simple refactor that allows to generate correctly the GMapT method in some cases but it's missing checks and IEnumerable parameters
