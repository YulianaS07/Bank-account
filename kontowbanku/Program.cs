using Bank;

//Test1();
//Test2();
//Test3();
//Test4();
//Test5();
//Test11();
Test12();



void Test1()
{
    /* wypłaty
*/
    var account = new Account("John");
    account.Deposit(100.00m);
    Console.WriteLine(account.Withdrawal(10.00m));
    Console.WriteLine(account);
    Console.WriteLine(account.Withdrawal(100.00m));
    Console.WriteLine(account);
    Console.WriteLine(account.Withdrawal(0.00m));
    Console.WriteLine(account);
    Console.WriteLine(account.Withdrawal(-10.00m));
    Console.WriteLine(account);
    account.Block();
    Console.WriteLine(account.Withdrawal(10.4999m));
    Console.WriteLine(account);
}

void Test2()
{
    /* Utworzenie konta dla dwóch argumentów, ujemne saldo początkowe
*/
    try
    {
        var account = new Account("Jim", -100.01m);
        Console.WriteLine(account);
    }
    catch (ArgumentOutOfRangeException)
    {
        Console.WriteLine("negative argument");
    }
}

void Test3()
{
    /* Utworzenie konta dla dwóch argumentów, nazwa jest null
*/
    try
    {
        var account = new Account(null, 100.0m);
        Console.WriteLine(account);
    }
    catch (ArgumentOutOfRangeException)
    {
        Console.WriteLine("Name is null");
    }
}

void Test4()
{
    /* wpłata, kwota ujemna, konto zablokowane,
saldo nie zmienione */
    var account = new Account("John");
    account.Block();
    Console.WriteLine(account.Deposit(-100.2345m));
}

void Test5()
{
    /* wypłaty
*/
    var account = new Account("John");
    account.Deposit(100.00m);
    Console.WriteLine(account.Withdrawal(10.00m));
    Console.WriteLine(account);
    Console.WriteLine(account.Withdrawal(100.00m));
    Console.WriteLine(account);
    Console.WriteLine(account.Withdrawal(0.00m));
    Console.WriteLine(account);
    Console.WriteLine(account.Withdrawal(-10.00m));
    Console.WriteLine(account);
    account.Block();
    Console.WriteLine(account.Withdrawal(10.4999m));
    Console.WriteLine(account);
}

void Test11()
{
    // tworzenie konta, zmiana limitu
    // utworzenie konta plus z domyslnym limitem 100
    var john = new AccountPlus("John");
    Console.WriteLine(john);

    // utworzenie konta plus z ustawionym limitem na 200 i bilansem początkowym 99
    var eve = new AccountPlus("Eve", initialLimit: 200.0m, initialBalance: 99.0m);
    Console.WriteLine(eve);

    // zmiana limitu, konto nie zablokowane
    eve.OneTimeDebetLimit = 120.0m;
    Console.WriteLine(eve);

    // próba zmiany limitu, konto zablokowane
    eve.Block();
    eve.OneTimeDebetLimit = 500.0m;
    Console.WriteLine(eve);

    // próba utworzenia konta z limitem ujemnym
    var stan = new AccountPlus(name: "Stan", initialLimit: -200.0m);
    Console.WriteLine(stan);


}

void Test12()
{
    // scenariusz: wpłaty wypłaty, blokada konta
    // utworzenie konta plus z domyslnym limitem 100
    var john = new AccountPlus("John", initialBalance: 100.0m);
    Console.WriteLine(john);

    // wypłata - podanie kwoty ujemnej
    john.Withdrawal(-50.0m);
    Console.WriteLine(john);

    // wypłata bez wykorzystania debetu
    john.Withdrawal(50.0m);
    Console.WriteLine(john);

    // wypłata z wykorzystaniem debetu
    john.Withdrawal(100.0m);
    Console.WriteLine(john);

    // konto zablokowane, wypłata niemożliwa
    john.Withdrawal(10.0m);
    Console.WriteLine(john);

    // wpłata odblokowująca konto
    john.Deposit(80.0m);
    Console.WriteLine(john);

    // wpłata podanie kwoty ujemnej
    john.Deposit(-80.0m);
    Console.WriteLine(john);
}





