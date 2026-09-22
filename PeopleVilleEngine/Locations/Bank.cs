using System;
using System.Collections.Generic;

namespace PeopleVilleEngine.Locations
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message)
            : base(message)
        {
        }
    }

    public class BankAccount
    {
        public int VillagerId { get; private set; }
        public int Money { get; private set; }
        public bool HasBankCard { get; private set; }

        public BankAccount(int villagerId)
        {
            VillagerId = villagerId;
            Money = 0;
            HasBankCard = false;
        }

        public void DepositMoney(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than 0.");

            Money += amount;
        }

        public void WithdrawMoney(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than 0.");

            if (amount > Money)
            {
                throw new InsufficientFundsException(
                    "You do not have enough money."
                );
            }

            Money -= amount;
        }

        public void GetBankCard()
        {
            HasBankCard = true;
        }
    }

    public class Bank : ILocation
    {
        public string Name { get; set; }

        private Dictionary<int, BankAccount> Accounts { get; set; }

        public Bank(string name)
        {
            Name = name;
            Accounts = new Dictionary<int, BankAccount>();
        }

        // Creates a new account for a villager
        public void CreateAccount(int villagerId)
        {
            if (!Accounts.ContainsKey(villagerId))
            {
                Accounts.Add(
                    villagerId,
                    new BankAccount(villagerId)
                );
            }
        }

        
        public BankAccount GetAccount(int villagerId)
        {
            if (!Accounts.ContainsKey(villagerId))
            {
                CreateAccount(villagerId);
            }

            return Accounts[villagerId];
        }

        public void CheckMoney(int villagerId)
        {
            BankAccount account = GetAccount(villagerId);

            Console.WriteLine(
                $"Your balance is: {account.Money}"
            );
        }

        public void DepositMoney(int villagerId, int amount)
        {
            BankAccount account = GetAccount(villagerId);

            account.DepositMoney(amount);

            Console.WriteLine(
                $"Deposited {amount}. New balance: {account.Money}"
            );
        }

        public void WithdrawMoney(int villagerId, int amount)
        {
            BankAccount account = GetAccount(villagerId);

            account.WithdrawMoney(amount);

            Console.WriteLine(
                $"Withdrew {amount}. New balance: {account.Money}"
            );
        }

        public void GetBankCard(int villagerId)
        {
            BankAccount account = GetAccount(villagerId);

            account.GetBankCard();

            Console.WriteLine("Bank card issued successfully.");
        }

        public void PayBill(int villagerId, int amount)
        {
            BankAccount account = GetAccount(villagerId);

            if (!account.HasBankCard)
            {
                Console.WriteLine(
                    "You need a bank card to pay bills."
                );

                return;
            }

            account.WithdrawMoney(amount);

            Console.WriteLine(
                $"Bill paid. New balance: {account.Money}"
            );
        }

        public void ShowBankCard(int villagerId)
        {
            BankAccount account = GetAccount(villagerId);

            if (account.HasBankCard)
            {
                Console.WriteLine("You have a bank card.");
            }
            else
            {
                Console.WriteLine("You do not have a bank card.");
            }
        }

        public void ShowAccount(int villagerId)
        {
            BankAccount account = GetAccount(villagerId);

            Console.WriteLine("Account is active.");
            Console.WriteLine($"Villager ID: {account.VillagerId}");
            Console.WriteLine($"Balance: {account.Money}");
        }

        public List<BaseVillager> Villagers()
        {
            throw new NotImplementedException();
        }
    }
}   