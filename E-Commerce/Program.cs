using System;

namespace ECommerce
{
    public class Cart
    {
        public String userName { get; set; }
        public String userEmail { get; set; }
        public int Total { get; set; }

        public Cart()
        {
            userEmail = "kani@gmail.com";
            userName = "Kani";
            Total = 1000;
        }
        public void GetCartDetails()
        {
            Console.WriteLine("Name: {0}, Email: {1}", userName, userEmail);

        }
    }
    public class Order
    {
        protected Cart _cart;
        public Order ()
        {
            _cart = new Cart();
        }
        public Order(Cart cart)
        {
            _cart = cart;
        }
        public virtual void checkout ()
        {

        }
    }
    public class INotificationservice
    {
        public void SendMail(Cart _cart)
        {
            _cart.GetCartDetails();
            Console.WriteLine("Mail Sent");
        }
    }
    public class IPaymentProcessor
    {
        public void ProcessCreditCard(PaymentDetails _paymentDetails, Cart _cart)
        {
            _paymentDetails.GetPaymentDetails();
            Console.WriteLine("Payment Successfull");
        }

    }
    public class IReservationService
    {
        public void ReserveInventory()
        {
            Console.WriteLine("Invetory");
        }
    }
    public class PaymentDetails
    {
        public long cardNumber { get; set; }
        public String cardName { get; set; }
        public String cardType { get; set; }
        public int CVV { get; set; }
        public PaymentDetails(long _cardNumebr, String _cardName, String _cardType, int _CVV )
        {
            cardNumber = _cardNumebr;
            cardName = _cardName;
            cardType = _cardType;
            CVV = _CVV;
        }
        public void GetPaymentDetails()
        {
            Console.WriteLine("CardName: {0}, cardNumber : {1}", cardName, cardNumber);

        }
        public override string ToString() => $"{this.cardName} - {this.cardNumber}";
    }
    class OnlineOrder : Order
    {
         private INotificationservice _notificationService; 

         private PaymentDetails _paymentDetails; 

         private IPaymentProcessor _paymentProcessor; 

         private IReservationService _reservationService; 

         public OnlineOrder(PaymentDetails paymentDetails, Cart _cart)
         {
             this._cart = _cart;

            _paymentDetails = paymentDetails;

            _paymentProcessor = new IPaymentProcessor();

            _notificationService = new INotificationservice();

            _reservationService = new IReservationService();
         }

        public override void checkout()
        {
            Console.WriteLine("------- Online Order Checkout ------");

            _paymentProcessor.ProcessCreditCard(_paymentDetails, this._cart);

            _reservationService.ReserveInventory();

            _notificationService.SendMail(_cart);

        }
    }

    class SpotOrder : Order
    {
        private PaymentDetails _paymentDetails;

        private IPaymentProcessor _paymentProcessor;
        public SpotOrder(PaymentDetails paymentDetails, Cart _cart)
        {
            this._cart = _cart;

            _paymentDetails = paymentDetails;

            _paymentProcessor = new IPaymentProcessor();
        }

        public override void checkout()
        {
            Console.WriteLine("------- Spot Order Checkout ------");

            _paymentProcessor.ProcessCreditCard(_paymentDetails, this._cart);

        }
    }
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("E-Commerce Application Implemented following Single Responsibility Priniple!!!");
            Cart _cart = new Cart();
            PaymentDetails _paymentDetails = new PaymentDetails(9078653421, "Kanimozhi", "Platinum", 555);
            OnlineOrder onlineOrder = new OnlineOrder(_paymentDetails, _cart);
            onlineOrder.checkout();

            SpotOrder spotOrder = new SpotOrder(_paymentDetails, _cart);
            spotOrder.checkout();

         }
    }
}
