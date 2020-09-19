# CSLOX
### A Lox interpreter in C#
Following this online book for jlox, but implementing in C# instead of Java
http://www.craftinginterpreters.com/scanning.html

Re-writing the Java examples in C# (dotnet core on a Mac specifically) will force me to 
1. Actually write them myself instead of copying them if they get long or complicated
1. Get more familiar with C#, which I've wanted for a little while

## Day 1
### Basic structure
Got some basic structure in place with a simple echo prompt.
```
christopherdean@ChristohersMBP3:~/Dev/lox/cslox$ dotnet run
> hello
hello
> hi
hi
> world
world
```

## Day 3
Got to section 4.5.2 tonight. Implemented basis for a scanner and could theoretically parse single character tokens.

## Day 4
Got to section 4.7. Implemented double character tokens and string and number literals

## Day 5
Learned about `maximal munch`.  Implented keywords and identifiers in scanner.  Had to do a mass replace for all the substring calls, looks like substring in c# is substring(start, length) not substring(start, end) like Java appers to be. 
Trying to get it to build, realized I had Scanner.cs and Token.cs outside of the cslox directory, so had to move those.  Was issing the `using System.Collections.Generic` for `List<>` and `Dictionary<>`. 
Working through tons of build errors now that I had it structured correctly.  Oops. 
indexing a string to chars in c# is as easy as indexing the string 
```
s = "hi";
char c = s[1]
````
also needed to add `public` to class definitions, constructors, and some methods to build correctly
