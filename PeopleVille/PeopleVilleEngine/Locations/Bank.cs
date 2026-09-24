using System;
using System.Collections.Generic;
using System.Linq;

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
        public int AccountId { get; private set; }
        public int VillagerId { get; private set; }
        public int Money { get; private set; }
        public bool HasBankCard { get; private set; }

        public BankAccount(int villagerId, int accountId)
        {
            AccountId = accountId;
            VillagerId = villagerId;
            Money = 0;
            HasBankCard = false;
        }

        public void DepositMoney(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException(
                    "Amount must be greater than 0."
                );

            Money += amount;
        }

        public void WithdrawMoney(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException(
                    "Amount must be greater than 0."
                );

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

        private int GenerateAccountId()
        {
            int accountId;

            do
            {
                accountId = RNG.GetInstance().Next(100000, 1000000);
            }
            while (Accounts.Values.Any(
                account => account.AccountId == accountId
            ));

            return accountId;
        }

        // Creates a new account for a villager
        public void CreateAccount(int villagerId)
        {
            if (!Accounts.ContainsKey(villagerId))
            {
                int accountId = GenerateAccountId();

                Accounts.Add(
                    villagerId,
                    new BankAccount(villagerId, accountId)
                );
            }
        }

        // Gets a villager's account
        public BankAccount GetAccount(int villagerId)
        {
            if (!Accounts.ContainsKey(villagerId))
            {
                CreateAccount(villagerId);
            }

            return Accounts[villagerId];
        }

        // Gets an account using the random AccountId
        public BankAccount? GetAccountById(int accountId)
        {
            return Accounts.Values.FirstOrDefault(
                account => account.AccountId == accountId
            );
        }

        // Shows balance
        public void CheckMoney(int villagerId)
        {
            BankAccount account = GetAccount(villagerId);

            Console.WriteLine(
                $"Your balance is: {account.Money}"
            );
        }

        // Deposits money
        public void DepositMoney(int villagerId, int amount)
        {
            BankAccount account = GetAccount(villagerId);

            account.DepositMoney(amount);

            Console.WriteLine(
                $"Deposited {amount}. New balance: {account.Money}"
            );
        }

        // Withdraws money
        public void WithdrawMoney(int villagerId, int amount)
        {
            BankAccount account = GetAccount(villagerId);

            account.WithdrawMoney(amount);

            Console.WriteLine(
                $"Withdrew {amount}. New balance: {account.Money}"
            );
        }

        // Gives the villager a bank card
        public void GetBankCard(int villagerId)
        {
            BankAccount account = GetAccount(villagerId);

            account.GetBankCard();

            Console.WriteLine(
                "Bank card issued successfully."
            );
        }

        // Pays a bill
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

        // Shows bank card status
        public void ShowBankCard(int villagerId)
        {
            BankAccount account = GetAccount(villagerId);

            if (account.HasBankCard)
            {
                Console.WriteLine(
                    "You have a bank card."
                );
            }
            else
            {
                Console.WriteLine(
                    "You do not have a bank card."
                );
            }
        }

        // Shows account information
        public void ShowAccount(int villagerId)
        {
            BankAccount account = GetAccount(villagerId);

            Console.WriteLine("Account is active.");
            Console.WriteLine(
                $"Villager ID: {account.VillagerId}"
            );
            Console.WriteLine(
                $"Account ID: {account.AccountId}"
            );
            Console.WriteLine(
                $"Balance: {account.Money}"
            );
        }

        // Transfers money using AccountId
        public void TransferMoney(
            int fromVillagerId,
            int toAccountId,
            int amount)
        {
            BankAccount fromAccount = GetAccount(fromVillagerId);

            BankAccount? toAccount = GetAccountById(toAccountId);

            if (toAccount == null)
            {
                Console.WriteLine(
                    "Account not found."
                );

                return;
            }

            fromAccount.WithdrawMoney(amount);
            toAccount.DepositMoney(amount);

            Console.WriteLine(
                $"Transferred {amount} to account {toAccountId}."
            );
        }

        public List<BaseVillager> Villagers()
        {
            throw new NotImplementedException();
        }
    }
}
