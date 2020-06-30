# simple compiler
This is a simple compiler for a selection of mathematical expressions. The purpose of the project is to illustrate compiler functions.


### Presently the expressions of the following grammar are supported
command:             assignStatement | printStatement;  
assignStatement:     identifier '=' expression;  
printStatement:      PRINT expression;  
expression:          atomicValue ('+' expression)*;  
atomicValue:         identifier | intValue;

([C resx file](CalculatorCompiler/Properties/Resources.resx))
