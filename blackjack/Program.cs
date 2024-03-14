using System.Text;
int IntTime()
{
    DateTime currentTime = DateTime.Now;
    int timeAsInt = currentTime.Hour * 10000 + currentTime.Minute * 100 + currentTime.Second;
    return timeAsInt;
}

Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("Enter your name");
string name = Console.ReadLine();
Console.WriteLine("Enter your balance");
int balance = int.Parse(Console.ReadLine());
Player PlayerOne = new Player(name, balance);
Player Bot = new Player("BOT", 300000);
Random rand = new Random(IntTime());
Random random = new Random(IntTime() + 123);
while (true)
{
    Console.WriteLine("Select option\n[1]-Show player info\n[2]-Play BlackJack\n[3]-Exit");
    int action = int.Parse(Console.ReadLine());
    switch (action)
    {
        case 1:
            PlayerOne.PlayerInfo();
            break;
        case 2:
            bool game = true;
            PlayerOne.MakeBet(Bot);
            PlayerOne.ClearCards();
            Bot.ClearCards();
            while (PlayerOne.CheckSum() && Bot.CheckSum() && game)
            {
                Console.WriteLine("\nYour cards:");
                PlayerOne.ShowCards();
                Console.WriteLine($"\nsum of cards:{PlayerOne.CardsSum()}");
                Console.WriteLine("\nBot cards:");
                Bot.ShowCards();
                Console.WriteLine($"\nsum of cards:{Bot.CardsSum()}");
                Console.WriteLine("\nSelect option\n[1]-Take card\n[2]-End game\n");
                int temp = int.Parse(Console.ReadLine());

                switch (temp)
                {
                    case 1:
                        PlayerOne.TakeCard(random, Bot);
                        while (PlayerOne.CardsSum() > Bot.CardsSum() && Bot.CheckSum() && Bot.CardsSum() != 21 && PlayerOne.CheckSum())
                        {

                            Bot.TakeCard(rand, PlayerOne);
                        }
                        if (!Bot.CheckSum() && PlayerOne.CardsSum() <= 21)
                        {
                            PlayerOne.PlayerWin(Bot);
                            Console.WriteLine("\nYour cards:");
                            PlayerOne.ShowCards();
                            Console.WriteLine($"\nsum of cards:{PlayerOne.CardsSum()}");
                            Console.WriteLine("\nBot cards:");
                            Bot.ShowCards();
                            Console.WriteLine($"\nsum of cards:{Bot.CardsSum()}");
                        }
                        else if (!PlayerOne.CheckSum() && Bot.CardsSum() <= 21)
                        {
                            PlayerOne.PlayerLose(Bot);
                            Console.WriteLine("\nYour cards:");
                            PlayerOne.ShowCards();
                            Console.WriteLine($"\nsum of cards:{PlayerOne.CardsSum()}");
                            Console.WriteLine("\nBot cards:");
                            Bot.ShowCards();
                            Console.WriteLine($"\nsum of cards:{Bot.CardsSum()}");
                        }
                        else if (Bot.CardsSum() >= 21 && PlayerOne.CardsSum() >= 21)
                        {
                            PlayerOne.PlayerDraw();
                            Console.WriteLine("\nYour cards:");
                            PlayerOne.ShowCards();
                            Console.WriteLine($"\nsum of cards:{PlayerOne.CardsSum()}");
                            Console.WriteLine("\nBot cards:");
                            Bot.ShowCards();
                            Console.WriteLine($"\nsum of cards:{Bot.CardsSum()}");
                        }
                        break;
                    case 2:
                        while (PlayerOne.CardsSum() > Bot.CardsSum() && Bot.CheckSum() && Bot.CardsSum() != 21)
                        {

                            Bot.TakeCard(rand, PlayerOne);
                        }
                        if (PlayerOne.CardsSum() > Bot.CardsSum() || Bot.CardsSum() > 21)
                        {
                            PlayerOne.PlayerWin(Bot);

                        }
                        else PlayerOne.PlayerLose(Bot);
                        game = false;
                        break;
                    default: break;
                }
            }
            Console.WriteLine("Game end");
            break;
        case 3:
            return;
        default: break;
    }
}


enum CardsValue
{
    two = 2,
    three = 3,
    four = 4,
    five = 5,
    six = 6,
    seven = 7,
    eight = 8,
    nine = 9,
    ten_Valet_Dama_Korol = 10,
    Tuz = 11
}

struct Player(string _name, int _balance)
{
    private string name = _name;
    public int balance = _balance;
    public CardsValue[] cards = Array.Empty<CardsValue>();
    private int rate;
    public void PlayerInfo()
    {
        Console.WriteLine($"Player name:{name}\tBalance:{balance}");
    }
    public void TakeCard(Random rand, Player player)
    {
        CardsValue[] newcards = new CardsValue[cards.Length + 1];
        for (int i = 0; i < cards.Length; i++) newcards[i] = cards[i];
        do
        {
            int card = rand.Next(2, 12);
            newcards[cards.Length] = (CardsValue)card;
        } while (!CheckCardRepeat(player));
        cards = newcards;
    }
    public void ShowCards()
    {

        foreach (CardsValue element in cards)
        {
            Console.Write("{0}\t", element);
        }
    }
    public bool CheckSum()
    {
        int sum = 0;
        foreach (CardsValue card in cards)
        {
            sum += (int)card;
        }
        if (sum > 21) return false;

        return true;
    }
    public int CardsSum()
    {
        int sum = 0;
        foreach (CardsValue card in cards)
        {
            sum += (int)card;
        }
        return sum;
    }
    private bool CheckCardRepeat(Player player)
    {

        CardsValue[] secondplayercards = player.cards;
        foreach (CardsValue card in secondplayercards)
        {
            int temp = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] == card && (int)card != 10)
                {
                    temp++;
                }
            }
            for (int i = 0; i < secondplayercards.Length; i++)
            {
                if (secondplayercards[i] == card && (int)card != 10)
                {
                    temp++;
                }
            }
            if (temp >= 4) return false;
        }
        return true;
    }
    public void ClearCards()
    {
        cards = Array.Empty<CardsValue>();
    }
    public void MakeBet(Player player)
    {
        if(balance == 0)
        {
            Console.WriteLine("Oops, you don`t have money.");
            return;
        }
        if (player.balance == 0)
        {
            Console.WriteLine("Oops, Dealer don`t have money.");
            return;
        }

        Console.WriteLine("Make bet");
        rate = int.Parse(Console.ReadLine());
        if (rate > balance)
        {
            while (rate > balance)
            {
                Console.WriteLine("Oops, you don`t have enough money. Change your bet");
                rate = int.Parse(Console.ReadLine());
            }
        }
        if (rate > player.balance)
        {
            while (rate > player.balance)
            {
                Console.WriteLine($"Oops, Dealer have enough money. Change your bet. Dealer balance:{player.balance}");
                rate = int.Parse(Console.ReadLine());
            }
        }
        balance = balance - rate;
        Console.WriteLine($"you made bet. Your balance:{balance}");
    }
    public void PlayerWin(Player player)
    {
        Console.WriteLine("You win!");
        balance = balance + rate * 2;
        player.balance = balance - rate;
        Console.WriteLine($"Your balance:{balance}");
    }
    public void PlayerLose(Player player)
    {
        Console.WriteLine("You Lose!");
        player.balance = balance + rate * 2;
        Console.WriteLine($"Your balance:{balance}");
    }
    public void PlayerDraw()
    {
        Console.WriteLine("Draw!");
        balance = +rate;
        Console.WriteLine($"Your balance:{balance}");
    }
}