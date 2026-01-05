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

---

### 3. ALL – Generating a Truth Table
The `ALL` command generates a complete truth table for a logical function.

For a function with `n` parameters:
- all `2^n` possible input combinations are generated manually
- each combination is evaluated using the recursive solver
- the corresponding result is printed in tabular form

No built-in utilities are used for generating combinations.

---

### 4. FIND – Finding a Logical Function
The `FIND` command attempts to discover a logical expression that matches a
given truth table.

The search may include:
- basic logical operators
- previously defined user functions

A brute-force approach is used by generating valid expression trees with
a limited number of nodes and comparing their truth tables to the input table.
The search terminates when a matching expression is found or a predefined
limit is reached.

---

### 5. Internal Representation
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

