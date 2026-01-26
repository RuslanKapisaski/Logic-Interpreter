# Logical Expression Interpreter
---
## Overview
This project implements a console-based interpreter for logical expressions.
The interpreter allows defining, evaluating, and analyzing logical functions
built from basic logical operators and user-defined functions.

The solution is implemented in C# (.NET) and follows the requirement that
all parsing logic and data structures are implemented manually, without
using built-in string parsing utilities or collections.

---

## Supported Logical Operations
- AND (`&`)
- OR (`|`)
- NOT (`!`)

---

## Functionalities

### 1. DEFINE – Defining Logical Functions
The `DEFINE` command allows the user to define logical functions with a given name
and ordered list of parameters.

Logical expressions can be composed of:
- variables (function parameters)
- basic logical operators (`AND`, `OR`, `NOT`)
- previously defined user functions

During definition, the program:
- parses the input character by character
- validates the correctness of the expression
- checks for undefined variables
- validates correct number of function arguments
- builds a binary expression tree representing the logical expression

Each defined function is stored in a custom hash table, where the key is the
function name and the value is the root of the expression tree.

**Example**
```text
DEFINE Func1(a,b): ab&
Result: Function defined: Func1 -> ab&

DEFINE Func2(a,b,c): Func1(a,b) | c 
Result: Function defined: Func2 -> Func1(a,b)c|

```

`Note: Names of the function should start with capittal letter`

---

### 2. SOLVE – Evaluating a Function
The `SOLVE` command evaluates a previously defined logical function for given
argument values.

Execution steps:
- the corresponding function is retrieved from the hash table
- argument values are assigned to variable nodes in the expression tree
- the expression tree is evaluated recursively from the leaves to the root
- the final result is obtained at the root node

To optimize repeated evaluations, the program recalculates only the parts of
the expression tree whose values have changed.

**Example**
```text
SOLVE Func1(1,0)
```

`Note: Function should be defined in advance`

---

### 3. ALL – Generating a Truth Table
The `ALL` command generates a complete truth table for a logical function.

For a function with `n` parameters:
- all `2^n` possible input combinations are generated manually
- each combination is evaluated using the recursive solver
- the corresponding result is printed in tabular form

No built-in utilities are used for generating combinations.

**Example**
```text
All Func1

Output:
a , b : Func1
0 , 0 : 0
0 , 1 : 0
1 , 0 : 0
1 , 1 : 1
```

`Note: Function should be defined in advance`

---

### 4. FIND – Finding a Logical Function
The `FIND` command attempts to discover a logical expression that matches a
given truth table.

The search may include:
- basic logical operators
- previously defined user functions

**Example**
```text
 int[,] truthTable =
        {
            {0,0,0,0},
            {0,0,1,0},
            {0,1,0,0},
            {0,1,1,0},
            {1,0,0,0},
            {1,0,1,0},
            {1,1,0,0},
            {1,1,1,1}
        };
FIND 
Result: ((a & b) & c)
```

---
### 5. SAVE – Saving Defined Functions
The SAVE command allows all currently defined logical functions to be saved to a file.

The program stores the original DEFINE commands in a text file so that all functions
can later be restored exactly as they were defined.

Execution steps:
all DEFINE commands are written line by line to the specified file
each line represents one function definition

**Example**
```text
SAVE fileName.txt 
```

---
### 6. LOAD – Loading Defined Functions

The LOAD command allows previously saved function definitions to be loaded from a file.

Execution steps:
- the file is opened and read line by line
- each line is treated as a DEFINE command
- each function is parsed and added to the function table

**Example**
```text
LOAD fileName.txt
```

`Note:` If file does not exist an exception message is displayed.


---

### Internal Representation
All logical expressions are represented as binary trees:
- leaf nodes represent variables
- internal nodes represent logical operators

Evaluation is performed recursively, and intermediate results are cached
to avoid unnecessary recalculations.

---

## Technical Constraints
- All text parsing is implemented character by character
- No built-in string parsing functions (`Split`, `Regex`, `IndexOf`, LINQ, etc.)
- All data structures (stack, hash table, tree) are implemented manually
- The application uses a console-based interface

---

## Build and Run

```bash
dotnet build
dotnet run

