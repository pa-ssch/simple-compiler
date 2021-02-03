# simple compiler
This is a simple compiler for a selection of mathematical expressions. The purpose of the project is to illustrate compiler functions.


### Presently the expressions of the following grammar are supported
```
command:              (assignStatement | printStatement) ';'
assignStatement:      identifier '=' expression;
printStatement:       PRINT sumExpression;

sumExpression:        productExpression ('+' productExpression)*
productExpression:    ternaryExpression ('*' ternaryExpression)*
ternaryExpression:    unaryExpression ('?' unaryExpression ':' unaryExpression)?
unaryExpression:      '-'? atomicExpression

atomicExpression:     identifier;
atomicExpression:     naturalNumber;
atomicExpression:     '(' sumExpression ')';
```

([C resx file](Compiler/Properties/Resources.resx))

### Example
![Screenshot not available](simple-compiler-screenshot-7.png)
