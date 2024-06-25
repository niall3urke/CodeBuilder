# CodeBuilder

CodeBuilder is designed to "codify" engineering design codes by producing a database of engineering calculations that can be used to create checks and briefs.

## Overview

CodeBuilder breaks up design codes into three main components:

1. **Variables**: These can be as simple as numbers or composed of other variables and determined through a calculation.
2. **Checks**: These are composed of several variables with nesting, conditional blocks, and an outcome.
3. **Briefs**: These are composed of several checks and can represent a design code requirement in its entirety.

CodeBuilder can also handle more complex ideas around the grouping of data through:

- **Tabular Data**: Set rows and columns of variables against one another and create tabular data.
- **Variable Groups**: Collections of related variables that might represent a model class in code.

## Trying it out

1. Download the repository to your PC.
2. Open the project in Visual Studio and run the application.
3. An in-memory database contains a few simple pre-populated variables, checks, and briefs to help you get started.

## Why CodeBuilder

CodeBuilder is designed to offload the process of writing repetitive code for engineering calculations. Once you reach the point of having a mature database of variables, much of the work for creating new checks or briefs should already be done.

In addition, every variable, check, and brief can be tied back to a particular standard. So, when a standard is superseded or archived, you know exactly which records need to be replaced or updated.

CodeBuilder aims to speed up the process of creating engineering calculations by allowing you to input MathJax, which it will parse and generate variable records for automatically. Once you've created your variable, you can preview the C# code that will be generated.

The ultimate aim for this project is to get to the point where variables, checks, and briefs can be compiled, generated, and saved as working code files. This is largely possible at the moment through copying the preview code. It would also be possible to generate a basic front end for the generated code, but that's even further down the line.

## Usage

### Create Variables

1. Navigate to the `/variables` page and click the "+" button.
2. Give your variable a name.
3. Enter a value for the variable's Symbol; this can be MathJax, and a preview will be shown.
4. Specify the variable's type.
5. If you have any Standards that apply to the variable, you can start typing the name and select the appropriate standard from the dropdown list of results.
6. If the variable is calculated (i.e., it's a function), toggle the Calculated switch.
7. Enter the formula for the calculation in MathJax format. A MathJax preview will be shown, and the components of your calculation will be parsed for further variables. If the calculation's variables don't already exist in the database, they will be created on saving. Note that if you edit any existing database variables in the variables table, a new variable will be created. Existing database variables cannot be edited from the variables table.
8. You can optionally validate your calculation. This is determined from the default "value" properties of the calculation variables and the default result property.
9. When you're happy with everything, click save, and you'll be directed back to the `/variables` page.

### Create Checks

1. Navigate to the `/checks` page and click the "+" button.
2. Give your check a name.
3. Add the variables needed for the check.
4. Define the logic for the check using conditional blocks and outcomes.
5. Link the check to the appropriate standards.
6. Save the check and review it on the `/checks` page.
