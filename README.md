# simple compiler
This is a simple compiler for a selection of mathematical expressions. The purpose of the project is to illustrate compiler functions.


### Presently the expressions of the following grammar are supported
command:              (assignStatement | printStatement) ';'
assignStatement:      identifier '=' expression;
printStatement:       PRINT sumExpression;

sumExpression:        productExpression ('+' productExpression)*;
productExpression:    unaryExpression ('*' unaryExpression)*;
unaryExpression:      '-'? atomicExpression;

atomicExpression:     identifier;
atomicExpression:     intValue;
atomicExpression:     '(' sumExpression ')';

([C resx file](CalculatorCompiler/Properties/Resources.resx))
