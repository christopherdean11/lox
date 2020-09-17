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