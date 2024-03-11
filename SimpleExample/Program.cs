// See https://aka.ms/new-console-template for more information

using EchoOutLogging;

int someValue = 5;
int someV2 = 100;

bool test = "echo ".echo() + ("-- ".echo() + 1 * 10.echo() + someV2) == 110 ;
Console.WriteLine(test);

