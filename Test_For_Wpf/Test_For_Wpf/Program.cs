Random Random_Pass = new Random();
Random Random_User = new Random();
string Generated_Password = "";


for (int i = 0; i < 50; i++)
{
    Generated_Password = ((Random_Pass.Next(0, 99999999)).ToString()).PadLeft(8, '0');
    Console.WriteLine(Generated_Password);
    
}


