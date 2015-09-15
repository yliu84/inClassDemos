<Query Kind="Program" />

void Main()
{
	//simple concatenation
	//"hello word"
	//5+7
	
	//Simple C# statement
	//string name = "Don";
	//string message = "Hello " + name;
	//message.Dump();
	
	//simple C# program
	//subroutine call
	SayHello("Don");
	
}

// Define other methods and classes here
public void SayHello(string name)
{
	string message = "hello " + name;
	message.Dump();
}