namespace CAEvent
{
    class Program //Subscriber
    {
        static void Main(string[] args)
        {

            var stock = new Stock("Amazon");
            stock.Price = 100;

            stock.onPriceChanged += (Stock stock, decimal oldPrice) => {
                string result = "";
                if (stock.Price > oldPrice)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    result = "Increased";
                }
                else if (oldPrice > stock.Price)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    result = "Decreased";
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    result = "Unchanged";
                }
                Console.WriteLine($"{stock.Name} price is→ ${stock.Price} STATUS→ {result}");
            };

            stock.ChangeStockPriceBy(0.05m);
            stock.ChangeStockPriceBy(-0.02m);
            stock.ChangeStockPriceBy(0.00m);




            Console.ReadLine();
        }

       
    }
    //Events have to use with it delegate
    public delegate void StockPriceChangeHandler(Stock stock,decimal oldPrice);

    public class Stock // Publisher
    {
        private string name;
        private decimal price;


        //Event Intialize
        public event StockPriceChangeHandler onPriceChanged;

        public string Name => this.name;
        public decimal Price { get => this.price; set => this.price = value; }

        public Stock(string stockname)
        {
            this.name = stockname;
        }

        public void ChangeStockPriceBy(decimal percent)
        {
            decimal oldPrice = this.price;
            this.Price += Math.Round(this.price * percent, 2);

            //Event Fire
            if (onPriceChanged != null) // make sure this is subscriber
            {
                onPriceChanged(this, oldPrice);
            }
        }
    }
}