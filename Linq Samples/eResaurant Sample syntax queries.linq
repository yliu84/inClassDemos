<Query Kind="Statements">
  <Connection>
    <ID>6ecbcb33-3e1a-4c90-9503-00d17cf707f1</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//simpliest form for dumping an entity
Waiters

//simple query syntax
from person in Waiters
select person

//simple method syntax
Waiters.Select(person => person)

//inside our project we will be writting C# statement
var results = from person in Waiters
select person;

//to Display the content of a variable in LinqPad
//use the .Dump() method
results.Dump();

//implemented inside a VS project's class library BLL method
//[DataObjectMethod(DataObjectMethodType.Select,false)]
//public List<Waiters> SomeMethodName ()
//{
	//You will need to connect to your DAL object 
	//this will be done using a new xxxxxx() constructor
	//assume your connection variable is called context variable
	
	//do your query 
	//var results = from person in contextvariable.Waiters
	//select person;
	
	//return your results
	//return results.ToList();
//}

