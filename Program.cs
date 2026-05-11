/*Implement class methods and parameters
Completed
100 XP
14 minutes
A method is a code block that contains a series of statements. A program causes the statements to be executed by calling the method and specifying any required method arguments. In C#, every executed instruction is performed in the context of a method.

Method signatures
Methods are declared in a class, record, or struct by specifying:

An optional access level, such as public or private. The default is private.
Optional modifiers such as abstract or sealed.
The return value, or void if the method has none.
The method name.
Any method parameters. Method parameters are enclosed in parentheses and are separated by commas. Empty parentheses indicate that the method requires no parameters.
These parts work together to form the method signature.*/

namespace MotorCycleExample
{
    abstract class Motorcycle
    {
        // Anyone can call this.
        public void StartEngine() {/* Method statements here */ }

        // Only derived classes can call this.
        protected void AddGas(int gallons) { /* Method statements here */ }

        // Derived classes can override the base class implementation.
        public virtual int Drive(int miles, int speed) { /* Method statements here */ return 1; }

        // Derived classes can override the base class implementation.
        public virtual int Drive(TimeSpan time, int speed) { /* Method statements here */ return 0; }

        // Derived classes must implement this.
        public abstract double GetTopSpeed();
    }
}

/*Method invocation
Methods can be either instance or static. You must instantiate an object to invoke an instance method on that instance; an instance method operates on that instance and its data. You invoke a static method by referencing the name of the type to which the method belongs; static methods don't operate on instance data. Attempting to call a static method through an object instance generates a compiler error.*/

public static class SquareExample
{
    public static void Main()
    {
        // Call with an int variable.
        int num = 4;
        int productA = Square(num);

        // Call with an integer literal.
        int productB = Square(12);

        // Call with an expression that evaluates to int.
        int productC = Square(productA * 3);
    }

    static int Square(int i)
    {
        // Store input argument in a local variable.
        int input = i;
        return input * input;
    }
}

/*Value and reference parameters
Types in C# are either value types or reference types. By default, both value types and reference types are passed by value to a method.

Value type parameters
When a value type is passed to a method by value, a copy of the object instead of the object itself is passed to the method. Therefore, changes to the object in the called method have no effect on the original object when control returns to the caller.*/

public static class ByValueExample
{
    public static void Main()
    {
        var value = 20;
        Console.WriteLine("In Main, value = {0}", value);
        ModifyValue(value);
        Console.WriteLine("Back in Main, value = {0}", value);
    }

    static void ModifyValue(int i)
    {
        i = 30;
        Console.WriteLine("In ModifyValue, parameter value = {0}", i);
        return;
    }
}
// The example displays the following output:
//      In Main, value = 20
//      In ModifyValue, parameter value = 30
//      Back in Main, value = 20

/*The following example defines a class (which is a reference type) named SampleRefType. It instantiates a SampleRefType object, assigns 44 to its value field, and passes the object to the ModifyObject method. This example does essentially the same thing as the previous example (it passes an argument by value to a method). However, the result is different because a reference type is used rather than a value type. The modification that's made in ModifyObject to the obj.value field also changes the value field of the argument, rt. When the Main method displays the value of rt we see that it's been updated to 33, as the output from the example shows.*/

public class SampleRefType
{
    public int value;
}

public static class ByRefTypeExample
{
    public static void Main()
    {
        var rt = new SampleRefType { value = 44 };
        Console.WriteLine("In Main, rt.value = {0}", rt.value);
        ModifyObject(rt);
        Console.WriteLine("Back in Main, rt.value = {0}", rt.value);
    }

    static void ModifyObject(SampleRefType obj)
    {
        obj.value = 33;
        Console.WriteLine("In ModifyObject, obj.value = {0}", obj.value);
    }
}
// The example displays the following output:
//      In Main, rt.value = 44
//      In ModifyObject, obj.value = 33
//      Back in Main, rt.value = 33

/*Reference type parameters
You pass a parameter by reference when you want to change the value of an argument in a method and want to reflect that change when control returns to the calling method. To pass a parameter by reference, you use the ref or out keyword. You can also pass a value by reference to avoid copying but still prevent modifications using the in keyword.*/

public static class ByRefExample
{
    public static void Main()
    {
        var value = 20;
        Console.WriteLine("In Main, value = {0}", value);
        ModifyValue(ref value);
        Console.WriteLine("Back in Main, value = {0}", value);
    }

    private static void ModifyValue(ref int i)
    {
        i = 30;
        Console.WriteLine("In ModifyValue, parameter value = {0}", i);
        return;
    }
}
// The example displays the following output:
//      In Main, value = 20
//      In ModifyValue, parameter value = 30
//      Back in Main, value = 30

/*A common pattern that uses by ref parameters involves swapping the values of variables. You pass two variables to a method by reference, and the method swaps their contents. The following example swaps integer values.*/
public static class RefSwapExample
{
    static void Main()
    {
        int i = 2, j = 3;
        Console.WriteLine("i = {0}  j = {1}", i, j);

        Swap(ref i, ref j);

        Console.WriteLine("i = {0}  j = {1}", i, j);
    }

