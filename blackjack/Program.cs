using System.Text;
int IntTime()
{
    DateTime currentTime = DateTime.Now;
    int timeAsInt = currentTime.Hour * 10000 + currentTime.Minute * 100 + currentTime.Second;
    return timeAsInt;
}

Console.OutputEncoding = Encoding.UTF8;
Player playerone = new Player("merely",300);
Player Bot = new Player("BOT",300);
Random rand = new Random(IntTime());
Random random = new Random(IntTime()+123);
while (playerone.CheckSum()&&Bot.CheckSum())
{
    Console.WriteLine("\nYour cards:");
    playerone.ShowCards();
    Console.WriteLine($"\nsum of cards:{playerone.CardsSum()}");
    Console.WriteLine("\nBot cards:");
    Bot.ShowCards();
    Console.WriteLine($"\nsum of cards:{Bot.CardsSum()}");
    Console.WriteLine("\nВиберіть дію\n[1]-Взяти карту\n[2]-Закінчити гру\n[3]-Вихід");
    int temp = int.Parse(Console.ReadLine());
    switch (temp)
    {
        case 1: int card = rand.Next(2,12);
                playerone.TakeCard(random, Bot);
                while (playerone.CardsSum() > Bot.CardsSum()&&Bot.CheckSum()&&Bot.CardsSum()!=21&&playerone.CheckSum())
                {
                    
                    Bot.TakeCard(rand,playerone);
                }
                if (!Bot.CheckSum()&&playerone.CardsSum()<=21)
                {
                    Console.WriteLine("Ви виграли");
                    Console.WriteLine("\nYour cards:");
                    playerone.ShowCards();
                    Console.WriteLine($"\nsum of cards:{playerone.CardsSum()}");
                    Console.WriteLine("\nBot cards:");
                    Bot.ShowCards();
                    Console.WriteLine($"\nsum of cards:{Bot.CardsSum()}");
                }else if (!playerone.CheckSum() && Bot.CardsSum() <= 21)
                {
                    Console.WriteLine("Ви програли");
                    Console.WriteLine("\nYour cards:");
                    playerone.ShowCards();
                    Console.WriteLine($"\nsum of cards:{playerone.CardsSum()}");
                    Console.WriteLine("\nBot cards:");
                    Bot.ShowCards();
                    Console.WriteLine($"\nsum of cards:{Bot.CardsSum()}");
                }else if( Bot.CardsSum() >= 21&&playerone.CardsSum()>=21)
                {   Console.WriteLine("Нічия"); 
                    Console.WriteLine("\nYour cards:");
                    playerone.ShowCards();
                    Console.WriteLine($"\nsum of cards:{playerone.CardsSum()}");
                    Console.WriteLine("\nBot cards:");
                    Bot.ShowCards();
                    Console.WriteLine($"\nsum of cards:{Bot.CardsSum()}");
                }
                break;
        case 2:
            while (playerone.CardsSum() > Bot.CardsSum()&&Bot.CheckSum()&&Bot.CardsSum()!=21)
                {
                   
                    Bot.TakeCard(rand,playerone);
                }
            if (playerone.CardsSum() > Bot.CardsSum()||Bot.CardsSum()>21)
            {
                Console.WriteLine("Ви виграли");

            }else Console.WriteLine("Ви програли");
            break;
        case 3: return; 

    }


}

enum CardsValue
{
    two=2,
    three=3,
    four=4,
    five=5,
    six=6,
    seven=7,
    eight=8,
    nine=9,
    ten_Valet_Dama_Korol=10, 
    Tuz=11
}

struct Player(string _name, int _balance)
{
    private string name=_name;
    private int balance=_balance; 
    public CardsValue[] cards = new CardsValue[0];

    public void TakeCard(Random rand, Player player)
    { 
        CardsValue [] newcards = new CardsValue[cards.Length+1];
        for(int i = 0;i<cards.Length;i++) newcards[i]=cards[i];
        do{
            int card = rand.Next(2,12);
            newcards[cards.Length] = (CardsValue)card; }while(!CheckCardRepeat(player));
        cards = newcards;
    }
    public void ShowCards() {
        
        foreach(CardsValue element in cards)
        {
            Console.Write("{0}\t",element);
        }
    }
    public bool CheckSum()
    {
        int sum=0;
        foreach(CardsValue card in cards)
        {
            sum+=(int)card;
        }
        if(sum>21)return false;

        return true;
    }
    public int CardsSum()
    {
        int sum=0;
        foreach(CardsValue card in cards)
        {
            sum+=(int)card;
        }
        return sum;
    }
    private bool CheckCardRepeat(Player player)
    {
        
        CardsValue[] secondplayercards = player.cards;
        foreach(CardsValue card in secondplayercards)
        {
            int temp=0;
            for(int i = 0; i < cards.Length; i++)
            {
                if(cards[i] == card&&(int)card!=10)
                {
                    temp++;
                }
            }
            for(int i = 0; i < secondplayercards.Length; i++)
            {
                if(secondplayercards[i] == card&&(int)card!=10)
                {
                    temp++;
                }
            }
            if(temp>=4)return false;
        }
        return true;
    }

}

