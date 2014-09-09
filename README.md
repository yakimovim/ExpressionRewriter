Expressions Rewriter
==================

Simple rewriter of .NET expressions. It should change .NET lambda expressions in several ways:

1. Change types of arguments.
2. Change one sequence of property calls to another sequence. 

This library can be used in Web services which get one set objects of one type from database, but return objects of another type to users. In this case use can write expressions in terms of returned objects and this rewriter will translate them into expressions for database objects.