    static void Swap(ref int x, ref int y) =>
        (y, x) = (x, y);
}
// The example displays the following output:
//      i = 2  j = 3
//      i = 3  j = 2

/*Parameter collections
Sometimes, the requirement that you specify the exact number of arguments to your method is restrictive. By using the params keyword to indicate that a parameter is a parameter collection, you allow your method to be called with a variable number of arguments. The parameter tagged with the params keyword must be a collection type, and it must be the last parameter in the method's parameter list.

A caller can then invoke the method in either of four ways for the params parameter:

By passing a collection of the appropriate type that contains the desired number of elements. The example uses a collection expression so the compiler creates an appropriate collection type.
By passing a comma-separated list of individual arguments of the appropriate type to the method. The compiler creates the appropriate collection type.
By passing null.
By not providing an argument to the parameter collection.
The following example defines a method named GetVowels that returns all the vowels from a parameter collection. The Main method illustrates all four ways of invoking the method. Callers aren't required to supply any arguments for parameters that include the params modifier. In that case, the parameter is an empty collection.*/

static class ParamsExample
{
    static void Main()
    {
        string fromArray = GetVowels(["apple", "banana", "pear"]);
        Console.WriteLine($"Vowels from collection expression: '{fromArray}'");

        string fromMultipleArguments = GetVowels("apple", "banana", "pear");
        Console.WriteLine($"Vowels from multiple arguments: '{fromMultipleArguments}'");

        string fromNull = GetVowels(null);
        Console.WriteLine($"Vowels from null: '{fromNull}'");

        string fromNoValue = GetVowels();
        Console.WriteLine($"Vowels from no value: '{fromNoValue}'");
    }

    static string GetVowels(params IEnumerable<string>? input)
    {
        if (input == null || !input.Any())
        {
            return string.Empty;
        }

        char[] vowels = ['A', 'E', 'I', 'O', 'U'];
        return string.Concat(
            input.SelectMany(
                word => word.Where(letter => vowels.Contains(char.ToUpper(letter)))));
    }
}

// The example displays the following output:
//     Vowels from array: 'aeaaaea'
//     Vowels from multiple arguments: 'aeaaaea'
//     Vowels from null: ''
//     Vowels from no value: ''

/*Return values of methods
Methods can return a value to the caller. If the return type (the type listed before the method name) isn't void, the method can return the value by using the return keyword. A statement with the return keyword followed by a variable, constant, or expression that matches the return type returns that value to the method caller. Methods with a nonvoid return type are required to use the return keyword to return a value. The return keyword also stops the execution of the method.*/

class SimpleMath
{
    public int AddTwoNumbers(int number1, int number2) =>
        number1 + number2;

    public int SquareANumber(int number) =>
        number * number;
}

/*This example uses expression bodied members to assign the return value of the methods. This syntax is a shorthand for methods that use a single statement (expression) to calculate the return value.

You can also choose to define your methods with a statement body and a return statement:*/

class SimpleMathExtnsion
{
    public int DivideTwoNumbers(int number1, int number2)
    {
        return number1 / number2;
    }
}
/*To use a value returned from a method, the calling method can use the method call itself anywhere a value of the same type would be sufficient. You can also assign the return value to a variable. For example, the following three code examples accomplish the same goal:*/

int result = obj.AddTwoNumbers(1, 2);
result = obj.SquareANumber(result);
// The result is 9.
Console.WriteLine(result);

result = obj.SquareANumber(obj.AddTwoNumbers(1, 2));
// The result is 9.
Console.WriteLine(result);

result = obj2.DivideTwoNumbers(6,2);
// The result is 3.
Console.WriteLine(result);

/*Sometimes, you want your method to return more than a single value. You use tuple types and tuple literals to return multiple values. The tuple type defines the data types of the tuple's elements. Tuple literals provide the actual values of the returned tuple. In the following example, (string, string, string, int) defines the tuple type returned by the GetPersonalInfo method. The expression (per.FirstName, per.MiddleName, per.LastName, per.Age) is the tuple literal; the method returns the first, middle, and family name, along with the age, of a PersonInfo object.*/

public (string, string, string, int) GetPersonalInfo(string id)
{
    PersonInfo per = PersonInfo.RetrieveInfoById(id);
    return (per.FirstName, per.MiddleName, per.LastName, per.Age);
}
var person = GetPersonalInfo("111111111");
Console.WriteLine($"{person.Item1} {person.Item3}: age = {person.Item4}");

public (string FName, string MName, string LName, int Age) GetPersonalInfo(string id)
{
    PersonInfo per = PersonInfo.RetrieveInfoById(id);
    return (per.FirstName, per.MiddleName, per.LastName, per.Age);
}

var person = GetPersonalInfo("111111111");
Console.WriteLine($"{person.FName} {person.LName}: age = {person.Age}");

public Point Move(int dx, int dy) => new Point(x + dx, y + dy);
public void Print() => Console.WriteLine(First + " " + Last);
// Works with operators, properties, and indexers too.
public static Complex operator +(Complex a, Complex b) => a.Add(b);
public string Name => First + " " + Last;
public Customer this[long id] => store.LookupCustomer(id);
